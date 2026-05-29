using LogicApi.Model.Request.GymSubscriptionPlan;
using LogicApi.Model.Response.GymSubscriptionPlan;

namespace LogicApi.BusinessLogic.GymSubscriptionPlanHandler;

/// <summary>
/// Handler para obtener planes de suscripción
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetGymSubscriptionPlansHandler(
    ILogger<GetGymSubscriptionPlansHandler> logger,
    IPluginFactory pluginFactory) : GymSubscriptionPlanBase<GetGymSubscriptionPlansRequest, GetGymSubscriptionPlansResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de planes de suscripción con paginación
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetGymSubscriptionPlansResponse> Handle(GetGymSubscriptionPlansRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetGymSubscriptionPlans, request, async () =>
            {
                return await Task.FromResult(new GetGymSubscriptionPlansResponse(new List<GymSubscriptionPlanItem>(), 0, request.Page, request.PageSize)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                });
            },
            registerLogAudit: false
        ).ConfigureAwait(false);
}

