using LogicApi.Model.Request.GymBranch;
using LogicApi.Model.Response.GymBranch;
using LogicCommon.Model.Response.File;

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
                        page: request.PageNumber,
                        select => new GymBranchItem
                        {
                            Guid = select.Guid,
                            Name = select.Name,
                            CalificationPercentage = 90,
                            Address = select.Address,
                            ImageUrls = select.GymBranchImages
                                .Where(image => image.FilePersistence.State)
                                .Select(image => new FileUrlResponse(image.FilePersistence.Guid, image.FilePersistence.FileBasePath.BaseUrl, image.FilePersistence.Path))
                                .ToList()
                        },
                        where => true
                    ).ConfigureAwait(false);

                return new GetGymBranchesResponse
                (
                    totalRegister: paginatedResult.TotalItems,
                    registers: paginatedResult.Items
                );
            }
        ).ConfigureAwait(false);
}

