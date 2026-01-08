using System.Linq.Expressions;
using LogicAdministratorApi.Model.Request.Gym;
using LogicAdministratorApi.Model.Response.Gym;
using PersistenceDb.Models.Core;
using PersistenceDb.Models.Enums;
using Common.WebCommon.Models.Enum;

namespace LogicAdministratorApi.BusinessLogic.GymHandler;

/// <summary>
/// Handler para obtener gimnasios paginados
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetGymsHandler(
    ILogger<GetGymsHandler> logger,
    IPluginFactory pluginFactory) : GymBase<GetGymsRequest, GetGymsResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de gimnasios con paginación
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetGymsResponse> Handle(GetGymsRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.GetGymsPaginated, request, async () =>
            {
                // Construir el filtro where combinando todas las condiciones
                var nameFilter = request.NameFilter?.ToLower();
                var codeFilter = request.CodeFilter?.ToLower();
                var isSuperAdmin = ContextRequest.CustomClaims.IsSuperAdmin;
                var gymsRoleIds = ContextRequest.CustomClaims.GymRoleContextClaims
                .Where(where => where.RoleType == RoleType.GymAdministrator)
                .Select(where => where.Identifier).ToList();
                var gymsBranchRoleIds = ContextRequest.CustomClaims.GymRoleContextClaims
                .Where(where => where.RoleType == RoleType.GymBranchAdministrator)
                .Select(where => where.Identifier).ToList();
                var whereClause = new List<Expression<Func<Gym, bool>>>
                {
                    {!request.NameFilter.IsNullOrEmpty(), where => where.Name.ToLower().Contains(nameFilter)},
                    {!request.CodeFilter.IsNullOrEmpty(), where => where.Code.ToLower().Contains(codeFilter)},
                    {where => isSuperAdmin || gymsRoleIds.Contains(where.Id)},
                };
                // Obtener datos paginados
                var paginatedResult = await UnitOfWork.GymRepository
                    .GetPaginatorGenericAsync(
                        itemsByPage: request.PageSize,
                        page: request.Page,
                        select => new GymItem
                        {
                            Guid = select.Guid,
                            Name = select.Name,
                            Code = select.Code,
                            Description = select.Description,
                            ShortDescription = select.ShortDescription,
                            Phone = select.Phone,
                            Email = select.Email,
                            Website = select.Website,
                            IsActive = select.IsActive == GymStatus.Active,
                            DateTimeRegister = select.DateTimeRegister
                        },
                        where: whereClause,
                        orderBy: g => g.Name,
                        orderByType: OrderByType.Asc
                    ).ConfigureAwait(false);

                return new GetGymsResponse(paginatedResult.TotalItems, paginatedResult.Items)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            }
        ).ConfigureAwait(false);
}

