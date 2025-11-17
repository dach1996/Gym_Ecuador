using LogicApi.Model.Request.Service;
using LogicApi.Model.Response.Service;

namespace LogicApi.BusinessLogic.ServiceHandler;

/// <summary>
/// Handler para actualizar servicio
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class UpdateServiceHandler(
    ILogger<UpdateServiceHandler> logger,
    IPluginFactory pluginFactory) : ServiceBase<UpdateServiceRequest, UpdateServiceResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la actualización de un servicio
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<UpdateServiceResponse> Handle(UpdateServiceRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.UpdateService, request, async () =>
            {
                // Buscar el servicio por ID
                var service = await UnitOfWork.ServiceRepository
                    .GetByFirstOrDefaultAsync(where => where.Id == request.ServiceId)
                    .ConfigureAwait(false);

                if (service == null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "Servicio no encontrado");

                // Validar que no exista otro servicio con el mismo nombre (excluyendo el actual)
                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    var existingService = await UnitOfWork.ServiceRepository
                        .GetByFirstOrDefaultAsync(where => where.Name.ToLower() == request.Name.ToLower() 
                            && where.Id != service.Id)
                        .ConfigureAwait(false);

                    if (existingService != null)
                        throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe otro servicio con este nombre");

                    service.Name = request.Name;
                }

                // Actualizar los campos si se proporcionan
                if (!string.IsNullOrWhiteSpace(request.Description))
                    service.Description = request.Description;

                if (request.RequiresReservation.HasValue)
                    service.RequiresReservation = request.RequiresReservation.Value;

                if (request.IsActive.HasValue)
                    service.IsActive = request.IsActive.Value;

                return new UpdateServiceResponse(service.Id, service.Name)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            },
            registerLogAudit: true
        ).ConfigureAwait(false);
}

