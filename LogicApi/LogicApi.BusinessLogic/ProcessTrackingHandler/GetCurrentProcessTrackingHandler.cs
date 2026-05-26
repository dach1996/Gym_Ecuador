using LogicApi.Model.Common;
using LogicApi.Model.Enum;
using LogicApi.Model.Request.ProcessTracking;
using LogicApi.Model.Response.ProcessTracking;

namespace LogicApi.BusinessLogic.ProcessTrackingHandler;

/// <summary>
/// Handler para obtener el seguimiento de proceso más reciente del usuario
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetCurrentProcessTrackingHandler(
    ILogger<GetCurrentProcessTrackingHandler> logger,
    IPluginFactory pluginFactory) : ProcessTrackingBase<GetCurrentProcessTrackingRequest, GetCurrentProcessTrackingResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención del seguimiento de proceso más reciente del usuario autenticado
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetCurrentProcessTrackingResponse> Handle(GetCurrentProcessTrackingRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetCurrentProcessTracking, request, async () =>
            {
                CurrentProcessTrackingDetail currentProcessTrackingDetail = null;

                var results = await UnitOfWork.ProcessTrackingRepository
                    .GetGenericAsync(
                        select => new { select.Id },
                        tracking => tracking.UserId == UserId,
                        orderBy => orderBy.Id,
                        OrderByType.Desc,
                        top: 2
                    ).ConfigureAwait(false);

                if (results.Count != 0)
                {
                    var trackingToSearchIds = results.Select(result => result.Id).ToList();
                    var measurementsByProcessTrackingId = await GetMeasurementValuesByProcessTrackingIdsAsync(trackingToSearchIds).ConfigureAwait(false);

                    var current = measurementsByProcessTrackingId[results[0].Id];
                    var previous = measurementsByProcessTrackingId.Count > 1 ? measurementsByProcessTrackingId[results[1].Id] : null;
                    currentProcessTrackingDetail = new()
                    {
                        Statistics = CalculatePartialMeasurementsDifference(current, previous ?? [])
                    };
                }
                return new GetCurrentProcessTrackingResponse(currentProcessTrackingDetail);
            }
        ).ConfigureAwait(false);
}
