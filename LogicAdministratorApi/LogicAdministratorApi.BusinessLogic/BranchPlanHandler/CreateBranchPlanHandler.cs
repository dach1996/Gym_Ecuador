using LogicAdministratorApi.Model.Request.BranchPlan;
using LogicAdministratorApi.Model.Response.BranchPlan;
using PersistenceDb.Models.Core;

namespace LogicAdministratorApi.BusinessLogic.BranchPlanHandler;

/// <summary>
/// Handler para crear plan de sucursal
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class CreateBranchPlanHandler(
    ILogger<CreateBranchPlanHandler> logger,
    IPluginFactory pluginFactory) : BranchPlanBase<CreateBranchPlanRequest, CreateBranchPlanResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la creación de un plan de sucursal
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<CreateBranchPlanResponse> Handle(CreateBranchPlanRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.CreateBranchPlan, request, async () =>
            {
                // Validar que la sucursal existe
                var gymBranchId = await UnitOfWork.GymBranchRepository
                    .GetFirstOrDefaultGenericAsync(
                        select => (int?)select.Id,
                        where => where.Guid == request.GymBranchGuid)
                    .ConfigureAwait(false) ?? throw new CustomException((int)MessagesCodesError.GymBranchNotFound, "No se encontró la sucursal de gimnasio especificada");

                // Validar que no exista un plan con el mismo nombre para esta sucursal
                if (await UnitOfWork.BranchPlanRepository
                    .ExistAnyAsync(where => where.GymBranchId == gymBranchId && where.Name.ToLower() == request.Name.ToLower())
                    .ConfigureAwait(false))
                    throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe un plan con este nombre para la sucursal");

                // Validar que no exista un plan con el mismo código si se proporciona código
                if (!string.IsNullOrWhiteSpace(request.Code) &&
                    await UnitOfWork.BranchPlanRepository
                        .ExistAnyAsync(where => where.GymBranchId == gymBranchId && where.Code.ToLower() == request.Code.ToLower())
                        .ConfigureAwait(false))
                    throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe un plan con este código para la sucursal");

                // Crear el nuevo plan
                var newBranchPlan = new BranchPlan
                {
                    Guid = Guid.NewGuid(),
                    GymBranchId = gymBranchId,
                    Name = request.Name,
                    Code = request.Code,
                    Description = request.Description,
                    Price = request.Price,
                    DurationDays = request.DurationDays,
                    EnrollmentFee = request.EnrollmentFee,
                    IsActive = true
                };

                // Guardar en la base de datos
                await UnitOfWork.BeginTransactionAsync().ConfigureAwait(false);
                await UnitOfWork.BranchPlanRepository.AddAsync(newBranchPlan).ConfigureAwait(false);
                await UnitOfWork.CommitAsync().ConfigureAwait(false);

                return new CreateBranchPlanResponse(newBranchPlan.Guid, newBranchPlan.Name, request.GymBranchGuid)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            }
        ).ConfigureAwait(false);
}

