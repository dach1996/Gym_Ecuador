using System.Linq.Expressions;
using Common.Blob.Models.Response;
using Common.Cache.Interface;
using Common.Clock;
using Common.Messages;
using Common.PushNotification.Model;
using Common.Queue.Model.Enum;
using Common.Queue.Model.Template;
using Common.Templates.Interface;
using Common.Utils.ConstansCodes;
using Common.Utils.CustomExceptions;
using Common.Utils.Extensions;
using Common.WebCommon.Helper;
using Common.WebCommon.Models;
using Common.WebCommon.Models.Enum;
using Common.WebCommon.Models.Queues;
using Common.WebCommon.Templates.Notification;
using LogicCommon.Model.CacheModel;
using LogicCommon.Model.Request.File;
using LogicCommon.Model.Request.HelperValidation;
using LogicCommon.Model.Request.NotificationPush;
using LogicCommon.Model.Request.Queue;
using LogicCommon.Model.Response.File;
using LogicCommon.Model.Response.Queue;
using PersistenceDb.Models.Administration;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.UnitOfWork;

namespace LogicCommon.BusinessLogic;
/// <summary>
/// Clase base para Lógica común
/// </summary>
public abstract class BusinessLogicCommonBase
{
    protected readonly IPluginFactory PluginFactory;
    protected readonly ILogger<BusinessLogicCommonBase> Logger;
    private DateTime? _now;
    protected DateTime Now => _now ??= Clock.Now();
    private IClock _clock;
    protected IClock Clock => _clock ??= SetClockInstance();
    protected readonly AppSettingsCommon AppSettings;
    protected CommonContextRequest CommonContextRequest { get; set; }
    protected IUnitOfWork UnitOfWork => _unitOfWork ??= PluginFactory.GetType<IUnitOfWork>();
    private IUnitOfWork _unitOfWork;

    private IMediator _mediator;
    protected IMediator Mediator => _mediator ??= PluginFactory.GetType<IMediator>();
    private IAdministratorCache _administratorCache;
    protected IAdministratorCache AdministratorCache => _administratorCache ??= PluginFactory.GetType<IAdministratorCache>();
    private ITemplateFactory _templateFactory;
    protected ITemplateFactory TemplateFactory => _templateFactory ??= PluginFactory.GetType<ITemplateFactory>();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="pluginFactory"></param>
    protected BusinessLogicCommonBase(
        ILogger<BusinessLogicCommonBase> logger,
        IPluginFactory pluginFactory)
    {
        Logger = logger;
        PluginFactory = pluginFactory;
        AppSettings = PluginFactory.GetType<AppSettingsCommon>();
    }

    /// <summary>
    /// Configura la Instancia de Clock
    /// </summary>
    private IClock SetClockInstance()
    {
        if (CommonContextRequest?.TimeZone.IsNullOrEmpty() ?? true)
            throw new CustomException((int)MessagesCodesError.SystemError, $"La zona horaria en el Contexto está vacía");
        var clockInstance = PluginFactory.GetType<IClock>();
        clockInstance.ConfigureTimeZone(CommonContextRequest.TimeZone);
        return clockInstance;
    }




