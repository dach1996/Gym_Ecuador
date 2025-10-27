using LogicApi.Model.Request.Trainer;
using LogicApi.Model.Response.Trainer;
using PersistenceDb.Models.Core;

namespace LogicApi.BusinessLogic.TrainerHandler;

/// <summary>
/// Handler para crear entrenador
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class CreateTrainerHandler(
    ILogger<CreateTrainerHandler> logger,
    IPluginFactory pluginFactory) : TrainerBase<CreateTrainerRequest, CreateTrainerResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la creación de un entrenador
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<CreateTrainerResponse> Handle(CreateTrainerRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.CreateTrainer, request, async () =>
            {
                // Validar que la persona exista
                var person = await UnitOfWork.PersonRepository
                    .GetByFirstOrDefaultAsync(where => where.Id == request.PersonId)
                    .ConfigureAwait(false);

                if (person == null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "La persona especificada no existe");

                // Validar que el gimnasio exista
                var gym = await UnitOfWork.GymRepository
                    .GetByFirstOrDefaultAsync(where => where.Guid == request.GymGuid)
                    .ConfigureAwait(false);

                if (gym == null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "El gimnasio especificado no existe");

                // Validar que la persona no sea ya entrenador en este gimnasio
                var existingTrainer = await UnitOfWork.TrainerRepository
                    .GetByFirstOrDefaultAsync(where => where.PersonId == request.PersonId && where.GymId == gym.Id)
                    .ConfigureAwait(false);

                if (existingTrainer != null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "Esta persona ya es entrenador en este gimnasio");

                // Crear el nuevo entrenador
                var newTrainer = new PersistenceDb.Models.Core.Trainer
                {
                    Guid = Guid.NewGuid(),
                    PersonId = request.PersonId,
                    GymId = gym.Id,
                    Specialty = request.Specialty,
                    Biography = request.Biography,
                    ProfilePhotoUrl = request.ProfilePhotoUrl,
                    IsActive = true,
                    DateTimeRegister = Now,
                    UserIdRegister = UserId
                };

                // Guardar en la base de datos
                await UnitOfWork.TrainerRepository.AddAsync(newTrainer).ConfigureAwait(false);

                return new CreateTrainerResponse(newTrainer.Guid, newTrainer.Specialty)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            },
            registerLogAudit: true
        ).ConfigureAwait(false);
}
