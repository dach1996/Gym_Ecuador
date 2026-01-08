using LogicAdministratorApi.Model.Request.BranchPlan;
using LogicAdministratorApi.Model.Response.BranchPlan;

namespace LogicAdministratorApi.BusinessLogic.BranchPlanHandler;

/// <summary>
/// Handler para actualizar plan de sucursal
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class UpdateBranchPlanHandler(
    ILogger<UpdateBranchPlanHandler> logger,
    IPluginFactory pluginFactory) : BranchPlanBase<UpdateBranchPlanRequest, UpdateBranchPlanResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la actualización de un plan de sucursal
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<UpdateBranchPlanResponse> Handle(UpdateBranchPlanRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.UpdateBranchPlan, request, async () =>
        {
            // Buscar el plan por GUID
            var branchPlan = await UnitOfWork.BranchPlanRepository
                .GetByFirstOrDefaultAsync(where => where.Guid == request.BranchPlanGuid)
                .ConfigureAwait(false)
            ?? throw new CustomException((int)MessagesCodesError.SystemError, "Plan de sucursal no encontrado");

            // Validar que el nombre no esté vacío
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new CustomException((int)MessagesCodesError.SystemError, "El nombre del plan es requerido");

            // Validar que no exista otro plan con el mismo nombre para esta sucursal (excluyendo el actual)
            if (await UnitOfWork.BranchPlanRepository
                .ExistAnyAsync(where => where.Name.ToLower() == request.Name.ToLower() && where.Id != branchPlan.Id && where.GymBranchId == branchPlan.GymBranchId)
                .ConfigureAwait(false))
                throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe un plan con este nombre para la sucursal");

            // Validar que no exista otro plan con el mismo código si se proporciona código (excluyendo el actual)
            if (!string.IsNullOrWhiteSpace(request.Code) &&
                await UnitOfWork.BranchPlanRepository
                    .ExistAnyAsync(where => where.Code.ToLower() == request.Code.ToLower() && where.Id != branchPlan.Id && where.GymBranchId == branchPlan.GymBranchId)
                    .ConfigureAwait(false))
                throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe un plan con este código para la sucursal");

            // Actualizar los campos
            branchPlan.Name = request.Name;
            branchPlan.Code = request.Code;
            branchPlan.Description = request.Description;
            branchPlan.Price = request.Price;
            branchPlan.DurationDays = request.DurationDays;
            branchPlan.EnrollmentFee = request.EnrollmentFee;
            branchPlan.IsActive = request.IsActive;

            await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);
            await UnitOfWork.BranchPlanRepository.UpdateAsync(branchPlan).ConfigureAwait(false);
            await UnitOfWork.CommitAsync().ConfigureAwait(false);

            return new UpdateBranchPlanResponse(branchPlan.Guid, branchPlan.Name)
            {
                UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                ShowMessage = true
            };
        }
        ).ConfigureAwait(false);
}

