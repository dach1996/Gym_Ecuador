using LogicAdministratorApi.Model.Request.Role;
using LogicAdministratorApi.Model.Response.Role;

namespace LogicAdministratorApi.BusinessLogic.RoleHandler;

/// <summary>
/// Handler para obtener todos los roles
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetRolesHandler(
    ILogger<GetRolesHandler> logger,
    IPluginFactory pluginFactory) : RoleBase<GetRolesRequest, GetRolesResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de todos los roles
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetRolesResponse> Handle(GetRolesRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.GetRoles, request, async () =>
        {
            var roles = await UnitOfWork.RoleRepository.GetGenericAsync(
                select => new RoleItem
                {
                    Guid = select.Guid,
                    Name = select.Code,
                    Description = select.Name,
                    ScopeCode = select.Scope.ToString(),
                    PlatformName = select.Platform.Name
                }
            ).ConfigureAwait(false);

            return new GetRolesResponse
            {
                Roles = roles,
                UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                ShowMessage = false
            };
        }).ConfigureAwait(false);
}
