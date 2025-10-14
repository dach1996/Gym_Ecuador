

using Common.Cooperative;
using Common.Cooperative.Models.Response;
using LogicApi.Model.Common;

namespace LogicApi.BusinessLogic.SeatHandler;

/// <summary>
/// Clase base para Asientos
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
/// <returns></returns>
public abstract class SeatBase<TRequest, TResponse>(
    ILogger<SeatBase<TRequest, TResponse>> logger,
    IPluginFactory pluginFactory) : BusinessLogicBase(
        logger,
        pluginFactory),
    IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);

    /// <summary>
    /// Obtiene el estado final
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="seatState"></param>
    /// <returns></returns>
    protected static EnumLogicCommon.SeatState GetFinalState(SeatState seatState)
       => seatState switch
       {
           SeatState.Available => EnumLogicCommon.SeatState.Available,
           SeatState.Reserved => EnumLogicCommon.SeatState.Reserved,
           SeatState.Prepaid => EnumLogicCommon.SeatState.Prepaid,
           SeatState.Purchased => EnumLogicCommon.SeatState.Purchased,
           SeatState.Expired => EnumLogicCommon.SeatState.Available,
           _ => throw new NotImplementedException($"No está implementado la el estado: {seatState}"),
       };

    /// <summary>
    /// Obtiene implementación de servicios de cooperativa por el Id
    /// </summary>
    /// <param name="cooperativeId"></param>
    /// <returns></returns>
    protected async Task<GetBusSeatResponse> GetBusInformationByCooperativeServiceAsync(int cooperativeId, string ticketIdentifier)
    {
        var cooperativeServicesImplementation = await GetCooperativeServicesImplementationByCooperativeId(cooperativeId).ConfigureAwait(false);
        return await cooperativeServicesImplementation.GetBusSeatAsync(new(ticketIdentifier)).ConfigureAwait(false);
    }


}
