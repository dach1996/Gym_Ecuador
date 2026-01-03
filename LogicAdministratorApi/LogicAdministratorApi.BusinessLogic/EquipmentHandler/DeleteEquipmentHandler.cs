using LogicAdministratorApi.Model.Request.Equipment;
using LogicAdministratorApi.Model.Response.Equipment;
using LogicCommon.Model.Request.File;

namespace LogicAdministratorApi.BusinessLogic.EquipmentHandler;

/// <summary>
/// Handler para eliminar equipamiento
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class DeleteEquipmentHandler(
    ILogger<DeleteEquipmentHandler> logger,
    IPluginFactory pluginFactory) : EquipmentBase<DeleteEquipmentRequest, DeleteEquipmentResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la eliminación de un equipamiento
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<DeleteEquipmentResponse> Handle(DeleteEquipmentRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.DeleteEquipment, request, async () =>
            {
                // Buscar el equipamiento por GUID
                var equipment = await UnitOfWork.EquipmentRepository
                    .GetFirstOrDefaultGenericAsync(
                    select => new { select.Id, ImageGuids = select.EquipmentImages.Select(image => image.FilePersistence.Guid) },
                    where => where.Guid == request.EquipmentGuid)
                    .ConfigureAwait(false) ?? throw new CustomException((int)MessagesCodesError.SystemError, "Equipamiento no encontrado");

                // Eliminar el equipamiento
                await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);
                await ProcessEquipmentFiles(
                    [.. equipment.ImageGuids.Select(guid => RequestEncodeFile.ToDelete(guid))],
                    equipment.Id).ConfigureAwait(false);
                await UnitOfWork.EquipmentImageRepository.DeleteAsync(where => where.EquipmentId == equipment.Id).ConfigureAwait(false);
                await UnitOfWork.EquipmentRepository.DeleteAsync(where => where.Id == equipment.Id).ConfigureAwait(false);
                await UnitOfWork.CommitAsync().ConfigureAwait(false);

                return new DeleteEquipmentResponse()
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            }
        ).ConfigureAwait(false);
}

