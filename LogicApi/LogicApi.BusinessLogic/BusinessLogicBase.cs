using Common.Tasks;
using Common.UserDocumentation;
using Common.UserDocumentation.Models.Response;
using LogicApi.BusinessLogic.LoggerHandler;
using LogicApi.Model.Request.Administration;
using LogicApi.Model.Request.CommonConfiguration;
using LogicApi.Model.Request.Logger;
using LogicApi.Model.Request.Security;
using LogicApi.Model.Response.Common;
using LogicApi.Model.Response.CommonConfiguration.Common;
using LogicCommon.BusinessLogic;
using PersistenceDb.Models.Authentication;
namespace LogicApi.BusinessLogic;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class BusinessLogicBase(
    ILogger<BusinessLogicBase> logger,
    IPluginFactory pluginFactory) : BusinessLogicCommonBase(logger, pluginFactory)
{
    private IMapper _mapper;
    protected IMapper Mapper => _mapper ??= PluginFactory.GetType<IMapper>();
    private IUserMessages _userMessages;
    protected IUserMessages UserMessages => _userMessages ??= PluginFactory.GetType<IUserMessages>();
    private AppSettingsApi _appSettings;
    protected AppSettingsApi AppSettingsApi => _appSettings ??= PluginFactory.GetType<AppSettingsApi>();
    private TaskExecutorBuilder _taskExecutorBuilder;
    protected TaskExecutorBuilder TaskExecutorBuilder => _taskExecutorBuilder ??= PluginFactory.GetType<TaskExecutorBuilder>();
    private IDocumentationServices _documentationServices;
  
    protected IDocumentationServices DocumentationServices => _documentationServices ??=
        PluginFactory.GetPlugin<IDocumentationServices>(AppSettingsApi.DocumentationServicesConfiguration?.CurrentImplementation, true);
    protected ContextRequest ContextRequest;
    protected int UserId { get => ContextRequest.CustomClaims.UserId ?? throw new CustomException((int)MessagesCodesError.SystemError, "No se encontró el Id del usuario en el contexto"); }
    protected int PersonId { get => ContextRequest.CustomClaims.PersonId ?? throw new CustomException((int)MessagesCodesError.SystemError, "No se encontró el Id de la persona en el contexto"); }
    protected string MobileId { get => ContextRequest?.Headers?.DeviceId; }
    protected int DeviceId { get => ContextRequest.CustomClaims.DeviceId ?? throw new CustomException((int)MessagesCodesError.SystemError, "No se encontró el Id del dispositivo en el contexto"); }
    protected Guid UserGuid { get => ContextRequest.CustomClaims.UserGuid ?? throw new CustomException((int)MessagesCodesError.SystemError, "No se encontró el Guid del usuario en el contexto"); }

    /// <summary>
    /// Ejecuta el proceso dentro de la configuración inicial
    /// </summary>
    /// <param name="operationName">Nombre de Operación</param>
    /// <param name="request">Request que implementa Clase Base</param>
    /// <param name="process">Proceso o Ejecución</param>
    /// <param name="unitOfWorks">Lista de Unit Of Work</param>
    /// <param name="applyTransaction">Verifica si aplicar Transacción</param>
    /// <param name="registerLogAudit">Verifica si registra en Log de Auditoría</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected async Task<T> ExecuteHandlerAsync<T>(
        OperationApiName operationName,
        IApiBaseRequest request,
        Func<Task<T>> process,
        bool registerLogAudit = false)
    {
        try
        {
            SetContextRequest(request.ContextRequest);
            //Ejecuta el proceso
            var result = await process().ConfigureAwait(false);
            if (registerLogAudit)
                ExecuteTaskCreateAuditLog(RegisterLogAuditExecutorModel.SuccessRegister(operationName, request, result));
            return result;
        }
        catch (CustomException ex)
        {
            var message = $"{UserMessages.GetErrorMessageByCode(ex.Code).Message}" + (ex.Message.IsNullOrEmpty() ? $"Razón: {ex.Message}" : string.Empty);
            if (registerLogAudit)
                ExecuteTaskCreateAuditLog(RegisterLogAuditExecutorModel.ErrorRegister(operationName, request, message));
            throw;
        }
        catch (Exception ex)
        {
            if (registerLogAudit)
                ExecuteTaskCreateAuditLog(RegisterLogAuditExecutorModel.ErrorRegister(operationName, request, ex.Message));
            throw;
        }
    }


    /// <summary>
    /// Ejecuta el proceso dentro de la configuración inicial
    /// </summary>
    /// <param name="operationApiName"></param>
    /// <param name="cacheKey"></param>
    /// <param name="request"></param>
    /// <param name="process"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected async Task<T> ExecuteHandlerCacheAsync<T>(
            OperationApiName operationApiName,
            string cacheKey,
            IApiBaseRequest request,
            Func<Task<T>> process)
        => await AdministratorCache.TryGetOrSetAsync(
            cacheKey, () => ExecuteHandlerAsync(operationApiName, request, process, false)).ConfigureAwait(false);



    /// <summary>
    /// Ejecuta asincronamente el almacenado de un log
    /// </summary>
    /// <param name="model"></param>
    private void ExecuteTaskCreateAuditLog(RegisterLogAuditExecutorModel model)
        => TaskExecutorBuilder
            .AddConstructorParam(PluginFactory.GetType<ILoggerFactory>(), PluginFactory.GetType<IUnitOfWorkManager>().GetNewUnitOfWork(), Clock)
            .Execute<RegisterLogAuditTaskExecutor>(model);

 


    /// <summary>
    /// Obtiene el perfil del usuario actual a partir del contexto
    /// </summary>
    /// <returns></returns>
    protected async Task<User> GetUserFromContextAsync()
        => (await UnitOfWork.UserRepository
            .GetByFirstOrDefaultAsync(
                where => where.Id == UserId,
                inlcude => inlcude.Image,
                include => include.UserRegistrationForms).ConfigureAwait(false))
            ?? throw new CustomException((int)MessagesCodesError.InfoUserNotFound, "El perfil del usuario: {userId} no se encuentra en la base de datos");

    /// <summary>
    /// Configura el Contexto
    /// </summary>
    /// <returns></returns>
    protected void SetContextRequest(ContextRequest request)
    {
        ContextRequest = request;
        CommonContextRequest = request;
    }

    /// <summary>
    /// Configura en el contexto los nombres de usuario y el id
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="userName"></param>
    protected void ConfigureUserIdAndUserNameInContext(int userId, string userName)
        => ContextRequest.CustomClaims.ConfigureUser(userId, userName);

    /// <summary>
    /// Consigue las configuraciones jwt mediante el canal.
    /// </summary>
    /// <param name="channel"></param>
    /// <returns></returns>
    protected JwtSetting GetJwtSettings()
       => AppSettingsApi.JwtSettings.Find(t => t.Identifier == ContextRequest.Headers.Channel.ToString());

    /// <summary>
    /// Obtiene los catálogos desde un archivo
    /// </summary>
    /// <param name="catalogCode"></param>
    /// <returns></returns>
    protected async Task<CatalogCodes> GetCatalogCodesResponseByFile(string catalogCode)
    {
        var itemsCatalogDictionary = (await Mediator.Send(new GetItemsCatalogByCodeCatalogFileRequest(catalogCode, $"{ContextRequest.Headers.UserLanguage}", ContextRequest), CancellationToken.None).ConfigureAwait(false))
            ?.ItemCatalogs.Select(t => new KeyValuePair<string, string>(t.Code, t.Value))
            .ToDictionary(x => x.Key, x => x.Value);
        return new CatalogCodes($"{catalogCode}", itemsCatalogDictionary);
    }

    /// <summary>
    /// Obtiene los catálogos desde un archivo
    /// </summary>
    /// <param name="catalogCode"></param>
    /// <returns></returns>
    protected async Task<IEnumerable<ItemCatalogFileResponse>> GetItemsCatalogCodesResponseByFile(string catalogCode)
        => (await Mediator.Send(new GetItemsCatalogByCodeCatalogFileRequest(catalogCode, $"{ContextRequest.Headers.UserLanguage}", ContextRequest), CancellationToken.None).ConfigureAwait(false))
            ?.ItemCatalogs;

    /// <summary>
    /// Valida un Item de Catálogo que exista en un Catálogo
    /// </summary>
    /// <param name="itemCode"></param>
    /// <param name="catalogCode"></param>
    /// <returns></returns>
    protected async Task ValidateItemCodeInCatalog(string itemCode, string catalogCode)
    {
        var catalogCodes = await GetCatalogCodesResponseByFile(catalogCode).ConfigureAwait(false);
        if (!catalogCodes.ItemsCatalog.Any(t => t.Key == itemCode))
            throw new CustomException((int)MessagesCodesError.SystemError, $"No existe ningún item de catálogo con el código '{itemCode}', en el Catálogo '{catalogCode}'");
    }

    /// <summary>
    /// Obtiene un mensáje de éxito
    /// </summary>
    /// <param name="messagesCodesSucess"></param>
    /// <returns></returns>
    protected string GetSuccessMessage(MessagesCodesSucess messagesCodesSucess = MessagesCodesSucess.Ok)
    => UserMessages.GetSucessMessageByCode((int)messagesCodesSucess, ContextRequest.Headers.UserLanguage)
        ?.Message;

    /// <summary>
    /// Encriptar Dato 
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    protected async Task<string> EncryptedValueAsync(string value, bool applyEncode = false)
        => (await Mediator.Send(new EncryptRequest(value, ContextRequest, applyEncode)).ConfigureAwait(false))
            ?.DictionaryEncrypted?.FirstOrDefault().Value;

    /// <summary>
    /// Desencripta un texto
    /// </summary>
    /// <param name="textEncrypt"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected async Task<string> DecryptValueAsync(string textEncrypt, bool hasEncode = false, CancellationToken cancellationToken = default) =>
    //Envía a desencriptar el usuario
    (await Mediator.Send(new DecryptRequest(textEncrypt, ContextRequest, hasEncode), cancellationToken).ConfigureAwait(false))
      ?.DictionaryTextsDecrypt.FirstOrDefault().Value;

    /// <summary>
    /// Obtiene un parámetro de la base de datos en Int
    /// </summary>
    /// <param name="parameterCode"></param>
    /// <returns></returns>
    protected async Task<int> GetIntParameterAsync(string parameterCode)
        => (await Mediator.Send(new GetParameterByCodeRequest(parameterCode, ContextRequest)).ConfigureAwait(false)).IntValue;

    /// <summary>
    /// Valida si existe un valor en un catálogo
    /// </summary>
    /// <param name="catalogsTypeItemsCodes"></param>
    /// <param name="valueToValidate"></param>
    /// <returns></returns>
    protected async Task ValidateExistValueInFileCatalogAsync(EnumLogicApi.CatalogsTypeItemsCodes catalogsTypeItemsCodes, string valueToValidate)
    {
        var itemsCatalogs = (await Mediator.Send(new GetInitialCataloguesRequest(ContextRequest)).ConfigureAwait(false))
            ?.ListCatalogCodes?.FirstOrDefault(first => first.Catalog == catalogsTypeItemsCodes.GetEnumMember())
            ?.ItemsCatalog;
        if (!itemsCatalogs.Any(any => any.Key == valueToValidate))
            throw new CustomException((int)MessagesCodesError.ItemCatalogNotFound, $"El Item de Catálogo: '{valueToValidate}' del Catálogo: '{catalogsTypeItemsCodes.GetEnumMember()}' no fué encontrado.");
    }

    /// <summary>
    /// Obtiene la información de Persona
    /// </summary>
    /// <param name="documentNumber"></param>
    /// <returns></returns>
    protected async Task<VerifyDocumentResponse> GetPersonInformationAsync(string documentNumber, bool throwIfException = true)
    {
        try
        {
            return await DocumentationServices.GetPersonInformationAsync(new(documentNumber)).ConfigureAwait(false);
        }
        catch
        {
            if (throwIfException)
                throw new CustomException((int)MessagesCodesError.PersonInformationNotFound, $"No se pudo encontrar información del documento: '{documentNumber}'");
            return null;
        }
    }


    /// <summary>
    /// Retorna respuesta exitosa
    /// </summary>
    /// <param name="messagesCodesSucess"></param>
    /// <returns></returns>
    protected HandlerResponse SuccessMessage(MessagesCodesSucess messagesCodesSucess = MessagesCodesSucess.Ok) => HandlerResponse.Complete(GetSuccessMessage(messagesCodesSucess), true);
}
