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

         

                return new CreateTrainerResponse(Guid.Empty, null)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            },
            registerLogAudit: true
        ).ConfigureAwait(false);
}
