using LogicApi.Model.Request.ProcessTracking;
using LogicApi.Model.Response.Common.ProcessTracking;
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
                var processTracking = await UnitOfWork.ProcessTrackingRepository
                    .GetFirstOrDefaultGenericAsync(
                        select => new
                        {
                            select.Id,
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

                var measurementsByProcessTrackingId = await GetMeasurementValuesByProcessTrackingIdsAsync([processTracking.Id]).ConfigureAwait(false);
                ApplyMeasurementsToDetail(processTracking.Detail, measurementsByProcessTrackingId[processTracking.Id]);

                return new GetProcessTrackingByGuidResponse(processTracking.Detail);
            }).ConfigureAwait(false);


}
