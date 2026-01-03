using Common.WebCommon.Helper;
using Common.WebCommon.Models.Enum;
using LogicCommon.Model.Request.File;
using PersistenceDb.Models.Core;

namespace LogicAdministratorApi.BusinessLogic.EquipmentHandler;

/// <summary>
/// Clase base para handlers de equipamiento
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class EquipmentBase<TRequest, TResponse>(
    ILogger<EquipmentBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicAdministratorBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Procesa las imágenes del equipamiento
    /// </summary>
    /// <param name="images"></param>
    /// <param name="equipmentId"></param>
    /// <returns></returns>
    protected async Task ProcessEquipmentFiles(List<RequestEncodeFile> images, int equipmentId)
    {
        if (!images?.IsNullOrEmpty() ?? false)
        {
            var fileBasePaths = await GetFileBasePathCacheInformationByPathCodeAsync(PathCode.EquipmentImage).ConfigureAwait(false);
            var folderPath = HelperPathName.GetEquipmentPathName(fileBasePaths.FileDirectoryPath, equipmentId);
            await ProcessImagesAsync(
                images,
                PathCode.EquipmentImage,
                processCreateImagesAsync: async (images, response) =>
                {
                    var imagePaths = response.Items.Select(
                        select => new EquipmentImage
                        {
                            EquipmentId = equipmentId,
                            FilePersistenceId = select.Id,
                        }
                    ).ToList();
                    await UnitOfWork.EquipmentImageRepository.AddRangeIdentityAsync(imagePaths).ConfigureAwait(false);
                },
                beforeDeleteImagesAsync: async (images) =>
                {
                    var imageGuids = images.Select(select => select.Guid.Value);
                    var equipmentImages = await UnitOfWork.EquipmentImageRepository.GetGenericAsync(
                         select => new { select.EquipmentId, select.FilePersistence.Guid },
                         where => imageGuids.Contains(where.FilePersistence.Guid) && where.FilePersistence.State).ConfigureAwait(false);
                    if (equipmentImages.Any(select => select.EquipmentId != equipmentId, out var equipmentWithImageNotBelongToEquipment))
                        throw new CustomException((int)MessagesCodesError.SystemError, $"Las imágenes con guid {equipmentWithImageNotBelongToEquipment.Select(select => select.Guid).Join(", ")} no pertenecen al equipamiento");
                },
                processDeleteImagesAsync: async (images, response) =>
                {
                    var imageIds = response.Items.Select(select => select.Id).ToList();
                    await UnitOfWork.EquipmentImageRepository.DeleteAsync(where => imageIds.Contains(where.FilePersistenceId)).ConfigureAwait(false);
                },
                getFileExtension: (fileExtension) => HelperFileName.GetEquipmentImageName(fileExtension),
                folderPath: folderPath
            ).ConfigureAwait(false);
        }
    }
}
