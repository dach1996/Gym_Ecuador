using LogicApi.Model.Request.GymSubscriptionPlan;
using LogicApi.Model.Response.GymSubscriptionPlan;

namespace LogicApi.BusinessLogic.GymSubscriptionPlanHandler;

/// <summary>
/// Handler para actualizar plan de suscripción
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class UpdateGymSubscriptionPlanHandler(
    ILogger<UpdateGymSubscriptionPlanHandler> logger,
    IPluginFactory pluginFactory) : GymSubscriptionPlanBase<UpdateGymSubscriptionPlanRequest, UpdateGymSubscriptionPlanResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la actualización de un plan de suscripción
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<UpdateGymSubscriptionPlanResponse> Handle(UpdateGymSubscriptionPlanRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.UpdateGymSubscriptionPlan, request, async () =>
            {
                // Buscar el plan por GUID
                var plan = await UnitOfWork.BranchPlanRepository
                    .GetByFirstOrDefaultAsync(where => where.Guid == request.PlanGuid)
                    .ConfigureAwait(false);

                if (plan == null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "Plan de suscripción no encontrado");

                // Validar que no exista otro plan con el mismo nombre (excluyendo el actual)
                if (!string.IsNullOrWhiteSpace(request.Name))
                {
                    var existingPlan = await UnitOfWork.BranchPlanRepository
                        .GetByFirstOrDefaultAsync(where => where.Name.ToLower() == request.Name.ToLower() 
                            && where.Id != plan.Id 
                            && where.GymBranchId == plan.GymBranchId)
                        .ConfigureAwait(false);

                    if (existingPlan != null)
                        throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe otro plan con este nombre en el gimnasio");

                    plan.Name = request.Name;
                }

                // Actualizar los campos si se proporcionan
                if (!string.IsNullOrWhiteSpace(request.Code))
                    plan.Code = request.Code;

                if (!string.IsNullOrWhiteSpace(request.Description))
                    plan.Description = request.Description;

                if (request.Price.HasValue)
                    plan.Price = request.Price.Value;

                if (request.DurationDays.HasValue)
                    plan.DurationDays = request.DurationDays.Value;

                if (request.EnrollmentFee.HasValue)
                    plan.EnrollmentFee = request.EnrollmentFee.Value;

                if (request.IsActive.HasValue)
                    plan.IsActive = request.IsActive.Value;

                return new UpdateGymSubscriptionPlanResponse(plan.Guid, plan.Name)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            },
            registerLogAudit: true
        ).ConfigureAwait(false);
}

