using LogicApi.Model.Request.ProcessTracking;
using LogicApi.Model.Response.ProcessTracking;
using Common.WebCommon.Models.Enum;

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
                // Obtener todos los registros del usuario ordenados por fecha
                var processTrackings = await UnitOfWork.ProcessTrackingRepository
                    .GetGenericAsync(
                        select => new
                        {
                            ArmRightMeasurement = select.ArmRightMeasurement,
                            ChestMeasurement = select.ChestMeasurement,
                            WaistMeasurement = select.WaistMeasurement,
                            ThighRightMeasurement = select.ThighRightMeasurement,
                            HipMeasurement = select.HipMeasurement,
                            BodyFatPercentage = select.BodyFatPercentage,
                            MuscleMassPercentage = select.MuscleMassPercentage,
                            Weight = select.Weight,
                        },
                        where => where.UserId == UserId
                    ).ConfigureAwait(false);

                return new GetProcessTrackingStatisticsResponse
                {
                    WeightControlStatistics = new WeightControlCartesianPoint
                    {
                        MinXAxisValue = 50,
                        MaxXAxisValue = 100,
                        MinYAxisValue = 1,
                        MaxYAxisValue = 5,
                        CartesianPoints = new List<CartesianPoin>
                        {
                            new () { XLabel = "1 OCT", YLabel = "1", XValue = 1, YValue = 70 },
                            new () { XLabel = "15 OCT", YLabel = "2", XValue = 2, YValue = 75 },
                            new () { XLabel = "HOY", YLabel = "3", XValue = 3, YValue = 79.5m },
                        }
                    },
                    Metrics = new List<MeasurementMetricItem>
                    {
                        new () { Label = "IMC", Value = 24.5m, ComparisonValue = 1.5m, Unit = "kg/m2", Icon = "ImcIcon" },
                        new () { Label = "Pecho", Value = 90m, ComparisonValue = -5m, Unit = "cm", Icon = "ChestIcon" },
                        new () { Label = "Cintura", Value = 80m, ComparisonValue = +2m, Unit = "cm", Icon = "WaistIcon" },
                        new () { Label = "Brazo", Value = 30m, ComparisonValue = -3m, Unit = "cm", Icon = "ArmRightIcon" },
                    },
                    WeightComparison = new WeightComparison
                    {
                        InitialDate = Now.AddDays(68),
                        InitialWeight = 70m,
                        CurrentDate = Now,
                        CurrentWeight = 79.5m,
                        Message = "Has ganado 9.5kg en 68 días"
                    }
                };
            }).ConfigureAwait(false);
}
