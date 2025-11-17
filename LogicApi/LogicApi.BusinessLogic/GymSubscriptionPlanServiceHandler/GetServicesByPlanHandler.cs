using LogicApi.Model.Request.GymSubscriptionPlanService;
using LogicApi.Model.Response.GymSubscriptionPlanService;
using Microsoft.EntityFrameworkCore;

namespace LogicApi.BusinessLogic.GymSubscriptionPlanServiceHandler;

/// <summary>
/// Handler para obtener servicios de un plan
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetServicesByPlanHandler(
    ILogger<GetServicesByPlanHandler> logger,
    IPluginFactory pluginFactory) : GymSubscriptionPlanServiceBase<GetServicesByPlanRequest, GetServicesByPlanResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de servicios de un plan
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetServicesByPlanResponse> Handle(GetServicesByPlanRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetServicesByPlan, request, async () =>
            {
                // Buscar el plan
                var plan = await UnitOfWork.GymSubscriptionPlanRepository
                    .GetByFirstOrDefaultAsync(where => where.Guid == request.PlanGuid)
                    .ConfigureAwait(false);

                if (plan == null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "Plan de suscripción no encontrado");

                // Obtener los servicios del plan
                var planServices = await UnitOfWork.GymSubscriptionPlanServiceRepository
                    .GetByAsync()
                    .ConfigureAwait(false);

                // Mapear a DTOs
                var serviceItems = planServices.Select(ps => new PlanServiceItem
                {
                    PlanServiceId = ps.Id,
                    ServiceId = ps.ServiceId,
                    ServiceName = ps.Service?.Name,
                    ServiceDescription = ps.Service?.Description,
                    RequiresReservation = ps.Service?.RequiresReservation ?? false
                });

                return new GetServicesByPlanResponse(plan.Name, serviceItems)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            },
            registerLogAudit: false
        ).ConfigureAwait(false);
}

