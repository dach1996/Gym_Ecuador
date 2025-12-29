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
                // Validar que la persona exista
                var newProcessTracking = new ProcessTracking
                {
                    Guid = Guid.NewGuid(),
                    DateTimeRegister = Now,
                    Weight = request.Weight,
                    Height = request.Height,
                    BodyFatPercentage = request.BodyFatPercentage,
                    MuscleMassPercentage = request.MuscleMassPercentage,
                    ChestMeasurement = request.ChestMeasurement,
                    WaistMeasurement = request.WaistMeasurement,
                    HipMeasurement = request.HipMeasurement,
                    ArmRightMeasurement = request.ArmRightMeasurement,
                    ThighRightMeasurement = request.ThighRightMeasurement,
                    Observations = request.Observations,
                    UserId = UserId,
                };
                // Guardar en la base de datos
                await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);
                await UnitOfWork.ProcessTrackingRepository.AddAsync(newProcessTracking).ConfigureAwait(false);
                await ProcessProcessTrackingImageFiles(request.Images, newProcessTracking.Id, UserId).ConfigureAwait(false);
                await UnitOfWork.CommitAsync().ConfigureAwait(false);
                return GenericCommonOperationResponse.SuccessOperation();
            }
        ).ConfigureAwait(false);

}
