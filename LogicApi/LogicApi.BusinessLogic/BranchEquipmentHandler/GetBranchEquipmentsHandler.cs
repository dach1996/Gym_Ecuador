using LogicApi.Model.Request.BranchEquipment;
using LogicApi.Model.Response.BranchEquipment;
using LogicCommon.Model.Response.File;

namespace LogicApi.BusinessLogic.BranchEquipmentHandler;

/// <summary>
/// Handler para obtener equipos de sucursal
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetBranchEquipmentsHandler(
    ILogger<GetBranchEquipmentsHandler> logger,
    IPluginFactory pluginFactory) : BranchEquipmentBase<GetBranchEquipmentsRequest, GetBranchEquipmentsResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de equipos de sucursal con paginación
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetBranchEquipmentsResponse> Handle(GetBranchEquipmentsRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetBranchEquipments, request, async () =>
            {
                // Obtener el ID de la sucursal por GUID
                var gymBranch = await UnitOfWork.GymBranchRepository
                    .GetFirstOrDefaultGenericAsync(
                        select => new { select.Id },
                        where => where.Guid == request.GymBranchGuid)
                    .ConfigureAwait(false)
                    ?? throw new CustomException((int)MessagesCodesError.SystemError, "Sucursal de gimnasio no encontrada");

                // Obtener datos paginados de equipos
                var paginatedResult = await UnitOfWork.EquipmentRepository
                    .GetPaginatorGenericAsync(
                        itemsByPage: request.PageSize,
                        page: request.PageNumber,
                        select => new BranchEquipmentItem
                        {
                            Guid = select.Guid,
                            Name = select.Name,
                            Description = select.Description,
                            EquipmentTypeCode = select.EquipmentTypeCatalog.Code,
                            EquipmentTypeName = select.EquipmentTypeCatalog.CatalogLanguages.FirstOrDefault().Name,
                            ImageUrls = select.EquipmentImages
                                .Where(image => image.FilePersistence.State)
                                .Select(image => new FileUrlResponse(image.FilePersistence.Guid, image.FilePersistence.FileBasePath.BaseUrl, image.FilePersistence.Path))
                                .ToList()
                        },
                        where => where.GymBranchId == gymBranch.Id && where.IsActive
                    ).ConfigureAwait(false);

                return new GetBranchEquipmentsResponse
                (
                    totalRegister: paginatedResult.TotalItems,
                    registers: paginatedResult.Items
                );
            }
        ).ConfigureAwait(false);
}

