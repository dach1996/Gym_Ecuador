using System.Linq.Expressions;
using LogicAdministratorApi.Model.Request.GymBranch;
using LogicAdministratorApi.Model.Response.GymBranch;
using PersistenceDb.Models.Core;

namespace LogicAdministratorApi.BusinessLogic.GymBranchHandler;

/// <summary>
/// Handler para obtener sucursales de gimnasio paginadas
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetGymBranchesHandler(
    ILogger<GetGymBranchesHandler> logger,
    IPluginFactory pluginFactory) : GymBranchBase<GetGymBranchesRequest, GetGymBranchesResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de sucursales de gimnasio con paginación
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetGymBranchesResponse> Handle(GetGymBranchesRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.GetGymBranchesPaginated, request, async () =>
            {
                // Validar que el gimnasio existe
                var gymId = await UnitOfWork.GymRepository
                    .GetFirstOrDefaultGenericAsync(
                        select => (int?)select.Id,
                        where => where.Guid == request.GymGuid)
                    .ConfigureAwait(false) ?? throw new CustomException((int)MessagesCodesError.GymBranchNotFound, "No se encontró el gimnasio especificado");

                // Construir el filtro where combinando todas las condiciones
                var nameFilter = request.NameFilter?.ToLower();
                var isActiveFilter = request.IsActiveFilter;

                Expression<Func<GymBranch, bool>> whereClause = gb =>
                    gb.GymId == gymId &&
                    (string.IsNullOrWhiteSpace(nameFilter) || gb.Name.ToLower().Contains(nameFilter)) &&
                    (!isActiveFilter.HasValue || gb.IsActive == isActiveFilter.Value);

                // Obtener datos paginados
                var paginatedResult = await UnitOfWork.GymBranchRepository
                    .GetPaginatorGenericAsync(
                        itemsByPage: request.PageSize,
                        page: request.PageNumber,
                        select => new GymBranchItem
                        {
                            Guid = select.Guid,
                            Name = select.Name,
                            Code = select.Code,
                            Address = select.Address,
                            Phone = select.Phone,
                            Email = select.Email,
                            IsActive = select.IsActive,
                            DateTimeRegister = select.DateTimeRegister,
                            OpeningDate = select.OpeningDate
                        },
                        where: whereClause,
                        orderBy: gb => gb.Name,
                        orderByType: OrderByType.Asc
                    ).ConfigureAwait(false);

                return new GetGymBranchesResponse(paginatedResult.TotalItems, paginatedResult.Items)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            }
        ).ConfigureAwait(false);
}

