using System.Collections.Concurrent;
using Common.Utils.Extensions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace Common.WebHub.ControllerHub;

public abstract class ConcurrentHub(ILogger<ConcurrentHub> logger) : Hub
{

    /// <summary>
    /// Diccionario de usuarios y sus conexiones
    /// /// </summary>
    /// <returns></returns>
    private static readonly ConcurrentDictionary<string, HashSet<string>> _userConnections = new();

    /// <summary>
    /// Diccionario de usuarios y sus conexiones
    /// /// </summary>
    /// <returns></returns>
    protected static ConcurrentDictionary<string, HashSet<string>> GetConnectedUsers => _userConnections;

    /// <summary>
    /// Diccionario de grupos y sus usuarios
    /// /// </summary>
    /// <returns></returns>
    private static readonly ConcurrentDictionary<string, Dictionary<string, HashSet<string>>> _groupUsers = new();

    /// <summary>
    /// Diccionario de grupos y sus usuarios
    /// /// </summary>
    /// <returns></returns>
    protected static ConcurrentDictionary<string, Dictionary<string, HashSet<string>>> GetGroupUsers => _groupUsers;

    /// <summary>
    /// Diccionario de grupos y sus usuarios
    /// </summary>
    /// <param name="groupName"></param>
    /// <returns></returns>

    protected async Task AddToGroup(string groupName)
    {
        var userGuid = Context.UserIdentifier;
        if (!userGuid.IsNullOrEmpty())
        {
            var connectionId = Context.ConnectionId;
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            _groupUsers.AddOrUpdate(
                groupName,
                _ => new Dictionary<string, HashSet<string>> { { userGuid, [connectionId] } },  // Crea el grupo si no existe
                (_, existingUsers) =>
                {
                    // Si el usuario no está en el grupo, lo agregamos con la nueva conexión
                    if (!existingUsers.TryGetValue(userGuid, out var connections))
                        existingUsers[userGuid] = [connectionId];
                    else
                        // Si el usuario ya está en el grupo, agregamos la conexión
                        connections.Add(connectionId);
                    return existingUsers;
                }
            );
            logger.LogDebug("Usuario {UserGuid} agregado al grupo {GroupName}", userGuid, groupName);
        }
    }

    /// <summary>
    /// Elimina el usuario del grupo
    /// </summary>
    /// <param name="groupName"></param>
    /// <returns></returns>
    public async Task RemoveFromGroup(string groupName)
    {
        var userGuid = Context.UserIdentifier;
        if (!userGuid.IsNullOrEmpty())
            await RemoveFromGroupAsync(groupName, userGuid, Context.ConnectionId);
    }

    /// <summary>
    /// Elimina el usuario del grupo
    /// </summary>
    /// <param name="groupName"></param>
    /// <param name="userGuid"></param>
    /// <param name="connectionId"></param>
    private async Task<Dictionary<string, HashSet<string>>> RemoveFromGroupAsync(string groupName, string userGuid, string connectionId)
    {
        await Groups.RemoveFromGroupAsync(connectionId, groupName);
        _groupUsers.AddOrUpdate(
            groupName,
            _ => [], // No debería suceder, pero previene errores
            (_, existingUsers) =>
            {
                if (existingUsers.TryGetValue(userGuid, out var connectionsByUser))
                {
                    connectionsByUser.Remove(connectionId); // Eliminar solo esta conexión
                    if (connectionsByUser.Count == 0)
                        existingUsers.Remove(userGuid); // Si ya no tiene conexiones, eliminar usuario
                }
                return existingUsers;
            }
        );

        // Verificar si el grupo quedó vacío y eliminarlo
        if (_groupUsers.TryGetValue(groupName, out var users) && users.Count == 0)
            _groupUsers.TryRemove(groupName, out _);
        logger.LogDebug("Usuario {UserGuid} eliminado del grupo {GroupName} con ConnectionId {ConnectionId}", userGuid, groupName, connectionId);
        return _groupUsers.TryGetValue(groupName, out var remainingUsers) ? remainingUsers : [];
    }


    /// <summary>
    /// Obtiene los grupos del usuario
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    protected static HashSet<string> GetUsersByGroup(string groupName)
    {
        _groupUsers.TryGetValue(groupName, out var groups);
        return [.. groups?.Keys ?? Enumerable.Empty<string>()];
    }

    /// <summary>
    /// Obtiene los grupos del usuario
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    protected string[] GetContextUserGroups()
    {
        var userGuid = Context.UserIdentifier;
        if (userGuid.IsNullOrEmpty())
            return [];
        return [.. _groupUsers.Where(group => group.Value.Keys.Contains(userGuid)).Select(group => group.Key)];
    }

    /// <summary>
    /// Sobreescribe el evento conectar
    /// </summary>
    /// <returns></returns>
    public override async Task OnConnectedAsync()
    {
        var userGuid = Context.UserIdentifier;
        var connectionId = Context.ConnectionId;
        if (!userGuid.IsNullOrEmpty())
            _userConnections.AddOrUpdate(
                userGuid,
                _ => [connectionId], // Si el usuario no existe, lo crea
                (_, existingConnections) =>
                {
                    existingConnections.Add(connectionId);
                    return existingConnections;
                }
            );
        logger.LogDebug("Nueva Conexión Usuario {@UserGuid} Dispositivo N° {@CountConnections} conectado con ConnectionId {@ConnectionId}", userGuid, _userConnections[userGuid].Count, connectionId);
        await base.OnConnectedAsync();
    }

    protected event Func<Dictionary<string, Dictionary<string, HashSet<string>>>, Task> OnCustomDisconnectedEvent;

    /// <summary>
    /// Sobreescribe el evento desconectar
    /// </summary>
    /// <param name="exception"></param>
    /// <returns></returns>
    public override async Task OnDisconnectedAsync(Exception exception)
    {
        var userGuid = Context.UserIdentifier;
        var connectionId = Context.ConnectionId;
        var groupsToRemoveEvent = new Dictionary<string, Dictionary<string, HashSet<string>>>();
        if (!userGuid.IsNullOrEmpty() && _userConnections.ContainsKey(userGuid))
        {
            // Eliminar al usuario de todos los grupos a los que pertenece
            var groupsToRemove = _groupUsers
                .Where(g => g.Value.Keys.Contains(userGuid))
                .Select(g => g.Key)
                .ToList();  // Obtener los nombres de los grupos a los que el usuario pertenece

            foreach (var groupName in groupsToRemove)
                groupsToRemoveEvent.Add(groupName, await RemoveFromGroupAsync(groupName, userGuid, connectionId));
            _userConnections.AddOrUpdate(
                userGuid,
                _ => [], // No debería suceder, pero previene errores
                (_, existingConnections) =>
                {
                    existingConnections.Remove(connectionId);
                    return existingConnections;
                }
            );
            // Si ya no tiene conexiones activas, lo eliminamos del diccionario
            if (_userConnections[userGuid].Count == 0)
                _userConnections.TryRemove(userGuid, out _);

        }
        // Lanzar evento de desconexión
        if (OnCustomDisconnectedEvent is not null)
            await OnCustomDisconnectedEvent.Invoke(groupsToRemoveEvent).ConfigureAwait(false);
        _ = _groupUsers.TryGetValue(userGuid, out var deviceConnected);
        logger.LogDebug("Desconexión Usuario {@UserGuid} Dispositivo Restantes N° {@CountConnections} desconectado con ConnectionId {@ConnectionId}", userGuid, deviceConnected?.Count ?? 0, connectionId);
        await base.OnDisconnectedAsync(exception);
    }

}
