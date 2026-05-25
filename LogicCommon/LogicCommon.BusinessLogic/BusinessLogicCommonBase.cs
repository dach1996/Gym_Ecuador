using Common.ArtificialIntelligence;
using Common.ArtificialIntelligence.Model.Common;
using Common.Cache.Interface;
using Common.Clock;
using Common.Messages;
using Common.Security.Interface;
using Common.Security.Model.Enum;
using Common.Templates.Interface;
using Common.Utils.ConstansCodes;
using Common.Utils.CustomExceptions;
using Common.Utils.Extensions;
using Common.WebCommon.IaTemplateModel;
using Common.WebCommon.Models;
using Common.WebCommon.Models.Enum;
using LogicCommon.Model.CacheModel;
using LogicCommon.Model.Request.File;
using LogicCommon.Model.Request.HelperValidation;
using PasswordGenerator;
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
        CommonContextRequest ??= request.CommonContextRequest
            ?? throw new CustomException((int)MessagesCodesError.SystemError, "El contexto está vacío");
        //Ejecuta el proceso
        var result = await process().ConfigureAwait(false);
        return result;
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
    /// Desencripta un texto usando RSA
    /// </summary>
    /// <param name="listTextsEncrypt"></param>
    /// <param name="hasEncode"></param>
    /// <returns></returns>
    protected async Task<Dictionary<string, string>> DecryptRsaAsync(IEnumerable<string> listTextsEncrypt, bool hasEncode)
    {
        var rsaImplementation = PluginFactory.GetPlugin<IRsaSecurity>(RsaSecurityImplementation.ServerGeneral.ToString().ToUpper());
        var dictionaryResponse = new Dictionary<string, string>();
        foreach (var text in listTextsEncrypt)
        {
            var valueEncrypt = text;
            if (hasEncode)
                valueEncrypt = text.Decode();
            var stringResponse = rsaImplementation.Decrypt(valueEncrypt);
            dictionaryResponse.Add(text, stringResponse);
        }
        return await Task.FromResult(dictionaryResponse).ConfigureAwait(false);
    }

    /// <summary>
    /// Encripta un texto usando RSA
    /// </summary>
    /// <param name="listTexts"></param>
    /// <param name="applyEncode"></param>
    /// <returns></returns>
    protected async Task<Dictionary<string, string>> EncryptRsaAsync(IEnumerable<string> listTexts, bool applyEncode)
    {
        var rsaImplementation = PluginFactory.GetPlugin<IRsaSecurity>(RsaSecurityImplementation.ServerGeneral.ToString().ToUpper());
        var dictionaryResponse = new Dictionary<string, string>();
        foreach (var text in listTexts.Distinct())
        {
            var stringResponse = rsaImplementation.Encrypt(text);
            if (applyEncode)
                stringResponse = stringResponse.Encode();
            dictionaryResponse.Add(text, stringResponse);
        }
        return await Task.FromResult(dictionaryResponse).ConfigureAwait(false);
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
        foreach (var group in (images ?? []).GroupBy(group => group.Action))
        {
            switch (group.Key)
            {
                case ActionFile.Create:

                    var items = group.Select(select => new UpdateBlobFileItemRequest
                    {
                        File = Convert.FromBase64String(select.EncodeContent),
                        FileName = getFileExtension?.Invoke(select.FileExtension) ?? (select.FileName + "." + select.FileExtension.Replace(".", "")),
                        ReplaceIfExist = true
                    }).ToList();
                    var imageItemResponse = await Mediator.Send(new UpdateBlobFileRequest(pathCode, items, CommonContextRequest, folderPath)).ConfigureAwait(false);
                    if (processCreateImagesAsync is not null)
                        await processCreateImagesAsync.Invoke([.. group], imageItemResponse).ConfigureAwait(false);
                    break;
                case ActionFile.Delete:
                    if (beforeDeleteImagesAsync is not null)
                        await beforeDeleteImagesAsync.Invoke([.. group]).ConfigureAwait(false);
                    var guids = group.Select(select => select.Guid.Value).ToList();
                    var deleteFileResponse = await Mediator.Send(new DeleteBlobFileByGuidRequest(guids, CommonContextRequest)).ConfigureAwait(false);
                    if (processDeleteImagesAsync is not null)
                        await processDeleteImagesAsync.Invoke([.. group], deleteFileResponse).ConfigureAwait(false);
                    break;
                case ActionFile.None:
                    break;
            }
        }
    }

    /// <summary>
    /// Obtiene los roles
    /// </summary>
    /// <returns></returns>
    protected async Task<List<PersistenceDb.Models.Authentication.Role>> GetRolesAsync()
    {
        return await AdministratorCache.TryGetOrSetAsync(CacheCodes.ROLES, async () =>
            await UnitOfWork.RoleRepository.GetByAsync().ConfigureAwait(false)
        ).ConfigureAwait(false);
    }


    /// <summary>
    /// Obtiene las plataformas
    /// </summary>
    /// <returns></returns>
    public async Task<List<PersistenceDb.Models.Authentication.Platform>> GetPlatformsAsync()
        => await AdministratorCache.TryGetOrSetAsync(CacheCodes.PLATFORM_ROLES, async () =>
            await UnitOfWork.PlatformRepository.GetByAsync().ConfigureAwait(false)
        ).ConfigureAwait(false);

    /// <summary>
    /// Genera una contraseña
    /// </summary>
    /// <returns></returns>
    protected static string GeneratePassword() => new Password()
            .IncludeLowercase()
            .IncludeUppercase()
            .IncludeNumeric()
            .LengthRequired(10)
            .Next();

    /// <summary>
    /// Procesa un documento con una plantilla de la IA y devuelve el objeto de respuesta
    /// </summary>
    /// <param name="document"></param>
    /// <param name="externalSuiteTemplate"></param>
    /// <typeparam name="TIaObjectResponse"></typeparam>
    /// <returns></returns>
    protected async Task<TIaObjectResponse> ProcessDocumentTemplateIaAsJsonAsync<
    TIaObjectResponse>(byte[] document,
    IIaTemplateModel IaTemplateModel)
    {
        //Obtiene la plantilla de la IA
        var iaTemplateModelResponse = PluginFactory.GetType<IaTemplateFormat>().GetTemplate(IaTemplateModel);
        //Procesa la imagen con la IA
        var processedTextResponse = await PluginFactory.GetPlugin<IArtificialIntelligence>(iaTemplateModelResponse.AiImplementation)
            .ProcessDocumentAsync(new(
                document,
                iaTemplateModelResponse.Behavior,
                iaTemplateModelResponse.Indications,
                iaTemplateModelResponse.ResponseType.ToEnum<ProcessResponseType>()))
            .ConfigureAwait(false);
        var objectResponse = processedTextResponse.ToObject<TIaObjectResponse>()
            ?? throw new CustomException((int)MessagesCodesError.SystemError, $"No se pudo parsear la información de la IA: {processedTextResponse}");
        return objectResponse;
    }

    /// <summary>
    /// Obtiene la url de la imagen por el id del archivo
    /// </summary>
    /// <param name="fileId"></param>
    /// <returns></returns>
    protected async Task<string> GetUrlImageByCacheAsync(int fileId)
    {
        return await AdministratorCache.TryGetOrSetAsync(CacheCodes.UrlImageByFileId(fileId),
        async () => await UnitOfWork.FileRepository.GetFirstOrDefaultGenericAsync(
            select => select.FileBasePath.BaseUrl + select.Name,
            where => where.Id == fileId
        ).ConfigureAwait(false)).ConfigureAwait(false);
    }

    /// <summary>
    /// Calcula el índice de masa corporal
    /// </summary>
    /// <param name="weight">Peso en kilogramos</param>
    /// <param name="height">Altura en metros</param>
    /// <returns></returns>
    public static decimal CalculateBmi(decimal weight, decimal height) => Math.Round(weight / (height * height), 2);
}