using LogicApi.Model.Request.GymSubscriptionPlan;
using LogicApi.Model.Response.GymSubscriptionPlan;
using Microsoft.EntityFrameworkCore;

namespace LogicApi.BusinessLogic.GymSubscriptionPlanHandler;

/// <summary>
/// Handler para obtener plan de suscripción por GUID
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetGymSubscriptionPlanByGuidHandler(
    ILogger<GetGymSubscriptionPlanByGuidHandler> logger,
    IPluginFactory pluginFactory) : GymSubscriptionPlanBase<GetGymSubscriptionPlanByGuidRequest, GetGymSubscriptionPlanByGuidResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de un plan de suscripción por su GUID
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetGymSubscriptionPlanByGuidResponse> Handle(GetGymSubscriptionPlanByGuidRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetGymSubscriptionPlanByGuid, request, async () =>
            {
                // Buscar el plan con el gimnasio incluido
                var plan = await UnitOfWork.GymSubscriptionPlanRepository
                    .GetByFirstOrDefaultAsync(where => where.Guid == request.PlanGuid)
                    .ConfigureAwait(false);

                if (plan == null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "Plan de suscripción no encontrado");

                // Mapear a DTO
                var planDetail = new GymSubscriptionPlanDetail
                {
                    Guid = plan.Guid,
                    GymId = plan.GymId,
                    GymGuid = plan.Gym?.Guid ?? Guid.Empty,
                    GymName = plan.Gym?.Name,
                    Name = plan.Name,
                    Code = plan.Code,
                    Description = plan.Description,
                    Price = plan.Price,
                    DurationDays = plan.DurationDays,
                    EnrollmentFee = plan.EnrollmentFee,
                    IsActive = plan.IsActive
                };

                return new GetGymSubscriptionPlanByGuidResponse(planDetail)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            },
            registerLogAudit: false
        ).ConfigureAwait(false);
}

