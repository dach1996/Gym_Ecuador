using LogicApi.Model.Request.GymSubscriptionPlan;
using LogicApi.Model.Response.GymSubscriptionPlan;
using Microsoft.EntityFrameworkCore;

namespace LogicApi.BusinessLogic.GymSubscriptionPlanHandler;

/// <summary>
/// Handler para obtener planes de suscripción
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetGymSubscriptionPlansHandler(
    ILogger<GetGymSubscriptionPlansHandler> logger,
    IPluginFactory pluginFactory) : GymSubscriptionPlanBase<GetGymSubscriptionPlansRequest, GetGymSubscriptionPlansResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de planes de suscripción con paginación
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetGymSubscriptionPlansResponse> Handle(GetGymSubscriptionPlansRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetGymSubscriptionPlans, request, async () =>
            {
                /* // Construir query con filtros
                var query = UnitOfWork.GymSubscriptionPlanRepository.GetByFirstOrDefaultAsync(where => where.GymId == request.GymId);

                // Filtrar por gimnasio si se proporciona
                if (request.GymGuid.HasValue)
                {
                    var gym = await UnitOfWork.GymRepository
                        .GetByFirstOrDefaultAsync(where => where.Guid == request.GymGuid.Value)
                        .ConfigureAwait(false);

                    if (gym != null)
                        query = query.Where(p => p.GymId == gym.Id);
                }

                // Filtrar por nombre si se proporciona
                if (!string.IsNullOrWhiteSpace(request.NameFilter))
                    query = query.Where(p => p.Name.Contains(request.NameFilter));

                // Filtrar por estado si se proporciona
                if (request.IsActiveFilter.HasValue)
                    query = query.Where(p => p.IsActive == request.IsActiveFilter.Value);

                // Obtener el total de registros
                var totalRecords = await query.CountAsync(cancellationToken).ConfigureAwait(false);

                // Aplicar paginación e incluir gimnasio
                var plans = await query
                    .Include(p => p.Gym)
                    .OrderByDescending(p => p.Id)
                    .Skip((request.Page - 1) * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync(cancellationToken)
                    .ConfigureAwait(false);

                // Mapear a DTOs
                var planItems = plans.Select(p => new GymSubscriptionPlanItem
                {
                    Guid = p.Guid,
                    Name = p.Name,
                    Code = p.Code,
                    Description = p.Description,
                    Price = p.Price,
                    DurationDays = p.DurationDays,
                    EnrollmentFee = p.EnrollmentFee,
                    IsActive = p.IsActive,
                    GymName = p.Gym?.Name
                });

                return new GetGymSubscriptionPlansResponse(planItems, totalRecords, request.Page, request.PageSize)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                }; */
                return new GetGymSubscriptionPlansResponse(new List<GymSubscriptionPlanItem>(), 0, request.Page, request.PageSize)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            },
            registerLogAudit: false
        ).ConfigureAwait(false);
}

