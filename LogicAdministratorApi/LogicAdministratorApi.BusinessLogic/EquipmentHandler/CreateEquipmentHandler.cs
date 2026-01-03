using LogicAdministratorApi.Model.Request.Equipment;
using LogicAdministratorApi.Model.Response.Equipment;
using PersistenceDb.Models.Core;

namespace LogicAdministratorApi.BusinessLogic.EquipmentHandler;

/// <summary>
/// Handler para crear equipamiento
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class CreateEquipmentHandler(
    ILogger<CreateEquipmentHandler> logger,
    IPluginFactory pluginFactory) : EquipmentBase<CreateEquipmentRequest, CreateEquipmentResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la creación de un equipamiento
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<CreateEquipmentResponse> Handle(CreateEquipmentRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.CreateEquipment, request, async () =>
            {
                // Validar que la sucursal existe
                var gymBranchId = await UnitOfWork.GymBranchRepository
                    .GetFirstOrDefaultGenericAsync(
                        select => (int?)select.Id,
                        where => where.Guid == request.GymBranchGuid)
                    .ConfigureAwait(false) ?? throw new CustomException((int)MessagesCodesError.GymBranchNotFound, "No se encontró la sucursal de gimnasio especificada");

                // Validar que no exista un equipamiento con el mismo nombre para esta sucursal
                if (await UnitOfWork.EquipmentRepository
                    .ExistAnyAsync(where => where.GymBranchId == gymBranchId && where.Name.ToLower() == request.Name.ToLower())
                    .ConfigureAwait(false))
                    throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe un equipamiento con este nombre para la sucursal");

                // Validar que el tipo de equipamiento existe en el catálogo
                var equipmentTypeCatalogId = await UnitOfWork.CatalogRepository
                    .GetFirstOrDefaultGenericAsync(
                        select => (int?)select.Id,
                        where => where.Code == request.EquipmentTypeCatalogCode)
                    .ConfigureAwait(false) ?? throw new CustomException((int)MessagesCodesError.SystemError, $"El tipo de equipamiento con código: {request.EquipmentTypeCatalogCode} no existe");

                // Crear el nuevo equipamiento
                var newEquipment = new Equipment
                {
                    Guid = Guid.NewGuid(),
                    GymBranchId = gymBranchId,
                    Name = request.Name,
                    Description = request.Description,
                    EquipmentTypeCatalogId = equipmentTypeCatalogId,
                    IsActive = true
                };

                // Guardar en la base de datos
                await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);
                var equipment = await UnitOfWork.EquipmentRepository.AddAsync(newEquipment).ConfigureAwait(false);
                await ProcessEquipmentFiles(request.Images, equipment.Id).ConfigureAwait(false);
                await UnitOfWork.CommitAsync().ConfigureAwait(false);

                return new CreateEquipmentResponse(newEquipment.Guid, newEquipment.Name, request.GymBranchGuid)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            }
        ).ConfigureAwait(false);
}
