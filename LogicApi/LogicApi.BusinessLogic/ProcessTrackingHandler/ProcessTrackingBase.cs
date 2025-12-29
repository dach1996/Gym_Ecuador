using Common.WebCommon.Helper;
using Common.WebCommon.Models.Enum;
using LogicCommon.Model.Request.File;
using PersistenceDb.Models.Core;

namespace LogicApi.BusinessLogic.ProcessTrackingHandler;

/// <summary>
/// Clase base para handlers de seguimiento de procesos
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class ProcessTrackingBase<TRequest, TResponse>(
    ILogger<ProcessTrackingBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);


    /// <summary>
    /// Procesa las imágenes de la sucursal
    /// </summary>
    /// <param name="images"></param>
    /// <param name="userId"></param>
    /// <returns></returns>
    protected async Task ProcessProcessTrackingImageFiles(List<RequestEncodeFile> images, int processTrackingId, int registerUserId)
    {
        var fileBasePaths = await GetFileBasePathCacheInformationByPathCodeAsync(PathCode.ProcessTrackingImage).ConfigureAwait(false);
        var folderPath = HelperPathName.GetProcessTrackingPathName(fileBasePaths.FileDirectoryPath, processTrackingId);
        await ProcessImagesAsync(
                images,
                PathCode.ProcessTrackingImage,
                processCreateImagesAsync: async (images, response) =>
                    await UnitOfWork.ProcessTrackingImageRepository.AddRangeAsync([.. response.Items.Select(select => new ProcessTrackingImage
                {
                    ProcessTrackingId = processTrackingId,
                    FilePersistenceId = select.Id,
                    DateTimeRegister = Now,
                    UserIdRegister = registerUserId
                })]).ConfigureAwait(false),
                beforeDeleteImagesAsync: async (images) =>
                {
                    var processTrackingImageGuids = images.Select(select => select.Guid.Value);
                    var processTrackingImages = await UnitOfWork.ProcessTrackingImageRepository.GetGenericAsync(
                        select => new { select.ProcessTrackingId, select.FilePersistence.Guid },
                        where => processTrackingImageGuids.Contains(where.FilePersistence.Guid) && where.FilePersistence.State).ConfigureAwait(false);
                    if (processTrackingImages.Any(select => select.ProcessTrackingId != processTrackingId, out var processTrackingImageNotBelongToProcessTracking))
                        throw new CustomException((int)MessagesCodesError.SystemError, $"Las imágenes con guid {processTrackingImageNotBelongToProcessTracking.Select(select => select.Guid).Join(", ")} no pertenecen al seguimiento de proceso");
                },
                processDeleteImagesAsync: async (_, response) =>
                {
                    var responseIds = response.Items.Select(select => select.Id).ToList();
                    await UnitOfWork.ProcessTrackingImageRepository.DeleteAsync(where => responseIds.Contains(where.FilePersistenceId)).ConfigureAwait(false);
                },
                getFileExtension: (fileExtension) => HelperFileName.GetProcessTrackingImageName(fileExtension),
                folderPath: folderPath
            ).ConfigureAwait(false);
    }
}
