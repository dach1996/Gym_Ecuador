using Common.WebCommon.Helper;
using Common.WebCommon.Models.Enum;
using LogicAdministratorApi.Model.Request.GymBranch;
using LogicAdministratorApi.Model.Response.GymBranch;
using LogicCommon.Model.Request.File;
using PersistenceDb.Models.Core;

namespace LogicAdministratorApi.BusinessLogic.GymBranchHandler;

/// <summary>
/// Handler para actualizar sucursal de gimnasio
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class UpdateGymBranchHandler(
    ILogger<UpdateGymBranchHandler> logger,
    IPluginFactory pluginFactory) : GymBranchBase<UpdateGymBranchRequest, UpdateGymBranchResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la actualización de una sucursal de gimnasio
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<UpdateGymBranchResponse> Handle(UpdateGymBranchRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.UpdateGymBranch, request, async () =>
        {
            // Buscar la sucursal por GUID
            var gymBranch = await UnitOfWork.GymBranchRepository
                .GetByFirstOrDefaultAsync(where => where.Guid == request.GymBranchGuid)
                .ConfigureAwait(false)
            ?? throw new CustomException((int)MessagesCodesError.SystemError, "Sucursal no encontrada");

            // Validar que el gimnasio existe
            var gym = await UnitOfWork.GymRepository
                .GetByFirstOrDefaultAsync(where => where.Guid == request.GymGuid)
                .ConfigureAwait(false)
            ?? throw new CustomException((int)MessagesCodesError.SystemError, "No se encontró el gimnasio especificado");

            // Validar que el nombre no esté vacío
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new CustomException((int)MessagesCodesError.SystemError, "El nombre de la sucursal es requerido");

            // Validar que no exista otra sucursal con el mismo nombre para este gimnasio (excluyendo la actual)
            if (await UnitOfWork.GymBranchRepository
                .ExistAnyAsync(where => where.GymId == gym.Id && where.Name.ToLower() == request.Name.ToLower() && where.Id != gymBranch.Id)
                .ConfigureAwait(false))
                throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe una sucursal con este nombre para el gimnasio");

            // Validar que el código no exista en otra sucursal (si se proporciona)
            if (!string.IsNullOrEmpty(request.Code) && await UnitOfWork.GymBranchRepository
                    .ExistAnyAsync(where => where.Code.ToLower() == request.Code.ToLower() && where.Id != gymBranch.Id)
                    .ConfigureAwait(false))
                throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe una sucursal con este código");

            // Actualizar los campos
            gymBranch.GymId = gym.Id;
            gymBranch.Name = request.Name;
            gymBranch.Code = request.Code;
            gymBranch.Description = request.Description;
            gymBranch.Address = request.Address;
            gymBranch.Phone = request.Phone;
            gymBranch.Email = request.Email;
            gymBranch.Latitude = request.Latitude;
            gymBranch.Longitude = request.Longitude;
            gymBranch.MaxCapacity = request.MaxCapacity;
            gymBranch.AreaSquareMeters = request.AreaSquareMeters;
            gymBranch.FloorCount = request.FloorCount;
            gymBranch.OpeningDate = request.OpeningDate;
            gymBranch.IsActive = request.IsActive;
            await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);
            await UnitOfWork.GymBranchRepository.UpdateAsync(gymBranch).ConfigureAwait(false);
            await ProcessGymBranchFiles(request.Images, gymBranch.Id).ConfigureAwait(false);
            await UnitOfWork.CommitAsync().ConfigureAwait(false);

            return new UpdateGymBranchResponse(gymBranch.Guid, gymBranch.Name, gymBranch.Code, gym.Guid)
            {
                UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                ShowMessage = true
            };
        }
        ).ConfigureAwait(false);


}

