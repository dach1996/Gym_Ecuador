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
                // Buscar el seguimiento de proceso por GUID
                var processTracking = await UnitOfWork.ProcessTrackingRepository
                    .GetByFirstOrDefaultAsync(where => where.Guid == request.ProcessTrackingGuid && where.UserId == UserId)
                    .ConfigureAwait(false) ?? throw new CustomException((int)MessagesCodesError.SystemError, "Seguimiento de proceso no encontrado");

                processTracking.Weight = request.Weight;
                processTracking.Height = request.Height;
                processTracking.BodyFatPercentage = request.BodyFatPercentage;
                processTracking.MuscleMassPercentage = request.MuscleMassPercentage;
                processTracking.ChestMeasurement = request.ChestMeasurement;
                processTracking.WaistMeasurement = request.WaistMeasurement;
                processTracking.HipMeasurement = request.HipMeasurement;
                processTracking.ArmRightMeasurement = request.ArmRightMeasurement;
                processTracking.ThighRightMeasurement = request.ThighRightMeasurement;
                await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);
                await ProcessProcessTrackingImageFiles(request.Images, processTracking.Id, UserId).ConfigureAwait(false);
                await UnitOfWork.CommitAsync().ConfigureAwait(false);
                return GenericCommonOperationResponse.SuccessOperation();
            }).ConfigureAwait(false);
}
