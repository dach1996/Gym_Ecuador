using LogicApi.Model.Request.Trainer;
using LogicApi.Model.Response.Trainer;

namespace LogicApi.BusinessLogic.TrainerHandler;

/// <summary>
/// Handler para obtener entrenador por GUID
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetTrainerByGuidHandler(
    ILogger<GetTrainerByGuidHandler> logger,
    IPluginFactory pluginFactory) : TrainerBase<GetTrainerByGuidRequest, GetTrainerByGuidResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de un entrenador por GUID
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetTrainerByGuidResponse> Handle(GetTrainerByGuidRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetTrainerByGuid, request, async () =>
            {
                // Buscar el entrenador por GUID con includes
                var trainer = await UnitOfWork.TrainerRepository
                    .GetByFirstOrDefaultAsync(
                        where => where.Guid == request.TrainerGuid,
                        include => include.Person
                    ).ConfigureAwait(false);

                if (trainer == null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "Entrenador no encontrado");

                // Mapear a DTO
                var trainerDetail = new TrainerDetail
                {
                    Guid = trainer.Guid,
                    
                    IsActive = trainer.IsActive,
                    DateTimeRegister = trainer.DateTimeRegister
                };

                return new GetTrainerByGuidResponse(trainerDetail)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            },
            registerLogAudit: false
        ).ConfigureAwait(false);
}
