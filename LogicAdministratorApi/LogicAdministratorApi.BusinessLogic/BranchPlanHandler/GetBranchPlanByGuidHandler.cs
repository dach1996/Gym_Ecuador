using LogicAdministratorApi.Model.Request.BranchPlan;
using LogicAdministratorApi.Model.Response.BranchPlan;

namespace LogicAdministratorApi.BusinessLogic.BranchPlanHandler;

/// <summary>
/// Handler para obtener plan de sucursal por GUID
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetBranchPlanByGuidHandler(
    ILogger<GetBranchPlanByGuidHandler> logger,
    IPluginFactory pluginFactory) : BranchPlanBase<GetBranchPlanByGuidRequest, GetBranchPlanByGuidResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de un plan de sucursal por GUID
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetBranchPlanByGuidResponse> Handle(GetBranchPlanByGuidRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.GetBranchPlanByGuid, request, async () =>
            {
                // Buscar el plan por GUID con las relaciones
                var branchPlan = await UnitOfWork.BranchPlanRepository
                    .GetFirstOrDefaultGenericAsync(branchPlanEntity =>
                        new BranchPlanDetail
                        {
                            Guid = branchPlanEntity.Guid,
                            GymBranchGuid = branchPlanEntity.GymBranch.Guid,
                            Name = branchPlanEntity.Name,
                            Code = branchPlanEntity.Code,
                            Description = branchPlanEntity.Description,
                            Price = branchPlanEntity.Price,
                            DurationDays = branchPlanEntity.DurationDays,
                            EnrollmentFee = branchPlanEntity.EnrollmentFee,
                            IsActive = branchPlanEntity.IsActive
                        },
                        where => where.Guid == request.BranchPlanGuid)
                    .ConfigureAwait(false)
                    ?? throw new CustomException((int)MessagesCodesError.SystemError, "Plan de sucursal no encontrado");

                return new GetBranchPlanByGuidResponse(branchPlan)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            }).ConfigureAwait(false);
}

