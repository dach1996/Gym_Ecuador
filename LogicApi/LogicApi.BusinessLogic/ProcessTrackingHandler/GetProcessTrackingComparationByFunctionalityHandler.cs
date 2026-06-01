using LogicApi.Model.Common;
using LogicApi.Model.Request.ProcessTracking;
using LogicApi.Model.Response.ProcessTracking;
using static LogicApi.Model.Request.ProcessTracking.GetProcessTrackingComparationByFunctionalityRequest;

namespace LogicApi.BusinessLogic.ProcessTrackingHandler;

/// <summary>
/// Handler para obtener comparación de seguimiento de proceso por funcionalidad
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetProcessTrackingComparationByFunctionalityHandler(
    ILogger<GetProcessTrackingComparationByFunctionalityHandler> logger,
    IPluginFactory pluginFactory) : ProcessTrackingBase<GetProcessTrackingComparationByFunctionalityRequest, GetProcessTrackingComparationByFunctionalityResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de la comparación del seguimiento de proceso más reciente del usuario autenticado
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetProcessTrackingComparationByFunctionalityResponse> Handle(GetProcessTrackingComparationByFunctionalityRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetProcessTrackingComparationByFunctionality, request, async () =>
            {
                List<StatisticComparisonModel> statistics = [];
                var codesToSearch = (request.Type switch
                {
                    GetProcessTrackingComparationFunctionalityType.Weight => [
                        PhysicalParameterCode.Weight,
                        PhysicalParameterCode.BodyFatPercentage,
                        PhysicalParameterCode.Bmi],
                    GetProcessTrackingComparationFunctionalityType.Measurements => new[] {
                        PhysicalParameterCode.ChestMeasurement,
                        PhysicalParameterCode.WaistMeasurement,
                        PhysicalParameterCode.HipMeasurement,
                        PhysicalParameterCode.ArmRightMeasurement,
                    },
                    _ => throw new CustomException((int)MessagesCodesError.SystemError, "Tipo de comparación por funcionalidad no válido"),
                }).Select(code => code.GetEnumMember());
                var results = await UnitOfWork.ProcessTrackingRepository
                    .GetGenericAsync(
                        select => new { select.Id },
                        tracking => tracking.UserId == UserId,
                        orderBy => orderBy.DateTimeRegister,
                        OrderByType.Desc,
                        top: 2
                    ).ConfigureAwait(false);

                if (results.Count != 0)
                {
                    var trackingToSearchIds = results.Select(result => result.Id).ToList();
                    var measurementsByProcessTrackingId = await GetMeasurementValuesByProcessTrackingIdsAsync(trackingToSearchIds).ConfigureAwait(false);

                    var current = measurementsByProcessTrackingId[results[0].Id];
                    var previous = measurementsByProcessTrackingId.Count > 1 ? measurementsByProcessTrackingId[results[1].Id] : null;
                    statistics = [.. CalculatePartialMeasurementsDifference(current, previous ?? []).Where(statistic => codesToSearch.Contains(statistic.Code))];
                }
                return new GetProcessTrackingComparationByFunctionalityResponse(statistics);
            }
        ).ConfigureAwait(false);
}
