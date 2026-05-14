using System.Linq.Expressions;
using LogicAdministratorApi.Model.Request.ClientMembership;
using LogicAdministratorApi.Model.Response.ClientMembership;
using PersistenceDb.Models.Core;

namespace LogicAdministratorApi.BusinessLogic.ClientMembershipHandler;

/// <summary>
/// Handler para obtener membresías de clientes paginadas
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetClientMembershipsHandler(
    ILogger<GetClientMembershipsHandler> logger,
    IPluginFactory pluginFactory) : ClientMembershipBase<GetClientMembershipsRequest, GetClientMembershipsResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de membresías de clientes con paginación
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetClientMembershipsResponse> Handle(GetClientMembershipsRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.GetClientMembershipsPaginated, request, async () =>
            {
                var whereClause = new List<Expression<Func<ClientGymBranch, bool>>>
                    {
                        { where => where.Person.Guid == request.PersonGuid },
                        { request.GymGuid.HasValue, where => where.GymBranch.Gym.Guid == request.GymGuid.Value },
                        { request.GymBranchGuid.HasValue, where => where.GymBranch.Guid == request.GymBranchGuid.Value },
                    };

                // Obtener datos paginados
                var clientGymBranch = await UnitOfWork.ClientGymBranchRepository
                    .GetFirstOrDefaultGenericAsync(
                        select => new
                        {
                            PersonInfo = new
                            {
                                PersonName = select.Person.Name + " " + select.Person.LastName,
                                PersonDocumentNumber = select.Person.DocumentNumber,
                                UserInfo = select.Person.Users.Select(u => new
                                {
                                    UserEmail = u.Email,
                                    UserPhone = u.Phone,
                                }).FirstOrDefault(),
                                PersonBirthDate = select.Person.BirthDate,
                            },
                            Subscriptions = select.ClientMemberships
                            .Select(cm => new
                            {
                                cm.RegistrationDate,
                                Subscription = new CurrentSubscription
                                {
                                    MembershipGuid = cm.Guid,
                                    StartDate = cm.StartDate,
                                    EndDate = cm.EndDate,
                                    IsActive = cm.IsActive,
                                    PlanName = cm.BranchPlan.Name,
                                    SubscriptionValue = cm.BranchPlan.Price,
                                    PaymentMethod = "Tarjeta",
                                }
                            }),
                        },
                        whereClause
                    ).ConfigureAwait(false);
                var suscriptions = clientGymBranch?.Subscriptions?.OrderByDescending(s => s.RegistrationDate).Select(s => s.Subscription);
                return new GetClientMembershipsResponse
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false,
                    Item = suscriptions != null ? new ClientMembershipInformation
                    {
                        PersonName = clientGymBranch.PersonInfo.PersonName,
                        PersonDocumentNumber = clientGymBranch.PersonInfo.PersonDocumentNumber,
                        PersonBirthDate = clientGymBranch.PersonInfo.PersonBirthDate,
                        Email = clientGymBranch.PersonInfo.UserInfo?.UserEmail,
                        Phone = clientGymBranch.PersonInfo.UserInfo?.UserPhone,
                        Status = suscriptions.Any(s => s.IsActive),
                        FirstSubscriptionDate = suscriptions.LastOrDefault().StartDate,
                        Registers = suscriptions,
                    }
                    : null
                };
            }
        ).ConfigureAwait(false);
}
