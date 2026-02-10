using LogicApi.Model.Request.ClientMembership;
using LogicApi.Model.Response.ClientMembership;
using LogicCommon.Model.Response.File;

namespace LogicApi.BusinessLogic.ClientMembershipHandler;

/// <summary>
/// Handler para obtener mis membresías agrupadas por sucursal
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetMyMembershipsHandler(
    ILogger<GetMyMembershipsHandler> logger,
    IPluginFactory pluginFactory) : ClientMembershipBase<GetMyMembershipsRequest, GetMyMembershipsResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de mis membresías
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetMyMembershipsResponse> Handle(GetMyMembershipsRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetMyMemberships, request, async () =>
            {
                // Obtener todos los ClientGymBranch del usuario actual con sus membresías
                var clientGymBranches = await UnitOfWork.ClientGymBranchRepository
                    .GetGenericAsync(
                        select => new GymBranchMembershipGroup
                        {
                            GymBranchGuid = select.GymBranch.Guid,
                            GymBranchName = select.GymBranch.Name,
                            GymBranchImageUrl = select.GymBranch.GymBranchImages
                                .Where(img => img.FilePersistence.State)
                                .Select(img => new FileUrlResponse(img.FilePersistence.Guid, img.FilePersistence.FileBasePath.BaseUrl, img.FilePersistence.Path).Url)
                                .FirstOrDefault(),
                            MembershipHistory = select.ClientMemberships
                                .OrderByDescending(m => m.StartDate)
                                .Select(m => new MembershipHistoryItem
                                {
                                    MembershipGuid = m.Guid,
                                    PlanName = m.BranchPlan.Name,
                                    PlanDescription = m.BranchPlan.Description,
                                    PlanPrice = m.BranchPlan.Price,
                                    StartDate = m.StartDate,
                                    EndDate = m.EndDate,
                                    Status = m.IsActive
                                        ? MembershipStatus.Active
                                        : (m.EndDate.HasValue && m.EndDate.Value < DateTime.UtcNow
                                            ? MembershipStatus.Expired
                                            : MembershipStatus.Cancelled),
                                    PeriodicityName = "Por Mes"
                                }).ToList()
                        },
                        where => where.PersonId == PersonId && where.Status)
                    .ConfigureAwait(false);

                // Procesar para identificar membresía activa
                var result = clientGymBranches.Select(group =>
                {
                    group.ActiveMembership = group.MembershipHistory
                        .FirstOrDefault(m => m.Status == MembershipStatus.Active);
                    return group;
                }).ToList();

                return new GetMyMembershipsResponse(result);
            }).ConfigureAwait(false);
}
