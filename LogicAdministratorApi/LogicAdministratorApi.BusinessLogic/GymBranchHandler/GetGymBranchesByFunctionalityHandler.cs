using Common.WebApi.Attributes.EnumFunctionality;
using Common.WebApi.Models.Enum;
using LogicAdministratorApi.Model.Request.GymBranch;
using LogicAdministratorApi.Model.Response.GymBranch;
using LogicCommon.Model.CacheModel;
using PersistenceDb.Models.Enums;

namespace LogicAdministratorApi.BusinessLogic.GymBranchHandler;

/// <summary>
/// Handler para obtener sucursales de gimnasio por funcionalidad
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetGymBranchesByFunctionalityHandler(
    ILogger<GetGymBranchesByFunctionalityHandler> logger,
    IPluginFactory pluginFactory) : GymBranchBase<GetGymBranchesByFunctionalityRequest, GetGymBranchesByFunctionalityResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de sucursales de gimnasio filtradas por funcionalidad
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetGymBranchesByFunctionalityResponse> Handle(GetGymBranchesByFunctionalityRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.GetGymBranchesByFunctionality, request, async () =>
        {
            var gymCacheInformation = await GetGymCacheInformationAsync().ConfigureAwait(false);
            var branches = new List<GymCacheInformation>();
            if (CurrentUserRoles.Any(where => where.Scope == (byte)RoleScope.GymBranch))
                branches = [.. branches.Concat(gymCacheInformation.Where(where => CurrentUserRoles.Any(role => role.Identifier == where.GymBranchId)))];
            if (CurrentUserRoles.Any(where => where.Scope == (byte)RoleScope.Gym))
                branches = [.. branches.Concat(gymCacheInformation.Where(where => CurrentUserRoles.Any(role => role.Identifier == where.GymId)))];
            if (CurrentUserRoles.Any(where => where.Scope == (byte)RoleScope.Global))
                branches = [.. branches.Concat(gymCacheInformation)];
            var branchesResponse = branches.Distinct().GroupBy(select => select.GymGuid).Select(select => new GymByFunctionalityItem
            {
                Guid = select.Key,
                GymName = select.First().GymName,
                Branches = select.Select(b => new GymByFunctionalityItem.GymBranchByFunctionalityItem
                {
                    Guid = b.GymBranchGuid,
                    Name = b.GymBranchName,
                }),
            });
            return new GetGymBranchesByFunctionalityResponse
            {
                UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                ShowMessage = false,
                Branches = branchesResponse
            };
        }).ConfigureAwait(false);
}
