using Common.WebCommon.Helper;
using Common.WebCommon.Models.Enum;
using LogicAdministratorApi.Model.Request.GymBranch;
using LogicAdministratorApi.Model.Response.GymBranch;
using PersistenceDb.Models.Core;

namespace LogicAdministratorApi.BusinessLogic.GymBranchHandler;

/// <summary>
/// Handler para crear sucursal de gimnasio
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class CreateGymBranchHandler(
    ILogger<CreateGymBranchHandler> logger,
    IPluginFactory pluginFactory) : GymBranchBase<CreateGymBranchRequest, CreateGymBranchResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la creación de una sucursal de gimnasio
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<CreateGymBranchResponse> Handle(CreateGymBranchRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.CreateGymBranch, request, async () =>
            {
                // Validar que el gimnasio existe
                var gymId = await UnitOfWork.GymRepository
                    .GetFirstOrDefaultGenericAsync(
                        select => (int?)select.Id,
                        where => where.Guid == request.GymGuid)
                    .ConfigureAwait(false) ?? throw new CustomException((int)MessagesCodesError.GymBranchNotFound, "No se encontró el gimnasio especificado");

                // Validar que no exista una sucursal con el mismo nombre para este gimnasio
                if (await UnitOfWork.GymBranchRepository
                    .ExistAnyAsync(where => where.GymId == gymId && where.Name.ToLower() == request.Name.ToLower())
                    .ConfigureAwait(false))
                    throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe una sucursal con este nombre para el gimnasio");

                // Validar que el código no exista (si se proporciona)
                if (!string.IsNullOrEmpty(request.Code) && await UnitOfWork.GymBranchRepository
                        .ExistAnyAsync(where => where.Code.ToLower() == request.Code.ToLower())
                        .ConfigureAwait(false))
                    throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe una sucursal con este código");
                // Crear la nueva sucursal
                var newGymBranch = new GymBranch
                {
                    Guid = Guid.NewGuid(),
                    GymId = gymId,
                    Name = request.Name,
                    Code = request.Code,
                    Description = request.Description,
                    Address = request.Address,
                    Phone = request.Phone,
                    Email = request.Email,
                    Latitude = request.Latitude,
                    Longitude = request.Longitude,
                    MaxCapacity = request.MaxCapacity,
                    AreaSquareMeters = request.AreaSquareMeters,
                    FloorCount = request.FloorCount,
                    OpeningDate = request.OpeningDate,
                    IsActive = true,
                    DateTimeRegister = Now,
                };
                // Guardar en la base de datos
                var gymBranch = await UnitOfWork.GymBranchRepository.AddAsync(newGymBranch).ConfigureAwait(false);
                var fileBasePaths = await GetFileBasePathCacheInformationByPathCodeAsync(PathCode.GymBranchImage).ConfigureAwait(false);
                var folderPath = HelperPathName.GetGymBranchPathName(fileBasePaths.FileDirectoryPath, newGymBranch.Id);
                await ProcessImagesAsync(
                    request.Images,
                    PathCode.GymBranchImage,
                    processCreateImages: async (images, response) =>
                    {
                        var imagePaths = response.Items.Select(
                            select => new GymBranchImage
                            {
                                GymBranchId = gymBranch.Id,
                                FilePersistenceId = select.Id,
                            }
                        ).ToList();
                        await UnitOfWork.GymBranchImageRepository.AddRangeIdentityAsync(imagePaths).ConfigureAwait(false);
                    },
                    getFileExtension: (fileExtension) => HelperFileName.GetGymBranchImageName(fileExtension),
                    folderPath: folderPath
                ).ConfigureAwait(false);
                return new CreateGymBranchResponse(newGymBranch.Guid, newGymBranch.Name, newGymBranch.Code, request.GymGuid)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            }
        ).ConfigureAwait(false);


}

