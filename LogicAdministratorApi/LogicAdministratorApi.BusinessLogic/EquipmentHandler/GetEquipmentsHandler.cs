using System.Linq.Expressions;
using LogicAdministratorApi.Model.Request.Equipment;
using LogicAdministratorApi.Model.Response.Equipment;
using LogicCommon.Model.Response.File;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Models;

namespace LogicAdministratorApi.BusinessLogic.EquipmentHandler;

/// <summary>
/// Handler para obtener equipamientos paginados
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetEquipmentsHandler(
    ILogger<GetEquipmentsHandler> logger,
    IPluginFactory pluginFactory) : EquipmentBase<GetEquipmentsRequest, GetEquipmentsResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de equipamientos con paginación
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetEquipmentsResponse> Handle(GetEquipmentsRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.GetEquipmentsPaginated, request, async () =>
            {
                // Validar que la sucursal existe
                var gymBranchId = await UnitOfWork.GymBranchRepository
                    .GetFirstOrDefaultGenericAsync(
                        select => (int?)select.Id,
                        where => where.Guid == request.GymBranchGuid)
                    .ConfigureAwait(false) ?? throw new CustomException((int)MessagesCodesError.GymBranchNotFound, "No se encontró la sucursal de gimnasio especificada");

                // Obtener IDs de catálogos que coinciden con el filtro de tipo si existe
                var catalogIds = new List<int>();
                if (!string.IsNullOrWhiteSpace(request.TypeFilter))
                {
                    var typeFilter = request.TypeFilter.ToLower();
                    catalogIds = (await UnitOfWork.CatalogRepository
                        .GetGenericAsync(
                            select => select.Id,
                            where => where.Code.ToLower().Contains(typeFilter) || where.Value.ToLower().Contains(typeFilter)
                        ).ConfigureAwait(false)).ToList();

                    if (catalogIds.Count == 0)
                    {
                        // Si no hay catálogos que coincidan, retornar lista vacía
                        return new GetEquipmentsResponse(0, new List<EquipmentItem>())
                        {
                            UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                            ShowMessage = false
                        };
                    }
                }

                // Construir el filtro where combinando todas las condiciones
                var nameFilter = request.NameFilter?.ToLower();
                var isActiveFilter = request.IsActiveFilter;

                Expression<Func<Equipment, bool>> whereClause = eq =>
                    eq.GymBranchId == gymBranchId &&
                    (string.IsNullOrWhiteSpace(nameFilter) || eq.Name.ToLower().Contains(nameFilter)) &&
                    (!isActiveFilter.HasValue || eq.IsActive == isActiveFilter.Value) &&
                    (catalogIds.Count == 0 || catalogIds.Contains(eq.EquipmentTypeCatalogId));

                var paginatedResult = await UnitOfWork.EquipmentRepository
                .GetPaginatorGenericAsync(
                    itemsByPage: request.PageSize,
                    page: request.PageNumber,
                    select => new EquipmentItem
                    {
                        Guid = select.Guid,
                        Name = select.Name,
                        Description = select.Description,
                        EquipmentTypeCode = select.EquipmentTypeCatalog.Code,
                        EquipmentTypeName = select.EquipmentTypeCatalog.CatalogLanguages.FirstOrDefault().Name,
                        IsActive = select.IsActive,
                        Image = select.EquipmentImages.Where(image => image.FilePersistence.State)
                        .Select(image => new FileUrlResponse
                        {
                            Guid = image.FilePersistence.Guid,
                            Url = image.FilePersistence.FileBasePath.BaseUrl + image.FilePersistence.Path
                        }).FirstOrDefault()
                    },
                    where: whereClause,
                    orderBy: eq => eq.Name,
                    orderByType: OrderByType.Asc
                ).ConfigureAwait(false);



                return new GetEquipmentsResponse(paginatedResult.TotalItems, paginatedResult.Items)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            }
        ).ConfigureAwait(false);
}
