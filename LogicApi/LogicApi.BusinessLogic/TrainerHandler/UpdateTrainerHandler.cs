using LogicApi.Model.Request.Trainer;
using LogicApi.Model.Response.Trainer;

namespace LogicApi.BusinessLogic.TrainerHandler;

/// <summary>
/// Handler para actualizar entrenador
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class UpdateTrainerHandler(
    ILogger<UpdateTrainerHandler> logger,
    IPluginFactory pluginFactory) : TrainerBase<UpdateTrainerRequest, UpdateTrainerResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la actualización de un entrenador
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<UpdateTrainerResponse> Handle(UpdateTrainerRequest request, CancellationToken cancellationToken)
    {
        return await ExecuteHandlerAsync(
            OperationApiName.UpdateTrainer,
            request,
            async () =>
            {
                // Buscar el entrenador por GUID
                var trainer = await UnitOfWork.TrainerRepository
                    .GetByFirstOrDefaultAsync(where => where.Guid == request.TrainerGuid)
                    .ConfigureAwait(false);

                if (trainer == null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "Entrenador no encontrado");

                // Actualizar los campos
                trainer.Specialty = request.Specialty;
                trainer.Biography = request.Biography;
                trainer.ProfilePhotoUrl = request.ProfilePhotoUrl;
                trainer.IsActive = request.IsActive;

                // Guardar cambios

                return new UpdateTrainerResponse(trainer.Guid, trainer.Specialty)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            },
            registerLogAudit: true
        ).ConfigureAwait(false);
    }
}
