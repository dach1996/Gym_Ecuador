using LogicAdministratorApi.Model.Request.Routine;
using LogicAdministratorApi.Model.Response.Routine;
using Common.WebApi.Models.Enum;

namespace LogicAdministratorApi.BusinessLogic.RoutineHandler;

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
        => await ExecuteHandlerAsync(OperationAdministratorName.GetRoutineExercisesByGuid, request, async () =>
            {
                // Buscar la rutina por GUID con sus ejercicios relacionados
                var routine = await UnitOfWork.RoutineRepository
                    .GetByFirstOrDefaultAsync(
                        where => where.Guid == request.RoutineGuid,
                        include => include.RoutineExercises,
                        include => include.RoutineExercises.Select(re => re.Exercise),
                        include => include.User,
                        include => include.User.Person)
                    .ConfigureAwait(false)
                    ?? throw new CustomException((int)MessagesCodesError.SystemError, "Rutina no encontrada");

                var routineDetail = new RoutineDetail
                {
                    Guid = routine.Guid,
                    Name = routine.Name,
                    CreationDate = routine.CreationDate,
                    UserGuid = routine.User.Guid,
                    UserName = routine.User.Person != null 
                        ? $"{routine.User.Person.RealNames ?? string.Empty} {routine.User.Person.RealLastNames ?? string.Empty}".Trim()
                        : routine.User.UserName,
                    Exercises = routine.RoutineExercises.Select(re => new RoutineExerciseDetail
                    {
                        ExerciseGuid = re.Exercise.Guid,
                        ExerciseName = re.Exercise.Name,
                        Series = re.Series,
                        RepetitionsFrom = re.RepetitionsFrom,
                        RepetitionsTo = re.RepetitionsTo,
                        RestSeconds = re.RestSeconds
                    }).ToList()
                };

                return new GetRoutineExercisesByGuidResponse
                {
                    Routine = routineDetail,
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            }
        ).ConfigureAwait(false);
}
