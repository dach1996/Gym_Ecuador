using LogicApi.Model.Request.ProcessTracking;
using LogicApi.Model.Response.ProcessTracking;
using LogicCommon.Model.Response.File;

namespace LogicApi.BusinessLogic.ProcessTrackingHandler;

/// <summary>
/// Handler para obtener seguimientos de proceso paginados
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetProcessTrackingsPaginatedHandler(
    ILogger<GetProcessTrackingsPaginatedHandler> logger,
    IPluginFactory pluginFactory) : ProcessTrackingBase<GetProcessTrackingsPaginatedRequest, GetProcessTrackingsPaginatedResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de seguimientos de proceso con paginación
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetProcessTrackingsPaginatedResponse> Handle(GetProcessTrackingsPaginatedRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetProcessTrackingsPaginated, request, async () =>
            {
                var processTrackings = await UnitOfWork.ProcessTrackingRepository
                    .GetPaginatorGenericAsync(
                        itemsByPage: request.PageSize,
                        page: request.Page,
                        select => new
                        {
                            select.Id,
                            Item = new ProcessTrackingItem
                            {
                                Guid = select.Guid,
                                RegistrationDate = select.DateTimeRegister,
                                Images = select.ProcessTrackingImages
                                    .Where(image => image.FilePersistence.State)
                                    .Select(image => new FileUrlResponse(
                                        image.FilePersistence.Guid,
                                        image.FilePersistence.FileBasePath.BaseUrl,
                                        image.FilePersistence.Path))
                                    .ToList()
                            }
                        },
                        where: where => where.UserId == UserId,
                        orderBy: processTracking => processTracking.DateTimeRegister,
                        orderByType: OrderByType.Desc
                    ).ConfigureAwait(false);

                var measurementsByProcessTrackingId = await GetMeasurementValuesByProcessTrackingIdsAsync(
                    processTrackings.Items.Select(item => item.Id)).ConfigureAwait(false);

                foreach (var processTracking in processTrackings.Items)
                {
                    if (measurementsByProcessTrackingId.TryGetValue(processTracking.Id, out var partialMeasurements))
                        ApplyMeasurementsToDetail(processTracking.Item, partialMeasurements);
                }

                return new GetProcessTrackingsPaginatedResponse
                {
                    Registers = [.. processTrackings.Items.Select(item => item.Item)],
                    TotalRegister = processTrackings.TotalItems
                };
            }).ConfigureAwait(false);
}
