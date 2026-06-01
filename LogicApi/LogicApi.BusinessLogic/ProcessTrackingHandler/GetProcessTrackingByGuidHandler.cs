using LogicApi.Model.Request.ProcessTracking;
using LogicApi.Model.Response.ProcessTracking;
using LogicCommon.Model.Response.File;

namespace LogicApi.BusinessLogic.ProcessTrackingHandler;

/// <summary>
/// Handler para obtener seguimiento de proceso por GUID
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetProcessTrackingByGuidHandler(
    ILogger<GetProcessTrackingByGuidHandler> logger,
    IPluginFactory pluginFactory) : ProcessTrackingBase<GetProcessTrackingByGuidRequest, GetProcessTrackingByGuidResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de un seguimiento de proceso por GUID
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetProcessTrackingByGuidResponse> Handle(GetProcessTrackingByGuidRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetProcessTrackingByGuid, request, async () =>
        {
            var currentTracking = await UnitOfWork.ProcessTrackingRepository
                .GetFirstOrDefaultGenericAsync(
                    select => new
                    {
                        select.Id,
                        select.DateTimeRegister,
                        Detail = new ProcessTrackingDetail
                        {
                            Guid = select.Guid,
                            Observations = select.Observations,
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
                    where => where.Guid == request.ProcessTrackingGuid && where.UserId == UserId
                ).ConfigureAwait(false) ?? throw new CustomException((int)MessagesCodesError.SystemError, "Seguimiento de proceso no encontrado");
            // Obtener el seguimiento de proceso anterior
            var previousTracking = await UnitOfWork.ProcessTrackingRepository
                .GetGenericAsync(
                    select => new { select.Id },
                    where => where.UserId == UserId
                     && where.Id < currentTracking.Id,
                    orderBy => orderBy.DateTimeRegister,
                        OrderByType.Desc,
                        top: 1)
                    .ConfigureAwait(false);
            var listToSearch = new List<int> { currentTracking.Id };
            if (previousTracking.Count > 0)
                listToSearch.Add(previousTracking[0].Id);

            var measurementsDifference = (await GetMeasurementsDifferenceAsync(listToSearch).ConfigureAwait(false))
                .Where(measurement => !measurement.Code.Equals(PhysicalParameterCode.Height.GetEnumMember(), StringComparison.OrdinalIgnoreCase));
            var listWeightCodes = new List<string> { PhysicalParameterCode.Weight.GetEnumMember(), PhysicalParameterCode.Bmi.GetEnumMember(), PhysicalParameterCode.BodyFatPercentage.GetEnumMember() };
            currentTracking.Detail.WeightMeasurements = [.. measurementsDifference.Where(measurement => listWeightCodes.Contains(measurement.Code))];
            currentTracking.Detail.Measurements = [.. measurementsDifference.Except(currentTracking.Detail.WeightMeasurements)];
            return new GetProcessTrackingByGuidResponse(currentTracking.Detail);
        }).ConfigureAwait(false);


}
