using LogicAdministratorApi.Model.Request.Routine;
using LogicAdministratorApi.Model.Response.Routine;
using PersistenceDb.Models.Core;
using Common.WebApi.Models.Enum;

namespace LogicAdministratorApi.BusinessLogic.RoutineHandler;

/// <summary>
/// Handler para crear rutina con ejercicios asignada a un usuario
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class CreateRoutineWithExercisesForUserHandler(
    ILogger<CreateRoutineWithExercisesForUserHandler> logger,
    IPluginFactory pluginFactory) : RoutineBase<CreateRoutineWithExercisesForUserRequest, CreateRoutineWithExercisesForUserResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la creación de una rutina con ejercicios asignada a un usuario
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<CreateRoutineWithExercisesForUserResponse> Handle(CreateRoutineWithExercisesForUserRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.CreateRoutineWithExercisesForUser, request, async () =>
            {
                // Validar que existan los ejercicios
                var exerciseGuids = request.Exercises.Select(e => e.ExerciseGuid).ToList();
                var exercises = await UnitOfWork.ExerciseRepository
                    .GetByAsync(where => exerciseGuids.Contains(where.Guid))
                    .ConfigureAwait(false);

                if (exercises.Count != exerciseGuids.Count)
                    throw new CustomException((int)MessagesCodesError.SystemError, "Uno o más ejercicios no fueron encontrados");

                // Obtener el ID del administrador por GUID
                var adminUser = await UnitOfWork.UserRepository
                    .GetFirstOrDefaultGenericAsync(
                        select => new { select.Id },
                        where => where.Guid == CurrentUserGuid)
                    .ConfigureAwait(false)
                    ?? throw new CustomException((int)MessagesCodesError.SystemError, "Usuario administrador no encontrado");

                // Obtener el ID del usuario al que se asigna la rutina
                var assignedUser = await UnitOfWork.UserRepository
                    .GetFirstOrDefaultGenericAsync(
                        select => new { select.Id, select.Guid },
                        where => where.Guid == request.UserGuid)
                    .ConfigureAwait(false)
                    ?? throw new CustomException((int)MessagesCodesError.SystemError, "Usuario asignado no encontrado");

                // Crear la rutina
                var newRoutine = new Routine
                {
                    Guid = Guid.NewGuid(),
                    Name = request.Name,
                    CreationDate = Now,
                    UserId = assignedUser.Id,
                    CreatedUserId = adminUser.Id
                };

                await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);
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
                await UnitOfWork.CommitAsync().ConfigureAwait(false);

                return new CreateRoutineWithExercisesForUserResponse
                {
                    RoutineGuid = newRoutine.Guid,
                    Name = newRoutine.Name,
                    UserGuid = assignedUser.Guid,
                    ExercisesCount = routineExercises.Count,
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            }
        ).ConfigureAwait(false);
}
