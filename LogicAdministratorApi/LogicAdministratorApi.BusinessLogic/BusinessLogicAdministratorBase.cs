
using Common.Tasks;
using Common.WebApi.Models.AppSettingsModel;
using Common.WebApi.Models.ContextRequestModel;
using Common.WebCommon.Models;
using Common.WebCommon.Models.Enum;
using LogicAdministratorApi.Model.CommonModels;
using LogicCommon.BusinessLogic;
namespace LogicAdministratorApi.BusinessLogic;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class BusinessLogicAdministratorBase(
    ILogger<BusinessLogicAdministratorBase> logger,
    IPluginFactory pluginFactory) : BusinessLogicCommonBase(logger, pluginFactory)
{
    private IUserMessages _userMessages;
    protected IUserMessages UserMessages => _userMessages ??= PluginFactory.GetType<IUserMessages>();
    private AppSettingsAdministrator _appSettingsAdministrator;
    protected AppSettingsAdministrator AppSettingsAdministrator => _appSettingsAdministrator ??= PluginFactory.GetType<AppSettingsAdministrator>();
    protected AdminContextRequest ContextRequest;
    protected DateTime CurrentUserInformationCacheDateTimeCreation => ContextRequest?.CustomClaims?.UserInformationCacheDateTimeCreation ?? throw NullException.ThrowNullException(nameof(ContextRequest.CustomClaims.UserInformationCacheDateTimeCreation));
    protected Guid CurrentUserGuid => ContextRequest?.CustomClaims?.UserGuid ?? throw NullException.ThrowNullException(nameof(ContextRequest.CustomClaims.UserGuid));
    protected Guid CurrentEstablishmentBranchGuid => ContextRequest?.CustomClaims?.EstablishmentBranchGuid ?? throw NullException.ThrowNullException(nameof(ContextRequest.CustomClaims.EstablishmentBranchGuid));
    protected int CurrentEstablishmentId => ContextRequest?.CustomClaims?.EstablishmentId ?? throw NullException.ThrowNullException(nameof(ContextRequest.CustomClaims.EstablishmentId));
    protected int CurrentEstablishmentBranchId => ContextRequest?.CustomClaims?.EstablishmentBranchId ?? throw NullException.ThrowNullException(nameof(ContextRequest.CustomClaims.EstablishmentBranchId));
    protected int CurrentVeterinarianId => ContextRequest?.CustomClaims?.VeterinarianId ?? throw NullException.ThrowNullException(nameof(ContextRequest.CustomClaims.VeterinarianId));
    protected UserLanguage CurrentUserLanguage => ContextRequest?.Headers?.UserLanguage ?? throw NullException.ThrowNullException(nameof(ContextRequest.Headers.UserLanguage));

    /// <summary>
    /// Ejecuta el proceso dentro de la configuración inicial
    /// </summary>
    /// <param name="operationName">Nombre de Operación</param>
    /// <param name="request">Request que implementa Clase Base</param>
    /// <param name="process">Proceso o Ejecución</param>
    /// <param name="unitOfWorks">Lista de Unit Of Work</param>
    /// <param name="verifyUserFunctionality">Verifica la funcionalidad que tenga el Usuario</param>
    /// <param name="registerLogAudit">Verifica si registra en Log de Auditoría</param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected async Task<T> ExecuteHandlerAsync<T>(
        OperationAdministratorName operationName,
        IApiBaseRequest request,
        Func<Task<T>> process,
        bool verifyUserFunctionality = true)
    {
        SetContextRequest(request.ContextRequest);
        try
        {
            //Ejecuta el proceso
            var result = await process().ConfigureAwait(false);

            return result;
        }
        catch (CustomException ex)
        {
            var message = $"{UserMessages.GetErrorMessageByCode(ex.Code).Message}" + (ex.Message.IsNullOrEmpty() ? $"Razón: {ex.Message}" : string.Empty);
            //  await ExecuteTaskCreateAuditLogAsync(RegisterLogAuditExecutorModel.ErrorRegister(operationName, request, message)).ConfigureAwait(false);
            throw;
        }
        catch (Exception ex)
        {
            //await ExecuteTaskCreateAuditLogAsync(RegisterLogAuditExecutorModel.ErrorRegister(operationName, request, ex.Message)).ConfigureAwait(false);
            throw;
        }
    }

    /* /// <summary>
    /// Ejecuta asincronamente el almacenado de un log
    /// </summary>
    /// <param name="model"></param>
    private async Task ExecuteTaskCreateAuditLogAsync(RegisterLogAuditExecutorModel model)
    {
        if (model.OperationName.GetRegisterAuditLog())
        {
            model.EstablishmentBranchName = await GetEstablishmentBranchNameByContextAsync().ConfigureAwait(false);
            PluginFactory.GetType<TaskExecutorBuilder>()
                    .AddConstructorParam(
                        PluginFactory.GetType<ILoggerFactory>(),
                        PluginFactory.GetType<IUnitOfWorkManager>().GetNewAdministratorUnitOfWork(),
                        Clock)
                    .Execute<RegisterLogAuditTaskExecutor>(model);
        }
    } */

    /// <summary>
    /// Ejecuta el proceso dentro de la configuración inicial con cache
    /// </summary>
    /// <param name="cacheKey"></param>
    /// <param name="operationName"></param>
    /// <param name="request"></param>
    /// <param name="process"></param>
    /// <param name="unitOfWorksTypes"></param>
    /// <param name="verifyUserFunctionality"></param>
    /// <param name="registerLogAudit"></param>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    protected async Task<T> ExecuteCacheHandlerAsync<T>(
        string cacheKey,
        OperationAdministratorName operationName,
        IApiBaseRequest request,
        Func<Task<T>> process,
        bool verifyUserFunctionality = true)
            => await AdministratorCache.TryGetOrSetAsync(cacheKey, () => ExecuteHandlerAsync<T>(operationName, request, process, verifyUserFunctionality)).ConfigureAwait(false);




    /// <summary>
    /// Configura el Contexto
    /// </summary>
    /// <returns></returns>
    protected void SetContextRequest(CommonContextRequest request)
    {
        ContextRequest = request as AdminContextRequest;
        CommonContextRequest = request;
    }
}
