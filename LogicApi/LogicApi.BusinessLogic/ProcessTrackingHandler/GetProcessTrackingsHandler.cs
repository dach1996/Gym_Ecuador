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
                    .GetPaginatorGenericAsync(
                        itemsByPage: request.PageSize,
                        page: request.Page,
                        select => new ProcessTrackingItem
                        {
                            Guid = select.Guid,
                            PersonFullName = select.Person.FullName,
                            GymName = select.GymBranch.Gym.Name,
                            DateTimeRegister = select.DateTimeRegister
                        }
                    ).ConfigureAwait(false);


                return new GetProcessTrackingsResponse(
                    processTrackings.Items,
                    processTrackings.TotalItems,
                    request.Page, request.PageSize)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            }
        ).ConfigureAwait(false);
}
