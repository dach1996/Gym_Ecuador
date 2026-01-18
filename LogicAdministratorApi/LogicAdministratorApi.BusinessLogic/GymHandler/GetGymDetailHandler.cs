using LogicAdministratorApi.Model.Request.Gym;
using LogicAdministratorApi.Model.Response.Gym;
using PersistenceDb.Models.Core;
using PersistenceDb.Models.Enums;

namespace LogicAdministratorApi.BusinessLogic.GymHandler;

/// <summary>
/// Handler para obtener detalle de gimnasio por GUID
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetGymDetailHandler(
    ILogger<GetGymDetailHandler> logger,
    IPluginFactory pluginFactory) : GymBase<GetGymDetailRequest, GetGymDetailResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de detalle de un gimnasio por GUID
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetGymDetailResponse> Handle(GetGymDetailRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationAdministratorName.GetGymDetail, request, async () =>
            {
                // Buscar el gimnasio por GUID con las relaciones necesarias
                var gym = await UnitOfWork.GymRepository
                    .GetFirstOrDefaultGenericAsync(select => new GymDetail
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
                        DateTimeRegister = select.DateTimeRegister,
                    },
                        where => where.Guid == request.GymGuid
                    ).ConfigureAwait(false)
                ?? throw new CustomException((int)MessagesCodesError.SystemError, "Gimnasio no encontrado");

                return new GetGymDetailResponse(gym)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            }
        ).ConfigureAwait(false);
}
