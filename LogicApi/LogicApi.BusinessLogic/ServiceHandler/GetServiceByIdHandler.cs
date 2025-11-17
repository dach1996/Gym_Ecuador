using LogicApi.Model.Request.Service;
using LogicApi.Model.Response.Service;

namespace LogicApi.BusinessLogic.ServiceHandler;

/// <summary>
/// Handler para obtener servicio por ID
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetServiceByIdHandler(
    ILogger<GetServiceByIdHandler> logger,
    IPluginFactory pluginFactory) : ServiceBase<GetServiceByIdRequest, GetServiceByIdResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de un servicio por su ID
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetServiceByIdResponse> Handle(GetServiceByIdRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetServiceById, request, async () =>
            {
                // Buscar el servicio
                var service = await UnitOfWork.ServiceRepository
                    .GetByFirstOrDefaultAsync(where => where.Id == request.ServiceId)
                    .ConfigureAwait(false);

                if (service == null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "Servicio no encontrado");

                // Mapear a DTO
                var serviceDetail = new ServiceDetail
                {
                    Id = service.Id,
                    Name = service.Name,
                    Description = service.Description,
                    RequiresReservation = service.RequiresReservation,
                    IsActive = service.IsActive
                };

                return new GetServiceByIdResponse(serviceDetail)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            },
            registerLogAudit: false
        ).ConfigureAwait(false);
}

