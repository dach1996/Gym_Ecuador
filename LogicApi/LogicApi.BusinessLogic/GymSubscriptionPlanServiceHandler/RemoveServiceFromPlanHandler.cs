using LogicApi.Model.Request.GymSubscriptionPlanService;
using LogicApi.Model.Response.GymSubscriptionPlanService;

namespace LogicApi.BusinessLogic.GymSubscriptionPlanServiceHandler;

/// <summary>
/// Handler para remover servicio de plan
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class RemoveServiceFromPlanHandler(
    ILogger<RemoveServiceFromPlanHandler> logger,
    IPluginFactory pluginFactory) : GymSubscriptionPlanServiceBase<RemoveServiceFromPlanRequest, RemoveServiceFromPlanResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la remoción de un servicio de un plan
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<RemoveServiceFromPlanResponse> Handle(RemoveServiceFromPlanRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.RemoveServiceFromPlan, request, async () =>
            {
                // Buscar la relación
                var planService = await UnitOfWork.GymSubscriptionPlanServiceRepository
                    .GetByFirstOrDefaultAsync(where => where.Id == request.PlanServiceId)
                    .ConfigureAwait(false);

                if (planService == null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "Relación plan-servicio no encontrada");

                // Eliminar la relación
                await UnitOfWork.GymSubscriptionPlanServiceRepository.DeleteAsync(planService).ConfigureAwait(false);

                return new RemoveServiceFromPlanResponse(true)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            },
            registerLogAudit: true
        ).ConfigureAwait(false);
}

