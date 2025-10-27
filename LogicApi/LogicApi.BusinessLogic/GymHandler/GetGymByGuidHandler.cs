using LogicApi.Model.Request.Gym;
using LogicApi.Model.Response.Gym;

namespace LogicApi.BusinessLogic.GymHandler;

/// <summary>
/// Handler para obtener gimnasio por GUID
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetGymByGuidHandler(
    ILogger<GetGymByGuidHandler> logger,
    IPluginFactory pluginFactory) : GymBase<GetGymByGuidRequest, GetGymByGuidResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de un gimnasio por GUID
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetGymByGuidResponse> Handle(GetGymByGuidRequest request, CancellationToken cancellationToken)
    {
        return await ExecuteHandlerAsync(
            OperationApiName.GetGymByGuid,
            request,
            async () =>
            {
                // Buscar el gimnasio por GUID
                var gym = await UnitOfWork.GymRepository
                    .GetByFirstOrDefaultAsync(where => where.Guid == request.GymGuid)
                    .ConfigureAwait(false);

                if (gym == null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "Gimnasio no encontrado");

                // Mapear a DTO
                var gymDetail = new GymDetail
                {
                    Guid = gym.Guid,
                    Name = gym.Name,
                    Description = gym.Description,
                    ShortDescription = gym.ShortDescription,
                    Address = gym.Address,
                    Phone = gym.Phone,
                    Email = gym.Email,
                    Website = gym.Website,
                    OpeningTime = gym.OpeningTime,
                    ClosingTime = gym.ClosingTime,
                    Latitude = gym.Latitude,
                    Longitude = gym.Longitude,
                    IsActive = gym.IsActive,
                    DateTimeRegister = gym.DateTimeRegister
                };

                return new GetGymByGuidResponse(gymDetail)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            },
            registerLogAudit: false
        ).ConfigureAwait(false);
    }
}
