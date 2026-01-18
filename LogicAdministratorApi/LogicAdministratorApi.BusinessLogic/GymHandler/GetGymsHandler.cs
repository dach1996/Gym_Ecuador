using System.Linq.Expressions;
using LogicAdministratorApi.Model.Request.Gym;
using LogicAdministratorApi.Model.Response.Gym;
using PersistenceDb.Models.Core;

namespace LogicAdministratorApi.BusinessLogic.GymHandler;

/// <summary>
/// Handler para obtener gimnasios paginados
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetGymsHandler(
    ILogger<GetGymsHandler> logger,
    IPluginFactory pluginFactory) : GymBase<GetGymsRequest, GetGymsResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de gimnasios con paginación
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetGymsResponse> Handle(GetGymsRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.GetGymsPaginated, request, async () =>
            {
                // Construir el filtro where combinando todas las condiciones
                var nameFilter = request.Filter?.ToLower();
                var whereClause = new List<Expression<Func<Gym, bool>>>
                {
                    {!request.Filter.IsNullOrEmpty(), where => where.Name.ToLower().Contains(nameFilter)},
                    {request.GymGuid.HasValue, where => where.Guid == request.GymGuid.Value},
                    {request.GymBranchGuid.HasValue, where => where.GymBranches.Any(gb => gb.Guid == request.GymBranchGuid.Value)},
                };
                // Obtener datos paginados
                var paginatedResult = await UnitOfWork.GymRepository
                    .GetPaginatorGenericAsync(
                        itemsByPage: request.PageSize,
                        page: request.Page,
                        select => new GymItem
                        {
                            Guid = select.Guid,
                            Name = select.Name,
                            Phone = select.Phone,
                            IsActive = select.IsActive == GymStatus.Active,
                            DateTimeRegister = select.DateTimeRegister,
                            GymBranches = select.GymBranches.Select(gb => new GymItem.GymBranchPartialItem
                            {
                                Guid = gb.Guid,
                                Name = gb.Name,
                                MemberCount = gb.ClientGymBranches.Count
                            })
                        },
                        where: whereClause,
                        orderBy: g => g.Name,
                        orderByType: OrderByType.Asc
                    ).ConfigureAwait(false);
                return new GetGymsResponse(paginatedResult.TotalItems, paginatedResult.Items)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            }
        ).ConfigureAwait(false);
}

