using Common.WebHub.ControllerHub;
namespace WebSocketsApp.Hubs;
/// <summary>
/// Hub para Cooperativas
/// </summary>
public partial class HubManager : ConcurrentHub
{
    public HubManager(ILogger<HubManager> logger) : base(logger)
    {
        OnCustomDisconnectedEvent += OnCustomDisconnectedEventImplementation;
    }

    /// <summary>
    /// Evento de desconexión personalizado
    /// </summary>
    /// <param name="groupsToRemoveEvent"> Grupos a los que se desconectó el usuario </param>
    /// <returns></returns>
    protected async Task OnCustomDisconnectedEventImplementation(Dictionary<string, Dictionary<string, HashSet<string>>> groupsToRemoveEvent)
    {
        await OnDisconnectedBusAsync(groupsToRemoveEvent).ConfigureAwait(false);
    }

}