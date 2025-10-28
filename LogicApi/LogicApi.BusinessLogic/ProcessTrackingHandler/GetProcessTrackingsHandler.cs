using LogicApi.Model.Request.ProcessTracking;
using LogicApi.Model.Response.ProcessTracking;

namespace LogicApi.BusinessLogic.ProcessTrackingHandler;

/// <summary>
/// Handler para obtener seguimientos de procesos
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetProcessTrackingsHandler(
    ILogger<GetProcessTrackingsHandler> logger,
    IPluginFactory pluginFactory) : ProcessTrackingBase<GetProcessTrackingsRequest, GetProcessTrackingsResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de seguimientos de procesos con paginación
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetProcessTrackingsResponse> Handle(GetProcessTrackingsRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetProcessTrackings, request, async () =>
            {
                // Construir la consulta base con includes
                var processTrackings = await UnitOfWork.ProcessTrackingRepository
                    .GetByAsync( 
                    ).ConfigureAwait(false);

                // Mapear a DTOs
                var processTrackingItems = processTrackings.Select(pt => new ProcessTrackingItem
                {
                    Guid = pt.Guid,
                    PersonFullName = pt.Person.FullName,
                    GymName = pt.Gym.Name,
                    DateTimeRegister = pt.DateTimeRegister
                });

                return new GetProcessTrackingsResponse(processTrackingItems, processTrackings.Count, request.Page, request.PageSize)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            }
        ).ConfigureAwait(false);
}
