using LogicApi.Model.Request.ProcessTracking;
using LogicApi.Model.Response.ProcessTracking;

namespace LogicApi.BusinessLogic.ProcessTrackingHandler;

/// <summary>
/// Handler para actualizar seguimiento de proceso
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class UpdateProcessTrackingHandler(
    ILogger<UpdateProcessTrackingHandler> logger,
    IPluginFactory pluginFactory) : ProcessTrackingBase<UpdateProcessTrackingRequest, UpdateProcessTrackingResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la actualización de un seguimiento de proceso
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<UpdateProcessTrackingResponse> Handle(UpdateProcessTrackingRequest request, CancellationToken cancellationToken)
    {
        return await ExecuteHandlerAsync(
            OperationApiName.UpdateProcessTracking,
            request,
            async () =>
            {
                // Buscar el seguimiento de proceso por GUID
                var processTracking = await UnitOfWork.ProcessTrackingRepository
                    .GetByFirstOrDefaultAsync(where => where.Guid == request.ProcessTrackingGuid)
                    .ConfigureAwait(false);

                if (processTracking == null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "Seguimiento de proceso no encontrado");

        

                // Guardar cambios
                await UnitOfWork.ProcessTrackingRepository.UpdateAsync(processTracking).ConfigureAwait(false);

                return new UpdateProcessTrackingResponse(processTracking.Guid, "")
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            },
            registerLogAudit: true
        ).ConfigureAwait(false);
    }
}
