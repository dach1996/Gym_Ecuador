using LogicAdministratorApi.Model.Request.Equipment;
using LogicAdministratorApi.Model.Response.Equipment;

namespace LogicAdministratorApi.BusinessLogic.EquipmentHandler;

/// <summary>
/// Handler para actualizar equipamiento
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class UpdateEquipmentHandler(
    ILogger<UpdateEquipmentHandler> logger,
    IPluginFactory pluginFactory) : EquipmentBase<UpdateEquipmentRequest, UpdateEquipmentResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la actualización de un equipamiento
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<UpdateEquipmentResponse> Handle(UpdateEquipmentRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.UpdateEquipment, request, async () =>
        {
            // Buscar el equipamiento por GUID
            var equipment = await UnitOfWork.EquipmentRepository
                .GetByFirstOrDefaultAsync(where => where.Guid == request.EquipmentGuid)
                .ConfigureAwait(false)
            ?? throw new CustomException((int)MessagesCodesError.SystemError, "Equipamiento no encontrado");

            // Validar que el nombre no esté vacío
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new CustomException((int)MessagesCodesError.SystemError, "El nombre del equipamiento es requerido");

            // Validar que no exista otro equipamiento con el mismo nombre para esta sucursal (excluyendo el actual)
            if (await UnitOfWork.EquipmentRepository
                .ExistAnyAsync(where => where.Name.ToLower() == request.Name.ToLower() && where.Id != equipment.Id)
                .ConfigureAwait(false))
                throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe un equipamiento con este nombre para la sucursal");

            var equipmentTypeCatalogId = await UnitOfWork.CatalogRepository
                .GetFirstOrDefaultGenericAsync(
                select => (int?)select.Id,
                where => where.Code == request.EquipmentTypeCatalogCode)
                .ConfigureAwait(false)
            ?? throw new CustomException((int)MessagesCodesError.SystemError, "El tipo de equipamiento especificado no existe");
            // Actualizar los campos
            equipment.Name = request.Name;
            equipment.Description = request.Description;
            equipment.EquipmentTypeCatalogId = equipmentTypeCatalogId;
            equipment.IsActive = request.IsActive;

            await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);
            await UnitOfWork.EquipmentRepository.UpdateAsync(equipment).ConfigureAwait(false);
            await ProcessEquipmentFiles(request.Images, equipment.Id).ConfigureAwait(false);
            await UnitOfWork.CommitAsync().ConfigureAwait(false);

            return new UpdateEquipmentResponse(equipment.Guid, equipment.Name)
            {
                UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                ShowMessage = true
            };
        }
        ).ConfigureAwait(false);
}
