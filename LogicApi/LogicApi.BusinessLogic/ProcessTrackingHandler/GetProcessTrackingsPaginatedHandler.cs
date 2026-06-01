using LogicApi.Model.Request.ProcessTracking;
using LogicApi.Model.Response.ProcessTracking;
using LogicCommon.BusinessLogic;
using LogicCommon.Model.Common.ProcessTracking;
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
                var heightCode = PhysicalParameterCode.Height.GetEnumMember();
                var processTrackings = await UnitOfWork.ProcessTrackingRepository
                    .GetPaginatorGenericAsync(
                        itemsByPage: request.PageSize,
                        page: request.Page,
                        select => new
                        {
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
                                    .ToList(),

                            },
                            Measurements = select.ProcessTrackingMeasurements
                            .Select(measurement => new
                            {
                                PhysicalParameter = new ProcessTrackingMeasurementModel
                                {
                                    Code = measurement.PhysicalParameter.Code,
                                    Label = measurement.PhysicalParameter.Name,
                                    Value = measurement.Value,
                                    Icon = measurement.PhysicalParameter.IconCode
                                },
                                measurement.PhysicalParameter.MeasurementUnit
                            }).ToList()
                        },
                        where: where => where.UserId == UserId,
                        orderBy: processTracking => processTracking.DateTimeRegister,
                        orderByType: OrderByType.Desc
                    ).ConfigureAwait(false);


                var listWeightCodes = new List<string> { PhysicalParameterCode.Weight.GetEnumMember(), PhysicalParameterCode.Bmi.GetEnumMember(), PhysicalParameterCode.BodyFatPercentage.GetEnumMember() };

                var parameters = await GetPhysicalParametersAsync().ConfigureAwait(false);
                foreach (var processTracking in processTrackings.Items)
                {
                    var currentProcessTracking = processTracking.Item;
                    var measurments = processTracking.Measurements.Select(measurement =>
                    {
                        var physicalParameter = measurement.PhysicalParameter;
                        physicalParameter.Unit = measurement.MeasurementUnit.GetEnumMember();
                        return physicalParameter;
                    }).ToList();
                    var bmiParameter = parameters.First(select => select.Code.Equals(PhysicalParameterCode.Bmi.GetEnumMember(), StringComparison.OrdinalIgnoreCase));
                    var weightParameter = measurments.FirstOrDefault(select => select.Code.Equals(PhysicalParameterCode.Weight.GetEnumMember(), StringComparison.OrdinalIgnoreCase))?.Value;
                    var heightParameter = measurments.FirstOrDefault(select => select.Code.Equals(PhysicalParameterCode.Height.GetEnumMember(), StringComparison.OrdinalIgnoreCase))?.Value;
                    if (weightParameter.HasValue && heightParameter.HasValue)
                        measurments.Add(new ProcessTrackingMeasurementModel
                        {
                            Code = bmiParameter.Code,
                            Label = bmiParameter.Name,
                            Value = BusinessLogicUtils.CalculateBmi(weightParameter.Value, heightParameter.Value),
                            Icon = bmiParameter.IconCode
                        });
                    
                    measurments = [.. measurments.Where(measurement => !measurement.Code.Equals(PhysicalParameterCode.Height.GetEnumMember(), StringComparison.OrdinalIgnoreCase))];
                    currentProcessTracking.PrincipalMeasurements = [.. measurments.Where(measurement => listWeightCodes.Contains(measurement.Code))];
                    currentProcessTracking.SecondaryMeasurements = [.. measurments.Except(currentProcessTracking.PrincipalMeasurements)];
                }

                return new GetProcessTrackingsPaginatedResponse
                {
                    Registers = [.. processTrackings.Items.Select(item => item.Item)],
                    TotalRegister = processTrackings.TotalItems
                };
            }).ConfigureAwait(false);
}
