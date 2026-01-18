using LogicAdministratorApi.Model.Request.UserClient;
using LogicAdministratorApi.Model.Response.UserClient;

namespace LogicAdministratorApi.BusinessLogic.UserClientHandler;

/// <summary>
/// Handler para obtener detalle de usuario cliente por GUID
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetUserClientByGuidHandler(
    ILogger<GetUserClientByGuidHandler> logger,
    IPluginFactory pluginFactory) : UserClientBase<GetUserClientByGuidRequest, GetUserClientByGuidResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de detalle de un usuario cliente por GUID
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetUserClientByGuidResponse> Handle(GetUserClientByGuidRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.GetUserClientByGuid, request, async () =>
            {
                // Buscar el cliente por GUID usando GetFirstOrDefaultGenericAsync con proyección directa
                var client = await UnitOfWork.ClientGymBranchRepository
                    .GetFirstOrDefaultGenericAsync(
                        select => new UserClientDetail
                        {
                            Guid = select.Guid,
                            RegistrationDate = select.RegistrationDate,
                            Status = select.Status,
                            UserGuid = select.User.Guid,
                            UserName = select.User.UserName,
                            Email = select.User.Email,
                            Phone = select.User.Phone,
                            LanguageCode = select.User.LanguageCode,
                            IsBlocked = select.User.IsBlocked,
                            HasCompleteRegistration = select.User.HasCompleteRegistration,
                            DateTimeRegister = select.User.DateTimeRegister,
                            FirstLoginDate = select.User.FirstLoginDate,
                            ImageUrl = select.User.Image != null && select.User.Image.State && select.User.Image.FileBasePath != null
                                ? select.User.Image.FileBasePath.BaseUrl + select.User.Image.Path
                                : null,
                            PersonGuid = select.User.Person != null ? select.User.Person.Guid : Guid.Empty,
                            PersonName = select.User.Person != null ? select.User.Person.RealNames : null,
                            PersonLastName = select.User.Person != null ? select.User.Person.RealLastNames : null,
                            PersonDocumentNumber = select.User.Person != null ? select.User.Person.DocumentNumber : null,
                            PersonBirthDate = select.User.Person != null ? select.User.Person.BirthDate : null,
                            GymBranchGuid = select.GymBranch.Guid,
                            GymBranchName = select.GymBranch.Name,
                            GymGuid = select.GymBranch.Gym.Guid,
                            GymName = select.GymBranch.Gym.Name,
                            Memberships = select.ClientMemberships
                                .Select(cm => new ClientMembershipDetail
                                {
                                    Guid = cm.Guid,
                                    BranchPlanGuid = cm.BranchPlan.Guid,
                                    PlanName = cm.BranchPlan.Name,
                                    StartDate = cm.StartDate,
                                    EndDate = cm.EndDate,
                                    IsActive = cm.IsActive,
                                    RegistrationDate = cm.RegistrationDate
                                })
                                .ToList()
                        },
                        where => where.Guid == request.ClientGuid
                    ).ConfigureAwait(false)
                    ?? throw new CustomException((int)MessagesCodesError.SystemError, "Cliente no encontrado");

                return new GetUserClientByGuidResponse(client)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            }
        ).ConfigureAwait(false);
}
