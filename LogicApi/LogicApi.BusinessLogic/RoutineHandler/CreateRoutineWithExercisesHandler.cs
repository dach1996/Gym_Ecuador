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

                // Obtener el ID del usuario por GUID
                var user = await UnitOfWork.UserRepository
                    .GetFirstOrDefaultGenericAsync(
                        select => new { select.Id },
                        where => where.Guid == UserGuid)
                    .ConfigureAwait(false)
                    ?? throw new CustomException((int)MessagesCodesError.SystemError, "Usuario no encontrado");

                // Crear la rutina
                var newRoutine = new Routine
                {
                    Guid = Guid.NewGuid(),
                    Name = request.Name,
                    CreationDate = Now,
                    UserId = user.Id,
                    CreatedUserId = user.Id
                };

                await UnitOfWork.RoutineRepository.AddAsync(newRoutine).ConfigureAwait(false);

                // Crear las relaciones con ejercicios
                var routineExercises = request.Exercises.Select(e =>
                {
                    var exercise = exercises.First(ex => ex.Guid == e.ExerciseGuid);
                    return new RoutineExercise
                    {
                        RoutineId = newRoutine.Id,
                        ExerciseId = exercise.Id,
                        Series = e.Series,
                        RepetitionsFrom = e.RepetitionsFrom,
                        RepetitionsTo = e.RepetitionsTo,
                        RestSeconds = e.RestSeconds
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
