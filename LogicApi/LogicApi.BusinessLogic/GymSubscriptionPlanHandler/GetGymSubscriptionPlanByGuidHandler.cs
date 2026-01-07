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
                // Buscar el plan con la sucursal y gimnasio incluidos
                var plan = await UnitOfWork.BranchPlanRepository
                    .GetByFirstOrDefaultAsync(where => where.Guid == request.PlanGuid)
                    .ConfigureAwait(false);

                if (plan == null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "Plan de suscripción no encontrado");

                // Mapear a DTO
                var planDetail = new GymSubscriptionPlanDetail
                {
                    Guid = plan.Guid,
                    GymId = plan.GymBranch?.GymId ?? 0,
                    GymGuid = plan.GymBranch?.Gym?.Guid ?? Guid.Empty,
                    GymName = plan.GymBranch?.Gym?.Name,
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

