using Common.Utils.Extensions;
using MediatR;
using PersistenceDb.Models.Authentication;

namespace LogicAdministratorApi.BusinessLogic.NotificationPushHandler;

/// <summary>
/// Clase base para handlers de notificaciones push
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class NotificationPushBase<TRequest, TResponse>(
    ILogger<NotificationPushBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicAdministratorBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Convierte UserGuids a UserIds
    /// </summary>
    /// <param name="userGuids"></param>
    /// <returns></returns>
    protected async Task<List<int>> ConvertUserGuidsToUserIdsAsync(IEnumerable<Guid> userGuids)
    {
        var userGuidsList = userGuids.ToList();
        if (userGuidsList.IsNullOrEmpty())
            return new List<int>();

        var users = await UnitOfWork.UserRepository
            .GetGenericAsync(
                select => new { select.Id, select.Guid },
                where => userGuidsList.Contains(where.Guid)
            ).ConfigureAwait(false);

        return users.Select(u => u.Id).ToList();
    }

    /// <summary>
    /// Obtiene el mapeo de UserId a UserGuid
    /// </summary>
    /// <param name="userIds"></param>
    /// <returns></returns>
    protected async Task<Dictionary<int, Guid>> GetUserIdToGuidMapAsync(IEnumerable<int> userIds)
    {
        var userIdsList = userIds.ToList();
        if (userIdsList.IsNullOrEmpty())
            return new Dictionary<int, Guid>();

        var users = await UnitOfWork.UserRepository
            .GetGenericAsync(
                select => new { select.Id, select.Guid },
                where => userIdsList.Contains(where.Id)
            ).ConfigureAwait(false);

        return users.ToDictionary(u => u.Id, u => u.Guid);
    }
}

