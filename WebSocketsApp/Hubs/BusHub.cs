using Common.Utils.Extensions;
using Common.WebCommon.Models.WebSocketApi.Event;
using LogicWebSocket.Model.Request.Hub.Bus;
using LogicWebSocket.Model.Response.Hub.BusCooperative;
using Microsoft.AspNetCore.SignalR;

namespace WebSocketsApp.Hubs;
/// <summary>
/// Hub para Cooperativas
/// </summary>
public partial class HubManager
{
    /// <summary>
    /// Suscribe a un evento de bus
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HubMethodName("joinBus")]
    public async Task JoinBus(JoinBusEventRequest request)
    {
        //Obtener información
        await AddToGroup($"{request.RouteGuid}").ConfigureAwait(false);
        var countUserts = GetUsersByGroup($"{request.RouteGuid}").Count;
        //Lanzar evento para actualizar tickets
        await Clients.Group($"{request.RouteGuid}").SendAsync(EventHubName.UpdateUsersVieweingTicket.GetEnumMember(), new NumberUserVieweingTicketEventResponse(countUserts)).ConfigureAwait(false);
    }

    /// <summary>
    /// Deja de suscribirse en un grupo específico
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HubMethodName("leaveBus")]
    public async Task LeaveBus(LeaveBusEventRequest request)
    {
        await RemoveFromGroup($"{request.RouteGuid}").ConfigureAwait(false);
        var countUserts = GetUsersByGroup($"{request.RouteGuid}").Count;
        //Lanzar evento para actualizar tickets
        await Clients.Group($"{request.RouteGuid}")
            .SendAsync(
                EventHubName.UpdateUsersVieweingTicket.GetEnumMember(),
                new NumberUserVieweingTicketEventResponse(countUserts)).ConfigureAwait(false);
    }


    /// <summary>
    /// Cuando se desconecta
    /// </summary>
    /// <param name="groupsToRemoveEvent"></param>
    /// <returns></returns>
    private Task OnDisconnectedBusAsync(Dictionary<string, Dictionary<string, HashSet<string>>> groupsToRemoveEvent)
    {
        return Task.WhenAll(groupsToRemoveEvent.Select(select => Clients.Group(select.Key)
             .SendAsync(
                 EventHubName.UpdateUsersVieweingTicket.GetEnumMember(),
                 new NumberUserVieweingTicketEventResponse(select.Value.Count)
             )));
    }

}