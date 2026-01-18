using Common.WebApi.Models.AppSettingsModel;
using Common.WebApi.Models.ContextRequestModel;
using Common.WebCommon.Models;
using Common.WebCommon.Models.Enum;
using LogicCommon.BusinessLogic;
using LogicCommon.Model.CacheModel;
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
    protected Guid CurrentUserGuid => ContextRequest?.CustomClaims?.UserGuid ?? throw NullException.ThrowNullException(nameof(ContextRequest.CustomClaims.UserGuid));
    protected UserLanguage CurrentUserLanguage => ContextRequest?.Headers?.UserLanguage ?? throw NullException.ThrowNullException(nameof(ContextRequest.Headers.UserLanguage));
    protected int CurrentUserId => ContextRequest?.CustomClaims.UserId ?? throw NullException.ThrowNullException(nameof(ContextRequest.CustomClaims.UserId));
    protected List<GymRoleContextClaim> CurrentUserRoles => ContextRequest?.CustomClaims.InformationRoles ?? throw NullException.ThrowNullException(nameof(ContextRequest.CustomClaims.InformationRoles));

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
        //Ejecuta el proceso
        var result = await process().ConfigureAwait(false);
        return result;
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

    /// <summary>
    /// Obtiene un mensaje de éxito
    /// </summary>
    /// <param name="messagesCodesSucess"></param>
    /// <returns></returns>
    protected string GetSuccessMessage(MessagesCodesSucess messagesCodesSucess = MessagesCodesSucess.Ok)
        => UserMessages.GetSucessMessageByCode((int)messagesCodesSucess, (Common.Messages.Models.UserLanguage)CurrentUserLanguage)
            ?.Message;

    /// <summary>
    /// Obtiene los IDs de las sucursales del contexto
    /// </summary>
    /// <returns></returns>
    protected async Task<List<int>> GetBranchsIdsByContextAsync()
    {
        var gymCacheInformation = await GetGymCacheInformationAsync().ConfigureAwait(false);
        var contextInformationRoles = ContextRequest.CustomClaims.InformationRoles;
        var branchsResponseIds = gymCacheInformation.Select(select => select.GymBranchId);
        if (contextInformationRoles.Any(where => where.Scope != (byte)RoleScope.Global))
        {
            var gymsIds = contextInformationRoles.Where(where => where.Scope == (byte)RoleScope.Gym).Select(select => select.Identifier).ToList();
            branchsResponseIds = [.. gymCacheInformation.Where(where => gymsIds.Contains(where.GymId)).Select(select => select.GymBranchId)];
            var branchsIds = contextInformationRoles.Where(where => where.Scope == (byte)RoleScope.GymBranch).Select(select => select.Identifier.Value).ToList();
            branchsResponseIds = [.. branchsResponseIds.Concat(branchsIds)];
        }
        return [.. branchsResponseIds.Distinct()];
    }

    /// <summary>
    /// Obtiene los IDs de los gimnasios del contexto
    /// </summary>
    /// <returns></returns>
    protected async Task<List<int>> GetGymsIdsByContextAsync()
    {
        var gymCacheInformation = await GetGymCacheInformationAsync().ConfigureAwait(false);
        var contextInformationRoles = ContextRequest.CustomClaims.InformationRoles;
        var gymResponseIds = gymCacheInformation.Select(select => select.GymBranchId);
        if (contextInformationRoles.Any(where => where.Scope != (byte)RoleScope.Global))
        {
            var branchsIds = contextInformationRoles.Where(where => where.Scope == (byte)RoleScope.GymBranch).Select(select => select.Identifier.Value).ToList();
            gymResponseIds = gymCacheInformation.Where(where => branchsIds.Contains(where.GymBranchId)).Select(select => select.GymId);
        }
        return [.. gymResponseIds.Distinct()];
    }


    /// <summary>
    /// Obtiene la información del gimnasio en caché
    /// </summary>
    /// <param name="gymId"></param>
    /// <returns></returns>
    protected async Task<List<GymCacheInformation>> GetGymCacheInformationAsync()
        => await AdministratorCache.TryGetOrSetAsync(CacheCodes.GYM_INFORMATION, async () => await UnitOfWork.GymBranchRepository.GetGenericAsync(
            select => new GymCacheInformation
            {
                GymId = select.Gym.Id,
                GymGuid = select.Gym.Guid,
                GymName = select.Gym.Name,
                GymBranchId = select.Id,
                GymBranchGuid = select.Guid,
                GymBranchName = select.Name,
            },
            where => where.Gym.IsActive == GymStatus.Active
        ).ConfigureAwait(false)).ConfigureAwait(false);

}
