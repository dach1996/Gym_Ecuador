using LogicAdministratorApi.Model.Request.BranchPlan;
using LogicAdministratorApi.Model.Response.BranchPlan;

namespace LogicAdministratorApi.BusinessLogic.BranchPlanHandler;

/// <summary>
/// Handler para eliminar plan de sucursal
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class DeleteBranchPlanHandler(
    ILogger<DeleteBranchPlanHandler> logger,
    IPluginFactory pluginFactory) : BranchPlanBase<DeleteBranchPlanRequest, DeleteBranchPlanResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la eliminación de un plan de sucursal
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<DeleteBranchPlanResponse> Handle(DeleteBranchPlanRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.DeleteBranchPlan, request, async () =>
            {
                // Buscar el plan por GUID
                var branchPlanId = await UnitOfWork.BranchPlanRepository
                    .GetFirstOrDefaultGenericAsync(
                        select => (int?)select.Id,
                        where => where.Guid == request.BranchPlanGuid)
                    .ConfigureAwait(false) ?? throw new CustomException((int)MessagesCodesError.SystemError, "Plan de sucursal no encontrado");

                // Validar que no tenga membresías asociadas
                var hasMemberships = await UnitOfWork.ClientMembershipRepository
                    .ExistAnyAsync(where => where.BranchPlanId == branchPlanId)
                    .ConfigureAwait(false);

                if (hasMemberships)
                    throw new CustomException((int)MessagesCodesError.SystemError, "No se puede eliminar el plan porque tiene membresías asociadas");

                // Eliminar el plan y sus características
                await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);

                // Eliminar características del plan primero
                await UnitOfWork.PlanFeatureRepository.DeleteAsync(where => where.BranchPlanId == branchPlanId).ConfigureAwait(false);

                // Eliminar el plan
                await UnitOfWork.BranchPlanRepository.DeleteAsync(where => where.Id == branchPlanId).ConfigureAwait(false);
                await UnitOfWork.CommitAsync().ConfigureAwait(false);

                return new DeleteBranchPlanResponse()
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            }
        ).ConfigureAwait(false);
}

