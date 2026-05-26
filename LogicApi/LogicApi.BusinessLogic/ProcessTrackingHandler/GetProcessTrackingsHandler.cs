using LogicApi.Model.Request.ProcessTracking;
using LogicApi.Model.Response.ProcessTracking;
using LogicCommon.Model.Response.File;

namespace LogicApi.BusinessLogic.ProcessTrackingHandler;

/// <summary>
/// Handler para obtener seguimientos de procesos
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetProcessTrackingsHandler(
    ILogger<GetProcessTrackingsHandler> logger,
    IPluginFactory pluginFactory) : ProcessTrackingBase<GetProcessTrackingsRequest, GetProcessTrackingsResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de seguimientos de procesos con paginación
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetProcessTrackingsResponse> Handle(GetProcessTrackingsRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetProcessTrackings, request, async () =>
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
                    ApplyMeasurementsToDetail(processTracking.Item, measurementsByProcessTrackingId[processTracking.Id]);

                return new GetProcessTrackingsResponse
                {
                    Registers = [.. processTrackings.Items.Select(item => item.Item)],
                    TotalRegister = processTrackings.TotalItems
                };
            }).ConfigureAwait(false);
}
