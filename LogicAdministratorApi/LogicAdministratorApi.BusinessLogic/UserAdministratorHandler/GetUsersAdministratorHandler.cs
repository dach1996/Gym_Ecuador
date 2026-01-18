using System.Linq.Expressions;
using LogicAdministratorApi.Model.Request.UserAdministrator;
using LogicAdministratorApi.Model.Response.UserAdministrator;
using PersistenceDb.Models.Authentication;

namespace LogicAdministratorApi.BusinessLogic.UserAdministratorHandler;

/// <summary>
/// Handler para obtener usuarios administradores paginados
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetUsersAdministratorHandler(
    ILogger<GetUsersAdministratorHandler> logger,
    IPluginFactory pluginFactory) : UserAdministratorBase<GetUsersAdministratorRequest, GetUsersAdministratorResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de usuarios administradores con paginación
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetUsersAdministratorResponse> Handle(GetUsersAdministratorRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.GetUsersAdministratorPaginated, request, async () =>
            {
                // Construir el filtro where combinando todas las condiciones
                var filter = request.Filter?.ToLower();
                var webPlatformId = (await GetPlatformsAsync().ConfigureAwait(false))
                .Find(where => where.Code == RolePlatformType.Web.GetEnumMember())?.Id;
                List<Expression<Func<User, bool>>> whereClause = new List<Expression<Func<User, bool>>>
                {
                    u => u.UserRoleScopes.Any(ur => ur.Role.PlatformId == webPlatformId),
                    { !request.Filter.IsNullOrEmpty(), where =>
                    where.UserName.ToLower().Contains(filter) ||
                    where.Email.ToLower().Contains(filter) ||
                    where.Phone.ToLower().Contains(filter) ||
                    where.Person.RealNames.ToLower().Contains(filter) }
                };

                // Obtener datos paginados
                var paginatedResult = await UnitOfWork.UserRepository
                    .GetPaginatorGenericAsync(
                        itemsByPage: request.PageSize,
                        page: request.PageNumber,
                        select => new AdministratorUserItem
                        {
                            Guid = select.Guid,
                            UserName = select.UserName,
                            Email = select.Email,
                            Phone = select.Phone,
                            PersonName = select.Person.FullName,
                            Id = select.Id,
                            DateTimeRegister = select.DateTimeRegister,
                            IsBlocked = select.IsBlocked,
                            UserRoleScopes = select.UserRoleScopes.Select(urs => new AdministratorUserRoleScopeItem
                            {
                                Guid = urs.Role.Guid,
                                Name = urs.Role.Name
                            }).ToList()
                        },
                        where: whereClause,
                        orderBy: u => u.Id,
                        orderByType: OrderByType.Desc
                    ).ConfigureAwait(false);

                return new GetUsersAdministratorResponse(paginatedResult.TotalItems, paginatedResult.Items)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            }
        ).ConfigureAwait(false);
}

