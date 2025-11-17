using LogicApi.Model.Request.GymSubscriptionPlanService;
using LogicApi.Model.Response.GymSubscriptionPlanService;
using Microsoft.EntityFrameworkCore;
using PersistenceDb.Models.Core;

namespace LogicApi.BusinessLogic.GymSubscriptionPlanServiceHandler;

/// <summary>
/// Handler para asignar servicio a plan
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class AssignServiceToPlanHandler(
    ILogger<AssignServiceToPlanHandler> logger,
    IPluginFactory pluginFactory) : GymSubscriptionPlanServiceBase<AssignServiceToPlanRequest, AssignServiceToPlanResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la asignación de un servicio a un plan
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<AssignServiceToPlanResponse> Handle(AssignServiceToPlanRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.AssignServiceToPlan, request, async () =>
            {
                // Validar que el plan existe
                var plan = await UnitOfWork.GymSubscriptionPlanRepository
                    .GetByFirstOrDefaultAsync(where => where.Guid == request.PlanGuid)
                    .ConfigureAwait(false);

                if (plan == null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "Plan de suscripción no encontrado");

                // Validar que el servicio existe
                var service = await UnitOfWork.ServiceRepository
                    .GetByFirstOrDefaultAsync(where => where.Id == request.ServiceId)
                    .ConfigureAwait(false);

                if (service == null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "Servicio no encontrado");

                // Validar que no exista ya la relación
                if (await UnitOfWork.GymSubscriptionPlanServiceRepository
                    .ExistAnyAsync(where => where.GymBranchSubscriptionPlanId == plan.Id && where.ServiceId == service.Id)
                    .ConfigureAwait(false))
                    throw new CustomException((int)MessagesCodesError.SystemError, "El servicio ya está asignado a este plan");

                // Crear la relación
                var planService = new GymSubscriptionPlanService
                {
                    GymBranchSubscriptionPlanId = plan.Id,
                    ServiceId = service.Id
                };

                await UnitOfWork.GymSubscriptionPlanServiceRepository.AddAsync(planService).ConfigureAwait(false);

                return new AssignServiceToPlanResponse(planService.Id, plan.Name, service.Name)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            }
        ).ConfigureAwait(false);
}

