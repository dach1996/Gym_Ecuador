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
                var emailFilter = request.EmailFilter?.ToLower();
                var userNameFilter = request.UserNameFilter?.ToLower();
                var webPlatformId = (await GetPlatformsAsync().ConfigureAwait(false))
                .Find(where => where.Code == RolePlatformType.Web.GetEnumMember())?.Id;
                Expression<Func<User, bool>> whereClause = u =>
                    (string.IsNullOrWhiteSpace(emailFilter) || u.Email.ToLower().Contains(emailFilter)) &&
                    (string.IsNullOrWhiteSpace(userNameFilter) || u.UserName.ToLower().Contains(userNameFilter)) &&
                    u.UserRoleScopes.Any(ur => ur.Role.PlatformId == webPlatformId) ;

                // Obtener datos paginados
                var paginatedResult = await UnitOfWork.UserRepository
                    .GetPaginatorGenericAsync(
                        itemsByPage: request.PageSize,
                        page: request.PageNumber,
                        select => new UserItem
                        {
                            Guid = select.Guid,
                            UserName = select.UserName,
                            Email = select.Email,
                            Phone = select.Phone,
                            LanguageCode = select.LanguageCode,
                            IsBlocked = select.IsBlocked,
                            HasCompleteRegistration = select.HasCompleteRegistration,
                            DateTimeRegister = select.DateTimeRegister,
                            FirstLoginDate = select.FirstLoginDate
                        },
                        where: whereClause,
                        orderBy: u => u.UserName,
                        orderByType: OrderByType.Asc
                    ).ConfigureAwait(false);

                return new GetUsersAdministratorResponse(paginatedResult.TotalItems, paginatedResult.Items)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            }
        ).ConfigureAwait(false);
}

