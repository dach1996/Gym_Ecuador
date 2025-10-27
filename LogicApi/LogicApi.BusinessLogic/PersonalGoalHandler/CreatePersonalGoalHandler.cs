using LogicApi.Model.Request.PersonalGoal;
using LogicApi.Model.Response.PersonalGoal;
using PersistenceDb.Models.Core;

namespace LogicApi.BusinessLogic.PersonalGoalHandler;

/// <summary>
/// Handler para crear objetivo personal
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class CreatePersonalGoalHandler(
    ILogger<CreatePersonalGoalHandler> logger,
    IPluginFactory pluginFactory) : PersonalGoalBase<CreatePersonalGoalRequest, CreatePersonalGoalResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la creación de un objetivo personal
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<CreatePersonalGoalResponse> Handle(CreatePersonalGoalRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.CreatePersonalGoal, request, async () =>
            {
                // Validar que la persona exista
                var person = await UnitOfWork.PersonRepository
                    .GetByFirstOrDefaultAsync(where => where.Id == request.PersonId)
                    .ConfigureAwait(false);

                if (person == null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "La persona especificada no existe");

                // Validar fechas
                if (request.EstimatedEndDate.HasValue && request.StartDate >= request.EstimatedEndDate.Value)
                    throw new CustomException((int)MessagesCodesError.SystemError, "La fecha de inicio debe ser anterior a la fecha de fin estimada");

                // Validar valores
                if (request.TargetValue <= 0)
                    throw new CustomException((int)MessagesCodesError.SystemError, "El valor objetivo debe ser mayor a cero");

                if (request.InitialValue.HasValue && request.InitialValue.Value < 0)
                    throw new CustomException((int)MessagesCodesError.SystemError, "El valor inicial no puede ser negativo");

                // Crear el nuevo objetivo personal
                var newPersonalGoal = new PersonalGoal
                {
                    Guid = Guid.NewGuid(),
                    PersonId = request.PersonId,
                    GoalType = request.GoalType,
                    InitialValue = request.InitialValue,
                    TargetValue = request.TargetValue,
                    StartDate = request.StartDate,
                    EstimatedEndDate = request.EstimatedEndDate,
                    GoalStatus = "Activo",
                    Description = request.Description,
                    DateTimeRegister = Now,
                    UserIdRegister = UserId
                };

                // Guardar en la base de datos
                await UnitOfWork.PersonalGoalRepository.AddAsync(newPersonalGoal).ConfigureAwait(false);

                return new CreatePersonalGoalResponse(newPersonalGoal.Guid, newPersonalGoal.GoalType, newPersonalGoal.TargetValue)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            }
        ).ConfigureAwait(false);
}
