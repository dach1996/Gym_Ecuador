using LogicAdministratorApi.Model.Request.UserAdministrator;
using LogicAdministratorApi.Model.Response.UserAdministrator;

namespace LogicAdministratorApi.BusinessLogic.UserAdministratorHandler;

/// <summary>
/// Handler para obtener detalle de usuario administrador por GUID
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetUserAdministratorByGuidHandler(
    ILogger<GetUserAdministratorByGuidHandler> logger,
    IPluginFactory pluginFactory) : UserAdministratorBase<GetUserAdministratorByGuidRequest, GetUserAdministratorByGuidResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de detalle de un usuario por GUID
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetUserAdministratorByGuidResponse> Handle(GetUserAdministratorByGuidRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.GetUserAdministratorByGuid, request, async () =>
            {
                var webPlatformId = (await GetPlatformsAsync().ConfigureAwait(false))
                    .Find(where => where.Code == RolePlatformType.Web.GetEnumMember())?.Id;
                // Buscar el usuario por GUID usando GetFirstOrDefaultGenericAsync con proyección directa
                var user = await UnitOfWork.UserRepository
                    .GetFirstOrDefaultGenericAsync(
                        select => new AdministratorUserDetail
                        {
                            Guid = select.Guid,
                            Id = select.Id,
                            UserName = select.UserName,
                            Email = select.Email,
                            Phone = select.Phone,
                            IsBlocked = select.IsBlocked,
                            PersonName = select.Person.FullName,
                            DateTimeRegister = select.DateTimeRegister,
                            UserRoleScopes = select.UserRoleScopes
                            .Select(urs => new AdministratorUserRoleScopeItem
                            {
                                Guid = urs.Role.Guid,
                                Name = urs.Role.Name
                            }).ToList()
                        },
                        where => where.Guid == request.UserGuid && where.UserRoleScopes.Any(urs => urs.Role.PlatformId == webPlatformId)
                    ).ConfigureAwait(false)
                    ?? throw new CustomException((int)MessagesCodesError.SystemError, "Usuario no encontrado");

                return new GetUserAdministratorByGuidResponse(user)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            }
        ).ConfigureAwait(false);
}

