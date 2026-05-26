using LogicApi.Model.Request.ProcessTracking;
using LogicCommon.Model.Response;

namespace LogicApi.BusinessLogic.ProcessTrackingHandler;

/// <summary>
/// Handler para actualizar seguimiento de proceso
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class UpdateProcessTrackingHandler(
    ILogger<UpdateProcessTrackingHandler> logger,
    IPluginFactory pluginFactory) : ProcessTrackingBase<UpdateProcessTrackingRequest, GenericCommonOperationResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la actualización de un seguimiento de proceso
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GenericCommonOperationResponse> Handle(UpdateProcessTrackingRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.UpdateProcessTracking, request, async () =>
            {
                var processTracking = await UnitOfWork.ProcessTrackingRepository
                    .GetByFirstOrDefaultAsync(where => where.Guid == request.ProcessTrackingGuid && where.UserId == UserId)
                    .ConfigureAwait(false) ?? throw new CustomException((int)MessagesCodesError.SystemError, "Seguimiento de proceso no encontrado");

                processTracking.Observations = request.Observations;

                await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);
                await UnitOfWork.ProcessTrackingRepository.UpdateAsync(processTracking).ConfigureAwait(false);

                await UnitOfWork.ProcessTrackingMeasurementRepository
                    .DeleteAsync(where => where.ProcessTrackingId == processTracking.Id)
                    .ConfigureAwait(false);

                var measurements = await MapToMeasurementEntitiesAsync(request, processTracking).ConfigureAwait(false);

                if (measurements.Count > 0)
                    await UnitOfWork.ProcessTrackingMeasurementRepository.AddRangeAsync(measurements).ConfigureAwait(false);

                await ProcessProcessTrackingImageFiles(request.Images, processTracking.Id, UserId).ConfigureAwait(false);
                await UnitOfWork.CommitAsync().ConfigureAwait(false);
                return GenericCommonOperationResponse.SuccessOperation();
            }).ConfigureAwait(false);
}
