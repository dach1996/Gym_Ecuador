using LogicApi.Model.Request.GymBranch;
using LogicApi.Model.Response.GymBranch;

namespace LogicApi.BusinessLogic.GymBranchHandler;

/// <summary>
/// Handler para obtener sucursales de gimnasio
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetGymBranchesHandler(
    ILogger<GetGymBranchesHandler> logger,
    IPluginFactory pluginFactory) : GymBranchBase<GetGymBranchesRequest, GetGymBranchesResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de sucursales de gimnasio con paginación
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetGymBranchesResponse> Handle(GetGymBranchesRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetGymBranches, request, async () =>
            {
                // Obtener datos paginados con filtros aplicados
                var paginatedResult = await UnitOfWork.GymBranchRepository
                    .GetPaginatorGenericAsync(
                        itemsByPage: request.PageSize,
                        page: request.Page,
                        selector: gb => new GymBranchItem
                        {
                            Guid = gb.Guid,
                            GymGuid = gb.Gym.Guid,
                            GymName = gb.Gym.Name,
                            Name = gb.Name,
                            Code = gb.Code,
                            Description = gb.Description,
                            Address = gb.Address,
                            Phone = gb.Phone,
                            Email = gb.Email,
                            Latitude = gb.Latitude,
                            Longitude = gb.Longitude,
                            MaxCapacity = gb.MaxCapacity,
                            AreaSquareMeters = gb.AreaSquareMeters,
                            FloorCount = gb.FloorCount,
                            IsActive = gb.IsActive,
                            OpeningDate = gb.OpeningDate,
                            DateTimeRegister = gb.DateTimeRegister
                        },
                        where: gb =>
                            (!request.GymGuid.HasValue || gb.Gym.Guid == request.GymGuid.Value) &&
                            (string.IsNullOrWhiteSpace(request.NameFilter) || gb.Name.Contains(request.NameFilter)) &&
                            (string.IsNullOrWhiteSpace(request.CodeFilter) || gb.Code.Contains(request.CodeFilter)) &&
                            (!request.IsActiveFilter.HasValue || gb.IsActive == request.IsActiveFilter.Value),
                        orderBy: gb => gb.DateTimeRegister,
                        orderByType: OrderByType.Desc
                    ).ConfigureAwait(false);

                return new GetGymBranchesResponse(paginatedResult.Items, paginatedResult.TotalItems, request.Page, request.PageSize)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            },
            registerLogAudit: false
        ).ConfigureAwait(false);
}

