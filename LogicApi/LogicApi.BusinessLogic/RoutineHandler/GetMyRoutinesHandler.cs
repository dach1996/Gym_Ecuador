using LogicApi.Model.Request.Routine;
using LogicApi.Model.Response.Routine;
using Common.WebApi.Models.Enum;

namespace LogicApi.BusinessLogic.RoutineHandler;

/// <summary>
/// Handler para obtener mis rutinas
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetMyRoutinesHandler(
    ILogger<GetMyRoutinesHandler> logger,
    IPluginFactory pluginFactory) : RoutineBase<GetMyRoutinesRequest, GetMyRoutinesResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de rutinas del usuario autenticado con paginación
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetMyRoutinesResponse> Handle(GetMyRoutinesRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetMyRoutines, request, async () =>
            {
                // Obtener datos paginados de rutinas del usuario
                var paginatedResult = await UnitOfWork.RoutineRepository
                    .GetPaginatorGenericAsync(
                        itemsByPage: request.PageSize,
                        page: request.PageNumber,
                        select => new RoutineItem
                        {
                            Guid = select.Guid,
                            Name = select.Name,
                            CreationDate = select.CreationDate,
                            ExerciseCount = select.RoutineExercises.Count
                        },
                        where => where.UserId == UserId,
                        orderBy: order => order.Id,
                        orderByType: OrderByType.Desc
                    ).ConfigureAwait(false);

                return new GetMyRoutinesResponse(
                    totalRegister: paginatedResult.TotalItems,
                    registers: paginatedResult.Items
                );
            }
        ).ConfigureAwait(false);
}
