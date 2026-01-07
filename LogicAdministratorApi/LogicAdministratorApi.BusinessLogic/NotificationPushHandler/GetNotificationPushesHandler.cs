using System.Linq.Expressions;
using LogicAdministratorApi.Model.Request.NotificationPush;
using LogicAdministratorApi.Model.Response.NotificationPush;
using PersistenceDb.Models.Administration;
using PersistenceDb.Models.Enums;
using PersistenceDb.Repository.Models;
using EnumsDataBase = PersistenceDb.Models.Enums;

namespace LogicAdministratorApi.BusinessLogic.NotificationPushHandler;

/// <summary>
/// Handler para obtener notificaciones push enviadas de manera paginada
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetNotificationPushesHandler(
    ILogger<GetNotificationPushesHandler> logger,
    IPluginFactory pluginFactory) : NotificationPushBase<GetNotificationPushesRequest, GetNotificationPushesResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de notificaciones push con paginación
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetNotificationPushesResponse> Handle(GetNotificationPushesRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.GetNotificationPushesPaginated, request, async () =>
            {
                // Construir el filtro where combinando todas las condiciones
                var titleFilter = request.TitleFilter?.ToLower();
                var dateFrom = request.DateFrom;
                var dateTo = request.DateTo;

                Expression<Func<NotificationPush, bool>> whereClause = np =>
                    (string.IsNullOrWhiteSpace(titleFilter) || np.Title.ToLower().Contains(titleFilter)) &&
                    (dateFrom == null || np.RegisterDate >= dateFrom.Value) &&
                    (dateTo == null || np.RegisterDate <= dateTo.Value.AddDays(1).AddSeconds(-1));

                // Obtener datos paginados
                var paginatedResult = await UnitOfWork.NotificationPushRepository
                    .GetPaginatorGenericAsync(
                        itemsByPage: request.PageSize,
                        page: request.PageNumber,
                        select => new NotificationPushItem
                        {
                            Id = select.Id,
                            Title = select.Title,
                            Description = select.Description,
                            RegisterDate = select.RegisterDate,
                            PushNotificationType = select.PushNotificationType.ToString(),
                            PushNotificationValue = select.PushNotificationValue,
                            AllowViewUser = select.AllowViewUser
                        },
                        where: whereClause,
                        orderBy: np => np.RegisterDate,
                        orderByType: OrderByType.Desc
                    ).ConfigureAwait(false);

                // Obtener estadísticas de envío para cada notificación
                var notificationIds = paginatedResult.Items.Select(item => item.Id).ToList();
                
                if (notificationIds.Any())
                {
                    // Obtener usuarios de notificaciones push
                    var notificationPushUsers = await UnitOfWork.NotificationPushUserRepository
                        .GetGenericAsync(
                            select => new
                            {
                                select.Id,
                                select.PushNotificationId
                            },
                            where => notificationIds.Contains(where.PushNotificationId)
                        ).ConfigureAwait(false);

                    var notificationPushUserIds = notificationPushUsers.Select(npu => npu.Id).ToList();

                    // Obtener dispositivos de notificaciones push con sus estados
                    var notificationPushUserDevices = await UnitOfWork.NotificationPushUserDeviceRepository
                        .GetGenericAsync(
                            select => new
                            {
                                select.NotificationPushUserId,
                                select.StatusNotification
                            },
                            where => notificationPushUserIds.Contains(where.NotificationPushUserId)
                        ).ConfigureAwait(false);

                    // Agrupar estadísticas por PushNotificationId
                    var statsByNotification = notificationPushUsers
                        .GroupJoin(
                            notificationPushUserDevices,
                            npu => npu.Id,
                            npud => npud.NotificationPushUserId,
                            (npu, devices) => new
                            {
                                npu.PushNotificationId,
                                Devices = devices.ToList()
                            }
                        )
                        .GroupBy(x => x.PushNotificationId)
                        .ToDictionary(
                            g => g.Key,
                            g => new
                            {
                                TotalUsers = g.Count(),
                                TotalDevices = g.SelectMany(x => x.Devices).Count(),
                                SuccessDevices = g.SelectMany(x => x.Devices).Count(d => d.StatusNotification == EnumsDataBase.StatusNotification.Sended),
                                ErrorDevices = g.SelectMany(x => x.Devices).Count(d => d.StatusNotification == EnumsDataBase.StatusNotification.Error)
                            }
                        );

                    // Actualizar estadísticas en los items
                    foreach (var item in paginatedResult.Items)
                    {
                        if (statsByNotification.TryGetValue(item.Id, out var stats))
                        {
                            item.TotalUsersSent = stats.TotalUsers;
                            item.SuccessfullySentCount = stats.SuccessDevices;
                            item.ErrorCount = stats.ErrorDevices;
                        }
                    }
                }

                return new GetNotificationPushesResponse(paginatedResult.TotalItems, paginatedResult.Items)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            }
        ).ConfigureAwait(false);
}

