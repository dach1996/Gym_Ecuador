using LogicApi.Model.Request.Trainer;
using LogicApi.Model.Response.Trainer;

namespace LogicApi.BusinessLogic.TrainerHandler;

/// <summary>
/// Handler para obtener entrenadores
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetTrainersHandler(
    ILogger<GetTrainersHandler> logger,
    IPluginFactory pluginFactory) : TrainerBase<GetTrainersRequest, GetTrainersResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de entrenadores con paginación
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetTrainersResponse> Handle(GetTrainersRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetTrainers, request, async () =>
            {
                // Construir la consulta base con includes
                var trainers = await UnitOfWork.TrainerRepository
                    .GetByAsync(
                     
                    ).ConfigureAwait(false);

             

                // Mapear a DTOs
                var trainerItems = trainers.Select(t => new TrainerItem
                {
                    Guid = t.Guid,
                    FullName = $"{t.Person.FullName}",
                    Specialty = t.Specialty,
                    ProfilePhotoUrl = t.ProfilePhotoUrl,
                    GymName = t.Gym.Name,
                    IsActive = t.IsActive,
                    DateTimeRegister = t.DateTimeRegister
                });

                return new GetTrainersResponse(trainerItems, trainers.Count, request.Page, request.PageSize)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            }
        ).ConfigureAwait(false);
}
