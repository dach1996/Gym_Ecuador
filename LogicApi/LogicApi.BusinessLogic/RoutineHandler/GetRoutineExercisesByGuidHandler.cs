using LogicApi.Model.Request.Routine;
using LogicApi.Model.Response.Routine;
using Common.WebApi.Models.Enum;

namespace LogicApi.BusinessLogic.RoutineHandler;

/// <summary>
/// Handler para obtener ejercicios de rutina por GUID
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetRoutineExercisesByGuidHandler(
    ILogger<GetRoutineExercisesByGuidHandler> logger,
    IPluginFactory pluginFactory) : RoutineBase<GetRoutineExercisesByGuidRequest, GetRoutineExercisesByGuidResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de ejercicios de una rutina por GUID
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetRoutineExercisesByGuidResponse> Handle(GetRoutineExercisesByGuidRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetRoutineExercisesByGuid, request, async () =>
            {
                // Buscar la rutina por GUID con sus ejercicios relacionados
                var routine = await UnitOfWork.RoutineRepository
                    .GetByFirstOrDefaultAsync(
                        where => where.Guid == request.RoutineGuid,
                        include => include.RoutineExercises,
                        include => include.RoutineExercises.Select(re => re.Exercise))
                    .ConfigureAwait(false)
                    ?? throw new CustomException((int)MessagesCodesError.SystemError, "Rutina no encontrada");

                var routineDetail = new RoutineDetail
                {
                    Guid = routine.Guid,
                    Name = routine.Name,
                    CreationDate = routine.CreationDate,
                    Exercises = routine.RoutineExercises.Select(re => new RoutineExerciseDetail
                    {
                        ExerciseGuid = re.Exercise.Guid,
                        ExerciseName = re.Exercise.Name,
                        Series = re.Series,
                        RepetitionsFrom = re.RepetitionsFrom,
                        RepetitionsTo = re.RepetitionsTo,
                        RestSeconds = re.RestSeconds,
                        Day = re.Day
                    }).ToList()
                };

                return new GetRoutineExercisesByGuidResponse
                {
                    Routine = routineDetail
                };
            }
        ).ConfigureAwait(false);
}
