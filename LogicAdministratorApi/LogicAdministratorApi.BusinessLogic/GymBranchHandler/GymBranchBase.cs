using Common.WebCommon.Helper;
using Common.WebCommon.Models.Enum;
using LogicCommon.Model.Request.File;
using PersistenceDb.Models.Core;

namespace LogicAdministratorApi.BusinessLogic.GymBranchHandler;

/// <summary>
/// Clase base para handlers de sucursal de gimnasio
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class GymBranchBase<TRequest, TResponse>(
    ILogger<GymBranchBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicAdministratorBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Procesa las imágenes de la sucursal
    /// </summary>
    /// <param name="images"></param>
    /// <param name="gymBranchId"></param>
    /// <returns></returns>
    protected async Task ProcessGymBranchFiles(List<RequestEncodeFile> images, int gymBranchId)
    {
        var fileBasePaths = await GetFileBasePathCacheInformationByPathCodeAsync(PathCode.GymBranchImage).ConfigureAwait(false);
        var folderPath = HelperPathName.GetGymBranchPathName(fileBasePaths.FileDirectoryPath, gymBranchId);
        await ProcessImagesAsync(
            images,
            PathCode.GymBranchImage,
            processCreateImagesAsync: async (images, response) =>
            {
                var imagePaths = response.Items.Select(
                    select => new GymBranchImage
                    {
                        GymBranchId = gymBranchId,
                        FilePersistenceId = select.Id,
                    }
                ).ToList();
                await UnitOfWork.GymBranchImageRepository.AddRangeIdentityAsync(imagePaths).ConfigureAwait(false);
            },
            beforeDeleteImagesAsync: async (images) =>
            {
                var imageGuids = images.Select(select => select.Guid.Value);
                var gymBranchImages = await UnitOfWork.GymBranchImageRepository.GetGenericAsync(
                     select => new { select.GymBranchId, select.FilePersistence.Guid },
                     where => imageGuids.Contains(where.FilePersistence.Guid) && where.FilePersistence.State).ConfigureAwait(false);
                if (gymBranchImages.Any(select => select.GymBranchId != gymBranchId, out var gymWithImageNotBelongToBranch))
                    throw new CustomException((int)MessagesCodesError.SystemError, $"Las imágenes con guid {gymWithImageNotBelongToBranch.Select(select => select.Guid).Join(", ")} no pertenecen a la sucursal");
            },
            processDeleteImagesAsync: async (images, response) =>
            {
                var imageIds = response.Items.Select(select => select.Id).ToList();
                await UnitOfWork.GymBranchImageRepository.DeleteAsync(where => imageIds.Contains(where.FilePersistenceId)).ConfigureAwait(false);
            },
            getFileExtension: (fileExtension) => HelperFileName.GetGymBranchImageName(fileExtension),
            folderPath: folderPath
        ).ConfigureAwait(false);
    }
}

