using Microsoft.AspNetCore.SignalR;

namespace Common.WebHub.Provider;

public class GuidUserIdentifierProvider : IUserIdProvider
{
    /// <summary>
    /// Obtiene el ID del usuario
    /// </summary>
    /// <param name="connection"></param>
    /// <returns></returns>
    public string GetUserId(HubConnectionContext connection)
    {
        return connection.User?.FindFirst("jti")?.Value; // Extrae el GUID del JWT
    }
}