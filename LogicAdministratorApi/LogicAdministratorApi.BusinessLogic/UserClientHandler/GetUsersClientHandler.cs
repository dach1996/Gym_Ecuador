using System.Linq.Expressions;
using LogicAdministratorApi.Model.Request.UserClient;
using LogicAdministratorApi.Model.Response.UserClient;
using PersistenceDb.Models.Authentication;
using PersistenceDb.Models.Core;

namespace LogicAdministratorApi.BusinessLogic.UserClientHandler;

/// <summary>
/// Handler para obtener usuarios clientes paginados
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetUsersClientHandler(
    ILogger<GetUsersClientHandler> logger,
    IPluginFactory pluginFactory) : UserClientBase<GetUsersClientRequest, GetUsersClientResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de usuarios clientes con paginación
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetUsersClientResponse> Handle(GetUsersClientRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.GetUserClientsPaginated, request, async () =>
            {
                var filter = request.Filter?.ToLower();
                var whereClause = new List<Expression<Func<Person, bool>>>
                {
                       {!request.Filter.IsNullOrEmpty(),
                        where => where.Users.Any(u => u.Email.ToLower().Contains(filter)||
                        u.UserName.ToLower().Contains(request.Filter)||
                        u.Person.DocumentNumber.ToLower().Contains(filter)||
                        u.Phone.ToLower().Contains(filter)||
                        u.Email.ToLower().Contains(filter)||
                        u.Phone.ToLower().Contains(filter)||
                        u.Person.Name.ToLower().Contains(filter)||
                        u.Person.LastName.ToLower().Contains(filter))},
                    { where => where.ClientGymBranches.Any()},
                    {request.GymGuid.HasValue, where => where.ClientGymBranches.Any(cg => cg.GymBranch.Gym.Guid == request.GymGuid.Value)},
                    {request.GymBranchGuid.HasValue, where => where.ClientGymBranches.Any(cg => cg.GymBranch.Guid == request.GymBranchGuid.Value)},
                };
                // Obtener datos paginados
                var paginatedResult = await UnitOfWork.PersonRepository
                    .GetPaginatorGenericAsync(
                        itemsByPage: request.PageSize,
                        page: request.PageNumber,
                        select => new ClientItem
                        {
                            Id = select.Id.ToString(),
                            Guid = select.Guid,
                            PersonName = select.Name + " " + select.LastName,
                            Email = select.Users.FirstOrDefault().Email,
                            Phone = select.Users.FirstOrDefault().Phone,
                            MembershipsItems = select.ClientGymBranches.Select(cg => new ClientMembershipItem
                            {
                                GymName = cg.GymBranch.Gym.Name,
                                GymGuid = cg.GymBranch.Gym.Guid,
                                GymBranchName = cg.GymBranch.Name,
                                GymBranchGuid = cg.GymBranch.Guid,
                                Status = cg.Status,
                                RegistrationDate = cg.RegistrationDate,
                                StartDate = cg.ClientMemberships.FirstOrDefault().StartDate,
                                EndDate = cg.ClientMemberships.FirstOrDefault().EndDate
                            }).ToList()
                        },
                        where: whereClause,
                        orderBy: u => u.Id,
                        orderByType: OrderByType.Desc
                    ).ConfigureAwait(false);

                var items = paginatedResult.Items.Select(item =>
                {
                    var membershipsItems = item.MembershipsItems;
                    if (request.GymGuid.HasValue)
                        membershipsItems = [.. membershipsItems.Where(m => m.GymGuid == request.GymGuid.Value)];
                    if (request.GymBranchGuid.HasValue)
                        membershipsItems = [.. membershipsItems.Where(m => m.GymBranchGuid == request.GymBranchGuid.Value)];
                    item.MembershipsItems = membershipsItems;
                    return item;
                });

                return new GetUsersClientResponse(paginatedResult.TotalItems, items)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            }
        ).ConfigureAwait(false);
}

