using LogicAdministratorApi.Model.Request.Equipment;
using LogicAdministratorApi.Model.Response.Equipment;
using LogicCommon.Model.Response.File;

namespace LogicAdministratorApi.BusinessLogic.EquipmentHandler;

/// <summary>
/// Handler para obtener equipamiento por GUID
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetEquipmentByGuidHandler(
    ILogger<GetEquipmentByGuidHandler> logger,
    IPluginFactory pluginFactory) : EquipmentBase<GetEquipmentByGuidRequest, GetEquipmentByGuidResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de un equipamiento por GUID
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetEquipmentByGuidResponse> Handle(GetEquipmentByGuidRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.GetEquipmentByGuid, request, async () =>
            {
                // Buscar el equipamiento por GUID con las relaciones
                var equipment = await UnitOfWork.EquipmentRepository
                    .GetFirstOrDefaultGenericAsync(equipmentEntity =>
                        new EquipmentDetail
                        {
                            Guid = equipmentEntity.Guid,
                            GymBranchGuid = equipmentEntity.GymBranch.Guid,
                            Name = equipmentEntity.Name,
                            Description = equipmentEntity.Description,
                            EquipmentTypeCatalogId = equipmentEntity.EquipmentTypeCatalogId,
                            EquipmentTypeCode = equipmentEntity.EquipmentTypeCatalog.Code,
                            EquipmentTypeName = equipmentEntity.EquipmentTypeCatalog.Value,
                            IsActive = equipmentEntity.IsActive,
                            Images = equipmentEntity.EquipmentImages
                            .Where(image => image.FilePersistence.State)
                            .Select(image => new FileUrlResponse
                            {
                                Guid = image.FilePersistence.Guid,
                                Url = image.FilePersistence.FileBasePath.BaseUrl + image.FilePersistence.Path
                            }).ToList()
                        },
                        where => where.Guid == request.EquipmentGuid)
                    .ConfigureAwait(false)
                    ?? throw new CustomException((int)MessagesCodesError.SystemError, "Equipamiento no encontrado");

                return new GetEquipmentByGuidResponse(equipment)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            }).ConfigureAwait(false);
}
