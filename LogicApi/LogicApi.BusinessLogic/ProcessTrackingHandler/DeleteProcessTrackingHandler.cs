using LogicApi.Model.Request.ProcessTracking;
using LogicCommon.Model.Request.File;
using LogicCommon.Model.Response;

namespace LogicApi.BusinessLogic.ProcessTrackingHandler;

/// <summary>
/// Handler para eliminar seguimiento de proceso
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class DeleteProcessTrackingHandler(
    ILogger<DeleteProcessTrackingHandler> logger,
    IPluginFactory pluginFactory) : ProcessTrackingBase<DeleteProcessTrackingRequest, GenericCommonOperationResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la eliminación de un seguimiento de proceso
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GenericCommonOperationResponse> Handle(DeleteProcessTrackingRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.DeleteProcessTracking, request, async () =>
            {
                // Buscar el seguimiento de proceso por GUID
                var processTracking = await UnitOfWork.ProcessTrackingRepository
                    .GetFirstOrDefaultGenericAsync(
                        select => new
                        {
                            select.Id,
                            Images = select.ProcessTrackingImages.Select(image => image.FilePersistence.Guid)
                        },
                        where => where.Guid == request.ProcessTrackingGuid && where.UserId == UserId)
                    .ConfigureAwait(false) ?? throw new CustomException((int)MessagesCodesError.SystemError, "Seguimiento de proceso no encontrado");

                // Eliminar el seguimiento de proceso
                await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);
                await ProcessProcessTrackingImageFiles([.. processTracking.Images.Select(image => RequestEncodeFile.ToDelete(image))], processTracking.Id, UserId).ConfigureAwait(false);
                await UnitOfWork.ProcessTrackingMeasurementRepository.DeleteAsync(where => where.ProcessTrackingId == processTracking.Id).ConfigureAwait(false);
                await UnitOfWork.ProcessTrackingRepository.DeleteAsync(where => where.Id == processTracking.Id).ConfigureAwait(false);
                await UnitOfWork.CommitAsync().ConfigureAwait(false);

                return GenericCommonOperationResponse.SuccessOperation();
            }
        ).ConfigureAwait(false);
}

