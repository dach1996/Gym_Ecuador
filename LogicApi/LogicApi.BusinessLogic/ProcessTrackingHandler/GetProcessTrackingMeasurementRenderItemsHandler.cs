using LogicApi.Model.Request.ProcessTracking;
using LogicApi.Model.Response.ProcessTracking;

namespace LogicApi.BusinessLogic.ProcessTrackingHandler;

/// <summary>
/// Handler para obtener items de medidas para renderizado del formulario
/// </summary>
public class GetProcessTrackingMeasurementRenderItemsHandler(
    ILogger<GetProcessTrackingMeasurementRenderItemsHandler> logger,
    IPluginFactory pluginFactory) : ProcessTrackingBase<GetProcessTrackingMeasurementRenderItemsRequest, GetProcessTrackingMeasurementRenderItemsResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Obtiene el catálogo de medidas con valores precargados del último seguimiento del usuario
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override Task<GetProcessTrackingMeasurementRenderItemsResponse> Handle(GetProcessTrackingMeasurementRenderItemsRequest request, CancellationToken cancellationToken)
        => ExecuteHandlerAsync(OperationApiName.GetProcessTrackingMeasurementRenderItems, request, async () =>
            new GetProcessTrackingMeasurementRenderItemsResponse
            {
                Items = await BuildMeasurementRenderItemsAsync().ConfigureAwait(false)
            });

    /// <summary>
    /// Construye items de medidas para renderizado del formulario con valores precargados del último seguimiento
    /// </summary>
    private async Task<List<ProcessTrackingMeasurementRenderItem>> BuildMeasurementRenderItemsAsync()
    {
        var imcCode = PhysicalParameterCode.Bmi.GetEnumMember();
        var weightCode = PhysicalParameterCode.Weight.GetEnumMember();
        var heightCode = PhysicalParameterCode.Height.GetEnumMember();

        var parameters = (await GetPhysicalParametersAsync().ConfigureAwait(false))
            .Where(parameter => parameter.IsActive && !parameter.Code.Equals(imcCode, StringComparison.OrdinalIgnoreCase))
            .OrderBy(parameter => parameter.Id)
            .ToArray();

        var latestResults = await UnitOfWork.ProcessTrackingRepository
            .GetGenericAsync(
                select => new { select.Id },
                tracking => tracking.UserId == UserId,
                orderBy => orderBy.Id,
                OrderByType.Desc,
                top: 1)
            .ConfigureAwait(false);

        var currentValuesByCode = new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase);
        if (latestResults.Count > 0)
        {
            var measurements = await UnitOfWork.ProcessTrackingMeasurementRepository
                .GetGenericAsync(
                    select => new { select.PhysicalParameter.Code, select.Value },
                    where => where.ProcessTrackingId == latestResults[0].Id)
                .ConfigureAwait(false);

            foreach (var measurement in measurements.Where(m => !m.Code.Equals(imcCode, StringComparison.OrdinalIgnoreCase)))
                currentValuesByCode.TryAdd(measurement.Code, measurement.Value);
        }

        return [.. parameters.Select(parameter => new ProcessTrackingMeasurementRenderItem
        {
            Code = parameter.Code,
            Label = parameter.Name,
            Unit = parameter.MeasurementUnit.GetEnumMember(),
            Icon = parameter.IconCode,
            IsRequired = parameter.Code.Equals(weightCode, StringComparison.OrdinalIgnoreCase)
                || parameter.Code.Equals(heightCode, StringComparison.OrdinalIgnoreCase),
            CurrentValue = currentValuesByCode.TryGetValue(parameter.Code, out var value) ? value : null,
            ValidationRegex = parameter.ValidationRegex
        })];
    }
}
