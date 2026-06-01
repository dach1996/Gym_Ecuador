using LogicApi.Model.Request.ProcessTracking;
using LogicCommon.Model.Response;
using PersistenceDb.Models.Core;

namespace LogicApi.BusinessLogic.ProcessTrackingHandler;

/// <summary>
/// Handler para crear seguimiento de proceso
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class CreateProcessTrackingHandler(
    ILogger<CreateProcessTrackingHandler> logger,
    IPluginFactory pluginFactory) : ProcessTrackingBase<CreateProcessTrackingRequest, GenericCommonOperationResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la creación de un seguimiento de proceso
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GenericCommonOperationResponse> Handle(CreateProcessTrackingRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.CreateProcessTracking, request, async () =>
        {
            var newProcessTracking = new ProcessTracking
            {
                Guid = Guid.NewGuid(),
                DateTimeRegister = Now,
                Observations = request.Observations,
                UserId = UserId,
            };

            await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);
            await UnitOfWork.ProcessTrackingRepository.AddAsync(newProcessTracking).ConfigureAwait(false);

            var measurements = await MapToMeasurementEntitiesAsync(request, newProcessTracking, requireWeightAndHeight: true).ConfigureAwait(false);

            if (measurements.Count > 0)
                await UnitOfWork.ProcessTrackingMeasurementRepository.AddRangeAsync(measurements).ConfigureAwait(false);

            await ProcessProcessTrackingImageFiles(request.Images, newProcessTracking.Id, UserId).ConfigureAwait(false);
            await UnitOfWork.CommitAsync().ConfigureAwait(false);
            return GenericCommonOperationResponse.SuccessOperation();
        }
        ).ConfigureAwait(false);

}
