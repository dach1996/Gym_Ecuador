using LogicApi.Model.Request.ProcessTracking;
using LogicApi.Model.Response.ProcessTracking;
using LogicApi.Model.Enum;

namespace LogicApi.BusinessLogic.ProcessTrackingHandler;

/// <summary>
/// Handler para obtener estadísticas de seguimientos de procesos
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetProcessTrackingStatisticsHandler(
    ILogger<GetProcessTrackingStatisticsHandler> logger,
    IPluginFactory pluginFactory) : ProcessTrackingBase<GetProcessTrackingStatisticsRequest, GetProcessTrackingStatisticsResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de estadísticas de seguimientos de procesos
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetProcessTrackingStatisticsResponse> Handle(GetProcessTrackingStatisticsRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetProcessTrackingStatistics, request, async () =>
            {
                _ = await UnitOfWork.ProcessTrackingRepository
                    .GetGenericAsync(
                        select => new
                        {
                            select.Id,
                            select.DateTimeRegister,
                        },
                        where => where.UserId == UserId && where.DateTimeRegister >= request.StartDate && where.DateTimeRegister <= request.EndDate)
                    .ConfigureAwait(false);

                return new GetProcessTrackingStatisticsResponse
                {
                    StatisticsControl = new List<CartesianPoint>
                    {
                        new CartesianPoint { Label = "Peso Corporal", CurrentValue = "70", ValueType = "kg", IconCode = "WeightIcon", DifferenceValue = "10 kg", DifferenceValueType = DifferenceValueType.Positive , MinXAxisValue = 50, MaxXAxisValue = 100, MinYAxisValue = 1, MaxYAxisValue = 5 , CartesianPoints = new List<CartesianPoin> { new () { XLabel = "1 OCT", YLabel = "1", XValue = 1, YValue = 70 }, new () { XLabel = "15 OCT", YLabel = "2", XValue = 2, YValue = 75 }, new () { XLabel = "HOY", YLabel = "3", XValue = 3, YValue = 79.5m } } },
                        new CartesianPoint { Label = "Porcentaje de Grasa Corporal", CurrentValue = "20", ValueType = "%", IconCode = "FatPercentageIcon", DifferenceValue = "10 %", DifferenceValueType = DifferenceValueType.Positive , MinXAxisValue = 50, MaxXAxisValue = 100, MinYAxisValue = 1, MaxYAxisValue = 5 , CartesianPoints = new List<CartesianPoin> { new () { XLabel = "1 OCT", YLabel = "1", XValue = 1, YValue = 70 }, new () { XLabel = "15 OCT", YLabel = "2", XValue = 2, YValue = 75 }, new () { XLabel = "HOY", YLabel = "3", XValue = 3, YValue = 79.5m } } },
                    },
                };
            }).ConfigureAwait(false);
}
