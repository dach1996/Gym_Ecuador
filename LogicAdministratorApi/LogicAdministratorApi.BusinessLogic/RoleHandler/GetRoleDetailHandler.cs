using LogicAdministratorApi.Model.Request.Role;
using LogicAdministratorApi.Model.Response.Role;

namespace LogicAdministratorApi.BusinessLogic.RoleHandler;

/// <summary>
/// Handler para obtener detalle de rol por GUID
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetRoleDetailHandler(
    ILogger<GetRoleDetailHandler> logger,
    IPluginFactory pluginFactory) : RoleBase<GetRoleDetailRequest, GetRoleDetailResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de detalle de un rol por GUID
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetRoleDetailResponse> Handle(GetRoleDetailRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.GetRoleDetail, request, async () =>
        {
            // Buscar el rol por GUID usando GetFirstOrDefaultGenericAsync con proyección directa
            var role = await UnitOfWork.RoleRepository
                .GetFirstOrDefaultGenericAsync(
                    select => new RoleDetail
                    {
                        Guid = select.Guid,
                        Name = select.Code,
                        Description = select.Name,
                        ScopeCode = select.Scope.ToString(),
                        PlatformName = select.Platform.Name,
                        FunctionalityGuids = select.RoleFunctionalities != null
                            ? select.RoleFunctionalities
                                .Where(rf => rf.Functionality != null)
                                .Select(rf => rf.Functionality.Id)
                                .Where(guid => guid != Guid.Empty)
                                .ToList()
                            : new List<Guid>()
                    },
                    where => where.Guid == request.RoleGuid
                ).ConfigureAwait(false)
                ?? throw new CustomException((int)MessagesCodesError.SystemError, "Rol no encontrado");

            return new GetRoleDetailResponse(role)
            {
                UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                ShowMessage = false
            };
        }).ConfigureAwait(false);
}
