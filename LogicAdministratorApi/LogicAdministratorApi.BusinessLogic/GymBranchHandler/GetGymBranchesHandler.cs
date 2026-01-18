using System.Linq.Expressions;
using LogicAdministratorApi.Model.Request.GymBranch;
using LogicAdministratorApi.Model.Response.GymBranch;
using LogicCommon.Model.Response.File;
using PersistenceDb.Models.Core;

namespace LogicAdministratorApi.BusinessLogic.GymBranchHandler;

/// <summary>
/// Handler para obtener sucursales de gimnasio paginadas
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
        => await ExecuteHandlerAsync(OperationAdministratorName.GetGymBranchesPaginated, request, async () =>
            {
                var gymId = (await GetGymCacheInformationAsync().ConfigureAwait(false))
                .Find(where => where.GymGuid == request.GymGuid)?.GymId ?? throw new CustomException((int)MessagesCodesError.GymBranchNotFound, "No se encontró el gimnasio especificado");
                // Construir el filtro where combinando todas las condiciones
                var filter = request.Filter?.ToLower();

                var whereClause = new List<Expression<Func<GymBranch, bool>>>
                {
                    {where => where.GymId == gymId},
                    {!request.Filter.IsNullOrEmpty(),
                        where => where.Name.ToLower().Contains(filter) ||
                        where.Address.ToLower().Contains(filter) ||
                        where.Phone.ToLower().Contains(filter) ||
                        where.Email.ToLower().Contains(filter) 
                        }
                };

                // Obtener datos paginados
                var paginatedResult = await UnitOfWork.GymBranchRepository
                    .GetPaginatorGenericAsync(
                        itemsByPage: request.PageSize,
                        page: request.PageNumber,
                        select => new GymBranchItem
                        {
                            Guid = select.Guid,
                            Name = select.Name,
                            Address = select.Address,
                            Phone = select.Phone,
                            Email = select.Email,
                            IsActive = select.IsActive,
                            Latitude = select.Latitude,
                            Longitude = select.Longitude,
                            OpeningDate = select.OpeningDate,
                            ImageUrl = select.GymBranchImages
                                .Where(image => image.FilePersistence.State)
                                .Select(image => new FileUrlResponse(image.FilePersistence.Guid, image.FilePersistence.FileBasePath.BaseUrl, image.FilePersistence.Path))
                                .FirstOrDefault()
                        },
                        where: whereClause,
                        orderBy: gb => gb.Name,
                        orderByType: OrderByType.Asc
                    ).ConfigureAwait(false);

                return new GetGymBranchesResponse(paginatedResult.TotalItems, paginatedResult.Items)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            }
        ).ConfigureAwait(false);
}

