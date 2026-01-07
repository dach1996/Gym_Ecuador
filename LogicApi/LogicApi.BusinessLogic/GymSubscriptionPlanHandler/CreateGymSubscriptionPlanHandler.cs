using LogicApi.Model.Request.GymSubscriptionPlan;
using LogicApi.Model.Response.GymSubscriptionPlan;
using PersistenceDb.Models.Core;

namespace LogicApi.BusinessLogic.GymSubscriptionPlanHandler;

/// <summary>
/// Handler para crear plan de suscripción
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class CreateGymSubscriptionPlanHandler(
    ILogger<CreateGymSubscriptionPlanHandler> logger,
    IPluginFactory pluginFactory) : GymSubscriptionPlanBase<CreateGymSubscriptionPlanRequest, CreateGymSubscriptionPlanResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la creación de un plan de suscripción
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<CreateGymSubscriptionPlanResponse> Handle(CreateGymSubscriptionPlanRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.CreateGymSubscriptionPlan, request, async () =>
            {
                // Validar que la sucursal de gimnasio existe
                var gymBranch = await UnitOfWork.GymBranchRepository
                    .GetByFirstOrDefaultAsync(where => where.Guid == request.GymGuid)
                    .ConfigureAwait(false);

                if (gymBranch == null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "Sucursal de gimnasio no encontrada");

                // Validar que no exista un plan con el mismo nombre en la misma sucursal
                if (await UnitOfWork.BranchPlanRepository
                    .ExistAnyAsync(where => where.GymBranchId == gymBranch.Id && where.Name.ToLower() == request.Name.ToLower())
                    .ConfigureAwait(false))
                    throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe un plan con este nombre en la sucursal");

                // Crear el nuevo plan
                var newPlan = new BranchPlan
                {
                    Guid = Guid.NewGuid(),
                    GymBranchId = gymBranch.Id,
                    Name = request.Name,
                    Code = request.Code,
                    Description = request.Description,
                    Price = request.Price,
                    DurationDays = request.DurationDays,
                    EnrollmentFee = request.EnrollmentFee,
                    IsActive = true
                };

                // Guardar en la base de datos
                await UnitOfWork.BranchPlanRepository.AddAsync(newPlan).ConfigureAwait(false);

                return new CreateGymSubscriptionPlanResponse(newPlan.Guid, newPlan.Name)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            }
        ).ConfigureAwait(false);
}

