using System.Linq.Expressions;
using LogicAdministratorApi.Model.Request.UserClient;
using LogicAdministratorApi.Model.Response.UserClient;
using PersistenceDb.Models.Authentication;

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
                // Construir el filtro where combinando todas las condiciones
                var emailFilter = request.EmailFilter?.ToLower();
                var userNameFilter = request.UserNameFilter?.ToLower();
                var mobilePlatformId = (await GetPlatformsAsync().ConfigureAwait(false))
                    .Find(where => where.Code == RolePlatformType.Mobile.GetEnumMember())?.Id;
                
                Expression<Func<User, bool>> whereClause = u =>
                    (string.IsNullOrWhiteSpace(emailFilter) || u.Email.ToLower().Contains(emailFilter)) &&
                    (string.IsNullOrWhiteSpace(userNameFilter) || u.UserName.ToLower().Contains(userNameFilter)) &&
                    (request.IsBlockedFilter == null || u.IsBlocked == request.IsBlockedFilter);

                // Obtener datos paginados
                var paginatedResult = await UnitOfWork.UserRepository
                    .GetPaginatorGenericAsync(
                        itemsByPage: request.PageSize,
                        page: request.PageNumber,
                        select => new ClientItem
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

                return new GetUsersClientResponse(paginatedResult.TotalItems, paginatedResult.Items)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            }
        ).ConfigureAwait(false);
}

