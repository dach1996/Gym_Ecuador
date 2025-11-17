using LogicApi.Model.Request.Service;
using LogicApi.Model.Response.Service;
using Microsoft.EntityFrameworkCore;

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
                // Construir query con filtros
                var query = await UnitOfWork.ServiceRepository.GetByAsync(where => where.Name.Contains(request.NameFilter))
                    .ConfigureAwait(false);

                // Filtrar por nombre si se proporciona
                if (!string.IsNullOrWhiteSpace(request.NameFilter))
                    query = await UnitOfWork.ServiceRepository.GetByAsync(where => where.Name.Contains(request.NameFilter))
                        .ConfigureAwait(false);

                // Filtrar por estado si se proporciona
                if (request.IsActiveFilter.HasValue)
                    query = await UnitOfWork.ServiceRepository.GetByAsync(where => where.IsActive == request.IsActiveFilter.Value)
                        .ConfigureAwait(false);

                // Filtrar por requiere reserva si se proporciona
                if (request.RequiresReservationFilter.HasValue)
                    query = await UnitOfWork.ServiceRepository.GetByAsync(where => where.RequiresReservation == request.RequiresReservationFilter.Value)
                        .ConfigureAwait(false);

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

