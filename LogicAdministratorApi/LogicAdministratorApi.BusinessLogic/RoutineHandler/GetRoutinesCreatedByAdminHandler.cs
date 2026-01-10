using System.Linq.Expressions;
using LogicAdministratorApi.Model.Request.Routine;
using LogicAdministratorApi.Model.Response.Routine;
using Common.WebApi.Models.Enum;

namespace LogicAdministratorApi.BusinessLogic.RoutineHandler;

/// <summary>
/// Handler para obtener rutinas creadas por el administrador
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetRoutinesCreatedByAdminHandler(
    ILogger<GetRoutinesCreatedByAdminHandler> logger,
    IPluginFactory pluginFactory) : RoutineBase<GetRoutinesCreatedByAdminRequest, GetRoutinesCreatedByAdminResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de rutinas creadas por el administrador con paginación
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetRoutinesCreatedByAdminResponse> Handle(GetRoutinesCreatedByAdminRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.GetRoutinesCreatedByAdmin, request, async () =>
            {
                // Obtener el ID del administrador por GUID
                var adminUser = await UnitOfWork.UserRepository
                    .GetFirstOrDefaultGenericAsync(
                        select => new { select.Id },
                        where => where.Guid == CurrentUserGuid)
                    .ConfigureAwait(false)
                    ?? throw new CustomException((int)MessagesCodesError.SystemError, "Usuario administrador no encontrado");

                // Construir el filtro
                Expression<Func<PersistenceDb.Models.Core.Routine, bool>> whereFilter = 
                    routine => routine.CreatedUserId == adminUser.Id;

                if (!string.IsNullOrWhiteSpace(request.NameFilter))
                {
                    var nameFilter = request.NameFilter.ToLower();
                    whereFilter = routine => routine.CreatedUserId == adminUser.Id && routine.Name.ToLower().Contains(nameFilter);
                }

                if (request.UserGuidFilter.HasValue)
                {
                    var assignedUser = await UnitOfWork.UserRepository
                        .GetFirstOrDefaultGenericAsync(
                            select => new { select.Id },
                            where => where.Guid == request.UserGuidFilter.Value)
                        .ConfigureAwait(false);

                    if (assignedUser != null)
                    {
                        whereFilter = routine => routine.CreatedUserId == adminUser.Id && routine.UserId == assignedUser.Id;
                    }
                }

                // Obtener datos paginados de rutinas creadas por el admin
                var paginatedResult = await UnitOfWork.RoutineRepository
                    .GetPaginatorGenericAsync(
                        itemsByPage: request.PageSize,
                        page: request.PageNumber,
                        select => new RoutineAdminItem
                        {
                            Guid = select.Guid,
                            Name = select.Name,
                            CreationDate = select.CreationDate,
                            UserGuid = select.User.Guid,
                            UserName = select.User.Person != null 
                                ? $"{select.User.Person.RealNames ?? string.Empty} {select.User.Person.RealLastNames ?? string.Empty}".Trim()
                                : select.User.UserName,
                            UserEmail = select.User.Email,
                            ExerciseCount = select.RoutineExercises.Count
                        },
                        whereFilter,
                        orderBy: r => r.CreationDate,
                        orderByType: OrderByType.Desc
                    ).ConfigureAwait(false);

                return new GetRoutinesCreatedByAdminResponse(
                    totalRegister: paginatedResult.TotalItems,
                    registers: paginatedResult.Items
                )
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            }
        ).ConfigureAwait(false);
}