    /// <summary>
    /// Ejecuta el proceso
    /// </summary>
    /// <param name="process"></param>
    /// <param name="unitOfWorkTypes"></param>
    /// <param name="applyTransaction"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected async Task<T> ExecuteHandlerAsync<T>(
           ICommonBaseRequest request,
           Func<Task<T>> process
           )
    {
        try
        {
            CommonContextRequest ??= request.CommonContextRequest
                ?? throw new CustomException((int)MessagesCodesError.SystemError, "El contexto está vacío");
            //Ejecuta el proceso
            var result = await process().ConfigureAwait(false);
            return result;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error: {@Message}", ex.Message);
            return default;
        }
    }


    /// <summary>
    /// Envía de Queue
    /// </summary>
    /// <param name="queueTemplate"></param>
    /// <param name="delaySeconds"></param>
    /// <returns></returns>
    protected async Task<SendMessageQueueResponse> SendQueueMessageAsync(
        IQueueTemplate queueTemplate,
        int delaySeconds = 0)
        => await Mediator.Send(new SendMessageQueueRequest(queueTemplate, CommonContextRequest, delaySeconds)).ConfigureAwait(false);

    /// <summary>
    /// Envía de Queue y guarda el Registro
    /// </summary>
    /// <param name="queueTemplate"></param>
    /// <param name="delaySeconds"></param>
    /// <param name="dateTime"></param>
    /// <returns></returns>
    protected async Task<QueueMessage> SendAndSaveQueueMessageAsync(
        IQueueTemplate queueTemplate,
        int delaySeconds = 0,
        DateTime? dateTime = null
        )
    {
        var sendMessageResponse = await SendQueueMessageAsync(queueTemplate, delaySeconds).ConfigureAwait(false);
        return await UnitOfWork.QueueMessageRepository.AddAsync(new()
        {
            DateTimeRegister = dateTime ?? Clock.Now(),
            InternlaIdentifier = sendMessageResponse.InternalIdentifier,
            Type = sendMessageResponse.TypeCode,
            AdditionalInformation = new SendMessageQueueAzureResponse(sendMessageResponse.MessageId, sendMessageResponse.PopReceipt).ToJson()
        }).ConfigureAwait(false);
    }

    /// <summary>
    /// Elimina un mensaje del Queue
    /// </summary>
    /// <param name="queueTemplate"></param>
    /// <param name="delaySeconds"></param>
    /// <returns></returns>
    protected async Task<DeleteMessageQueueResponse> DeleteQueueMessageAsync(Expression<Func<QueueMessage, bool>> where)
    {
        var deleteMessageQueueItems = (await UnitOfWork.QueueMessageRepository.GetGenericAsync(
            select => new
            {
                select.AdditionalInformation,
                select.Type
            },
            where
        ).ConfigureAwait(false))
        ?.Select(select => new
        {
            queueMessage = select,
            sendMessageQueueAzureResponse = select.AdditionalInformation.ToObject<SendMessageQueueAzureResponse>()
        })
        .Select(select => new DeleteMessageQueueItem((QueueTemplateName)select.queueMessage.Type, select.sendMessageQueueAzureResponse.MessageId, select.sendMessageQueueAzureResponse.PopReceipt));
        return deleteMessageQueueItems.IsNullOrEmpty()
            ? DeleteMessageQueueResponse.FailResponse(0)
            : await Mediator.Send(new DeleteMessageQueueRequest(deleteMessageQueueItems, CommonContextRequest)).ConfigureAwait(false);
    }

    /// <summary>
    /// Elimina un mensaje
    /// </summary>
    /// <param name="queue"></param>
    /// <returns></returns>
    protected async Task<DeleteMessageQueueResponse> DeleteQueueMessageAsync(QueueMessage queue)
    {
        var sendMessageQueueAzureResponse = queue.AdditionalInformation.ToObject<SendMessageQueueAzureResponse>();
        return await Mediator.Send(new DeleteMessageQueueRequest((QueueTemplateName)queue.Type, sendMessageQueueAzureResponse.MessageId, sendMessageQueueAzureResponse.PopReceipt, CommonContextRequest)).ConfigureAwait(false);
    }

    /// <summary>
    /// Elimina un mensaje del Queue
    /// </summary>
    /// <param name="queueTemplate"></param>
    /// <param name="delaySeconds"></param>
    /// <returns></returns>
    protected async Task DeleteQueueMessageWithoutResponseAsync(Expression<Func<QueueMessage, bool>> where)
    {
        var deleteMessageQueueItems = (await UnitOfWork.QueueMessageRepository.GetGenericAsync(
            select => new
            {
                select.AdditionalInformation,
                select.Type
            },
            where
        ).ConfigureAwait(false))
        ?.Select(select => new
        {
            queueMessage = select,
            sendMessageQueueAzureResponse = select.AdditionalInformation.ToObject<SendMessageQueueAzureResponse>()
        })
        .Select(select => new DeleteMessageQueueItem((QueueTemplateName)select.queueMessage.Type, select.sendMessageQueueAzureResponse.MessageId, select.sendMessageQueueAzureResponse.PopReceipt));
        if (!deleteMessageQueueItems.IsNullOrEmpty())
            _ = Mediator.Send(new DeleteMessageQueueRequest(deleteMessageQueueItems, CommonContextRequest));
    }


    /// <summary>
    /// Envía notificación push
    /// </summary>
    /// <param name="title"></param>
    /// <param name="body"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    protected async Task SendNotificationAsync(
        NotificationTemplateModelBase notificationTemplateModelBase,
        int userId,
        NotificationAction action = NotificationAction.Notification)
    {
        var template = TemplateFactory.NotificationTemplate.GetTemplate($"{notificationTemplateModelBase.NotificationTemplateName}", notificationTemplateModelBase);
        await Mediator.Send(new SendNotificationPushUsersRequest(template.Title, template.Body, userId, action, CommonContextRequest));
    }


    /// <summary>
    /// Obtiene el Guid de los usuarios
    /// </summary>
    /// <param name="userIds"></param>
    /// <returns></returns>
    protected async Task<Dictionary<int, UserCacheInformation>> GetUserInformationByUserIdAsync(IEnumerable<int> userIds)
    {
        userIds = [.. userIds.Distinct()];
        //Obtiene los usuarios de la cache
        var userCacheInformationTasks =
            userIds.ToDictionary(key => key, value => AdministratorCache.TryGetAsync<UserCacheInformation>(
            CacheCodes.UserInformationByUserId(value)));
        //Espera a que se obtengan los usuarios de la cache
        await Task.WhenAll(userCacheInformationTasks.Values).ConfigureAwait(false);
        //Obtiene los usuarios de la cache
        var userCacheInformation = userCacheInformationTasks.ToDictionary(key => key.Key, value => value.Value.Result);
        //Obtiene los usuarios que no se encontraron en la cache
        var userCacheNotFind = userCacheInformation.Where(user => user.Value is null);
        if (!userCacheNotFind.IsNullOrEmpty())
        {
            var userIdsNotFind = userCacheNotFind.Select(user => user.Key);
            var newUsers = await UnitOfWork.UserRepository.GetGenericAsync(
                select => new UserCacheInformation
                {
                    Guid = select.Guid,
                    Id = select.Id,
                },
                where: user => userIdsNotFind.Contains(user.Id)
            ).ConfigureAwait(false);

            foreach (var user in newUsers)
            {
                //Agrega los usuarios a la cache
                userCacheInformation[user.Id] = user;
                //Agrega los usuarios a la cache
                _ = AdministratorCache.SetAsync(
                    CacheCodes.UserInformationByUserId(user.Id),
                    user).ConfigureAwait(false);
            }
        }
        return userCacheInformation;
    }

    /// <summary>
    /// Obtiene la ruta de almacenamiento          
    /// </summary>
    /// <param name="pathCode"></param>
    /// <returns></returns>
    protected async Task<FileBasePathCacheInformation> GetFileBasePathCacheInformationByPathCodeAsync(PathCode pathCode)
    {
        return (await AdministratorCache.TryGetOrSetAsync(CacheCodes.FILE_BASE_PATHS, async () =>
            (await UnitOfWork.FileBasePathRepository.GetByAsync().ConfigureAwait(false))
            .Select(select => new FileBasePathCacheInformation
            {
                Id = select.Id,
                PathCode = select.Code.ToEnumFromMember<PathCode>(),
                BaseUrl = select.BaseUrl,
                FileDirectoryPath = select.FileDirectoryPath,
                Implementation = select.Implementation
            }).ToList()).ConfigureAwait(false))
            .Find(where => where.PathCode == pathCode)
            ?? throw new CustomException((int)MessagesCodesError.SystemError, $"No se encontró la ruta de almacenamiento '{pathCode}'");
    }

    /// <summary>
    /// Procesa las imágenes
    /// </summary>
    /// <param name="images"></param>
    /// <param name="pathCode"></param>
    /// <param name="Func<List<RequestEncodeFile>"></param>
    /// <param name="processCreateImagesAsync"></param>
    /// <param name="Func<List<RequestEncodeFile>"></param>
    /// <param name="processDeleteImagesAsync"></param>
    /// <returns></returns>
    protected async Task ProcessImagesAsync(
        List<RequestEncodeFile> images,
        PathCode pathCode,
        Func<List<RequestEncodeFile>, Model.Response.File.UpdateFileResponse, Task> processCreateImagesAsync = null,
        Func<List<RequestEncodeFile>, Task> beforeDeleteImagesAsync = null,
        Func<List<RequestEncodeFile>, Model.Response.File.DeleteFileResponse, Task> processDeleteImagesAsync = null,
        Func<string, string> getFileExtension = null,
        string folderPath = null
        )
    {
        RequestEncodeFileValidation.Validate(images);
        foreach (var group in images?.GroupBy(group => group.Action))
        {
            switch (group.Key)
            {
                case ActionFile.Create:

                    var items = group.Select(select => new UpdateBlobFileItemRequest
                    {
                        File = Convert.FromBase64String(select.EncodeContent),
                        FileName = getFileExtension?.Invoke(select.FileExtension) ?? select.FileName,
                        ReplaceIfExist = true
                    }).ToList();
                    var imageItemResponse = await Mediator.Send(new UpdateBlobFileRequest(pathCode, items, CommonContextRequest, folderPath)).ConfigureAwait(false);
                    await (processCreateImagesAsync?.Invoke([.. group], imageItemResponse)).ConfigureAwait(false);
                    break;
                case ActionFile.Delete:
                    await (beforeDeleteImagesAsync?.Invoke([.. group])).ConfigureAwait(false);
                    var guids = group.Select(select => select.Guid.Value).ToList();
                    var deleteFileResponse = await Mediator.Send(new DeleteBlobFileByGuidRequest(guids, CommonContextRequest)).ConfigureAwait(false);
                    await (processDeleteImagesAsync?.Invoke([.. group], deleteFileResponse)).ConfigureAwait(false);
                    break;
                case ActionFile.None:
                    break;
            }
        }
    }
}