using System.Linq.Expressions;
using LogicAdministratorApi.Model.Request.BranchPlan;
using LogicAdministratorApi.Model.Response.BranchPlan;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Models;

namespace LogicAdministratorApi.BusinessLogic.BranchPlanHandler;

/// <summary>
/// Handler para obtener planes de sucursal paginados
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetBranchPlansHandler(
    ILogger<GetBranchPlansHandler> logger,
    IPluginFactory pluginFactory) : BranchPlanBase<GetBranchPlansRequest, GetBranchPlansResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de planes de sucursal con paginación
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetBranchPlansResponse> Handle(GetBranchPlansRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.GetBranchPlansPaginated, request, async () =>
            {
                // Validar que la sucursal existe
                var gymBranchId = await UnitOfWork.GymBranchRepository
                    .GetFirstOrDefaultGenericAsync(
                        select => (int?)select.Id,
                        where => where.Guid == request.GymBranchGuid)
                    .ConfigureAwait(false) ?? throw new CustomException((int)MessagesCodesError.GymBranchNotFound, "No se encontró la sucursal de gimnasio especificada");

                // Construir el filtro where combinando todas las condiciones
                var nameFilter = request.NameFilter?.ToLower();
                var codeFilter = request.CodeFilter?.ToLower();
                var isActiveFilter = request.IsActiveFilter;

                Expression<Func<BranchPlan, bool>> whereClause = bp =>
                    bp.GymBranchId == gymBranchId &&
                    (string.IsNullOrWhiteSpace(nameFilter) || bp.Name.ToLower().Contains(nameFilter)) &&
                    (string.IsNullOrWhiteSpace(codeFilter) || (bp.Code != null && bp.Code.ToLower().Contains(codeFilter))) &&
                    (!isActiveFilter.HasValue || bp.IsActive == isActiveFilter.Value);

                var paginatedResult = await UnitOfWork.BranchPlanRepository
                .GetPaginatorGenericAsync(
                    itemsByPage: request.PageSize,
                    page: request.PageNumber,
                    select => new BranchPlanItem
                    {
                        Guid = select.Guid,
                        Name = select.Name,
                        Code = select.Code,
                        Description = select.Description,
                        Price = select.Price,
                        DurationDays = select.DurationDays,
                        EnrollmentFee = select.EnrollmentFee,
                        IsActive = select.IsActive
                    },
                    where: whereClause,
                    orderBy: bp => bp.Name,
                    orderByType: OrderByType.Asc
                ).ConfigureAwait(false);

                return new GetBranchPlansResponse(paginatedResult.TotalItems, paginatedResult.Items)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            }
        ).ConfigureAwait(false);
}

