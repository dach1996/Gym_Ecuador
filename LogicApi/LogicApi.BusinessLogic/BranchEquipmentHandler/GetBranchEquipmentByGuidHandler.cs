using LogicApi.Model.Request.BranchEquipment;
using LogicApi.Model.Response.BranchEquipment;
using LogicCommon.Model.Response.File;

namespace LogicApi.BusinessLogic.BranchEquipmentHandler;

/// <summary>
/// Handler para obtener detalle de equipo de sucursal por GUID
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetBranchEquipmentByGuidHandler(
    ILogger<GetBranchEquipmentByGuidHandler> logger,
    IPluginFactory pluginFactory) : BranchEquipmentBase<GetBranchEquipmentByGuidRequest, GetBranchEquipmentByGuidResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de detalle de un equipo por GUID
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetBranchEquipmentByGuidResponse> Handle(GetBranchEquipmentByGuidRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetBranchEquipmentByGuid, request, async () =>
            {
                // Buscar el equipo por GUID
                var equipment = (await UnitOfWork.EquipmentRepository
                    .GetFirstOrDefaultGenericAsync(
                        select => new BranchEquipmentDetail
                        {
                            Guid = select.Guid,
                            GymBranchGuid = select.GymBranch.Guid,
                            Name = select.Name,
                            Description = select.Description,
                            EquipmentTypeCode = select.EquipmentTypeCatalog.Code,
                            EquipmentTypeName = select.EquipmentTypeCatalog.CatalogLanguages.FirstOrDefault().Name,
                            IsActive = select.IsActive,
                            ImageUrls = select.EquipmentImages
                                .Where(image => image.FilePersistence.State)
                                .Select(image => new FileUrlResponse(image.FilePersistence.Guid, image.FilePersistence.FileBasePath.BaseUrl, image.FilePersistence.Path))
                                .ToList()
                        },
                        where => where.Guid == request.EquipmentGuid)
                    .ConfigureAwait(false))
                    ?? throw new CustomException((int)MessagesCodesError.SystemError, "Equipo de sucursal no encontrado");

                return new GetBranchEquipmentByGuidResponse(equipment);
            }).ConfigureAwait(false);
}

