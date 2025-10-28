using LogicApi.Model.Request.ProcessTracking;
using LogicApi.Model.Response.ProcessTracking;

namespace LogicApi.BusinessLogic.ProcessTrackingHandler;

/// <summary>
/// Handler para obtener seguimiento de proceso por GUID
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetProcessTrackingByGuidHandler(
    ILogger<GetProcessTrackingByGuidHandler> logger,
    IPluginFactory pluginFactory) : ProcessTrackingBase<GetProcessTrackingByGuidRequest, GetProcessTrackingByGuidResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la obtención de un seguimiento de proceso por GUID
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetProcessTrackingByGuidResponse> Handle(GetProcessTrackingByGuidRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.GetProcessTrackingByGuid, request, async () =>
            {
                // Buscar el seguimiento de proceso por GUID con includes
                var processTracking = await UnitOfWork.ProcessTrackingRepository
                    .GetByFirstOrDefaultAsync(
                        where => where.Guid == request.ProcessTrackingGuid,
                        include => include.Person,
                        include => include.Gym
                    ).ConfigureAwait(false);

                if (processTracking == null)
                    throw new CustomException((int)MessagesCodesError.SystemError, "Seguimiento de proceso no encontrado");

                // Mapear a DTO
                var processTrackingDetail = new ProcessTrackingDetail
                {
                    Guid = processTracking.Guid,
                    Gym = new GymInfo
                    {
                        Guid = processTracking.Gym.Guid,
                        Name = processTracking.Gym.Name,
                        Address = processTracking.Gym.Address
                    },
                    Person = new PersonInfo
                    {
                        Id = processTracking.Person.Id,
                        FullName = processTracking.Person.FullName,
                    },
                    
                    DateTimeRegister = processTracking.DateTimeRegister
                };

                return new GetProcessTrackingByGuidResponse(processTrackingDetail)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = false
                };
            },
            registerLogAudit: false
        ).ConfigureAwait(false);
}
