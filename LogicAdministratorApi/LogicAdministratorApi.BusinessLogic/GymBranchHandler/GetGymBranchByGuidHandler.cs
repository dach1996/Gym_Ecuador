using LogicAdministratorApi.Model.Request.GymBranch;
using LogicAdministratorApi.Model.Response.GymBranch;
using LogicCommon.Model.Response.File;

namespace LogicAdministratorApi.BusinessLogic.GymBranchHandler;

/// <summary>
/// Handler para obtener detalle de sucursal de gimnasio por GUID
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetGymBranchByGuidHandler(
    ILogger<GetGymBranchByGuidHandler> logger,
    IPluginFactory pluginFactory) : GymBranchBase<GetGymBranchByGuidRequest, GetGymBranchByGuidResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de detalle de una sucursal por GUID
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetGymBranchByGuidResponse> Handle(GetGymBranchByGuidRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.GetGymBranchByGuid, request, async () =>
            {
                // Buscar la sucursal por GUID con la relación al gimnasio
                var gymBranch = await UnitOfWork.GymBranchRepository
                    .GetFirstOrDefaultGenericAsync(select => new GymBranchDetail
                    {
                        Guid = select.Guid,
                        GymGuid = select.Gym.Guid,
                        Name = select.Name,
                        Code = select.Code,
                        Description = select.Description,
                        Address = select.Address,
                        Phone = select.Phone,
                        Email = select.Email,
                        Latitude = select.Latitude,
                        Longitude = select.Longitude,
                        MaxCapacity = select.MaxCapacity,
                        AreaSquareMeters = select.AreaSquareMeters,
                        FloorCount = select.FloorCount,
                        IsActive = select.IsActive,
                        OpeningDate = select.OpeningDate,
                        DateTimeRegister = select.DateTimeRegister,
                        Images = select.GymBranchImages
                            .Where(image => image.FilePersistence.State)
                            .Select(image => new FileUrlResponse
                            {
                                Guid = image.FilePersistence.Guid,
                                Url = image.FilePersistence.FileBasePath.BaseUrl + image.FilePersistence.Path
                            }).ToList()
                    },
                        where => where.Guid == request.GymBranchGuid
                    ).ConfigureAwait(false)
                ?? throw new CustomException((int)MessagesCodesError.SystemError, "Sucursal no encontrada");

                return new GetGymBranchByGuidResponse(gymBranch)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            }
        ).ConfigureAwait(false);
}

