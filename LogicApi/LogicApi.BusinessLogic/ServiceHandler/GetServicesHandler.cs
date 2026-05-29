using LogicApi.Model.Request.Service;
using LogicApi.Model.Response.Service;

namespace LogicApi.BusinessLogic.ServiceHandler;

/// <summary>
/// Handler para obtener servicios
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetServicesHandler(
    ILogger<GetServicesHandler> logger,
    IPluginFactory pluginFactory) : ServiceBase<GetServicesRequest, GetServicesResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de servicios con paginación
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetServicesResponse> Handle(GetServicesRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetServices, request, async () =>
            {
                // Obtener el total de registros
                var totalRecords = await UnitOfWork.ServiceRepository.CountAsync(where => true).ConfigureAwait(false);

                // Aplicar paginación
                var services = await UnitOfWork.ServiceRepository.GetByAsync(where => true)
                    .ConfigureAwait(false);

                // Mapear a DTOs
                var serviceItems = services.Select(s => new ServiceItem
                {
                    Id = s.Id,
                    Name = s.Name,
                    Description = s.Description,
                    RequiresReservation = s.RequiresReservation,
                    IsActive = s.IsActive
                });

                return new GetServicesResponse(serviceItems, totalRecords, request.Page, request.PageSize)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            },
            registerLogAudit: false
        ).ConfigureAwait(false);
}

