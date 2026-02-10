using LogicApi.Model.Request.Routine;
using LogicApi.Model.Response.Routine;
using PersistenceDb.Models.Core;
using Common.WebApi.Models.Enum;

namespace LogicApi.BusinessLogic.RoutineHandler;

/// <summary>
/// Handler para crear rutina con ejercicios
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class CreateRoutineWithExercisesHandler(
    ILogger<CreateRoutineWithExercisesHandler> logger,
    IPluginFactory pluginFactory) : RoutineBase<CreateRoutineWithExercisesRequest, CreateRoutineWithExercisesResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la creación de una rutina con sus ejercicios
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<CreateRoutineWithExercisesResponse> Handle(CreateRoutineWithExercisesRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.CreateRoutineWithExercises, request, async () =>
            {
                // Validar que existan los ejercicios
                var exerciseGuids = request.Exercises.Select(e => e.ExerciseGuid).ToList();
                var exercises = await UnitOfWork.ExerciseRepository
                    .GetByAsync(where => exerciseGuids.Contains(where.Guid))
                    .ConfigureAwait(false);

                if (exercises.Count != exerciseGuids.Count)
                    throw new CustomException((int)MessagesCodesError.SystemError, "Uno o más ejercicios no fueron encontrados");
                // Crear la rutina
                var newRoutine = new Routine
                {
                    Guid = Guid.NewGuid(),
                    Name = request.Name,
                    CreationDate = Now,
                    UserId = UserId,
                    CreatedUserId = UserId
                };
                await UnitOfWork.RoutineRepository.AddAsync(newRoutine).ConfigureAwait(false);

                // Crear las relaciones con ejercicios
                var routineExercises = request.Exercises.Select(e =>
                {
                    var exercise = exercises.First(select => select.Guid == e.ExerciseGuid);
                    return new RoutineExercise
                    {
                        RoutineId = newRoutine.Id,
                        ExerciseId = exercise.Id,
                        Series = e.Series,
                        RepetitionsFrom = e.RepetitionsFrom,
                        RepetitionsTo = e.RepetitionsTo,
                        RestSeconds = e.RestSeconds,
                        Day = e.Day
                    };
                }).ToList();

                await UnitOfWork.RoutineExerciseRepository.AddRangeAsync(routineExercises).ConfigureAwait(false);

                return new CreateRoutineWithExercisesResponse
                {
                    RoutineGuid = newRoutine.Guid,
                    Name = newRoutine.Name,
                    ExercisesCount = routineExercises.Count,
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            }
        ).ConfigureAwait(false);
}
