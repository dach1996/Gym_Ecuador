using LogicAdministratorApi.Model.Request.UserAdministrator;
using LogicAdministratorApi.Model.Response.UserAdministrator;
using PersistenceDb.Models.Authentication;

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
                // Buscar el usuario por GUID usando GetFirstOrDefaultGenericAsync con proyección directa
                var user = await UnitOfWork.UserRepository
                    .GetFirstOrDefaultGenericAsync(
                        select => new UserDetail
                        {
                            Guid = select.Guid,
                            UserName = select.UserName,
                            Email = select.Email,
                            Phone = select.Phone,
                            LanguageCode = select.LanguageCode,
                            IsBlocked = select.IsBlocked,
                            HasCompleteRegistration = select.HasCompleteRegistration,
                            DateTimeRegister = select.DateTimeRegister,
                            FirstLoginDate = select.FirstLoginDate,
                            ImageUrl = select.Image != null && select.Image.State && select.Image.FileBasePath != null
                                ? select.Image.FileBasePath.BaseUrl + select.Image.Path
                                : null,
                            RoleGuids = select.UserRoleScopes != null
                                ? select.UserRoleScopes
                                    .Where(urs => urs.Role != null)
                                    .Select(urs => urs.Role.Guid)
                                    .Where(guid => guid != Guid.Empty)
                                    .ToList()
                                : new List<Guid>()
                        },
                        where => where.Guid == request.UserGuid
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

