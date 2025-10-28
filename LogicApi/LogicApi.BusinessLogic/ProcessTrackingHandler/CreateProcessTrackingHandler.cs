using LogicApi.Model.Request.ProcessTracking;
using LogicApi.Model.Response.ProcessTracking;
using LogicCommon.Model.Request.File;
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
                var personId = await UnitOfWork.PersonRepository.GetIdByGuidAsync(request.PersonGuid).ConfigureAwait(false);
                // Crear el nuevo seguimiento de proceso
                var newProcessTracking = new ProcessTracking
                {
                    PersonId = personId,
                    GymId = null,
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
                    UserIdRegister = UserId,
                };
                // Guardar en la base de datos
                var processTrackingImages = new List<ProcessTrackingImage>();
                foreach (var image in request.Base64Images)
                {
                    var file = await UpdateFileAsync(image, "process_tracking", "process_tracking").ConfigureAwait(false);
                    processTrackingImages.Add(new ProcessTrackingImage
                    {
                        ProcessTrackingId = newProcessTracking.Id,
                        FilePersistenceId = file.Id,
                        DateTimeRegister = Now,
                        UserIdRegister = UserId
                    });
                }
                await UnitOfWork.ProcessTrackingImageRepository.AddRangeAsync(processTrackingImages).ConfigureAwait(false);
                await UnitOfWork.ProcessTrackingRepository.AddAsync(newProcessTracking).ConfigureAwait(false);
                return GenericCommonOperationResponse.SuccessOperation();
            }
        ).ConfigureAwait(false);
}
