using Common.EventHub.Models.Request;
namespace Common.EventHub;
/// <summary>
/// Interfaz de Eventos
/// </summary>
public interface IEventHub
{
    /// <summary>
    /// Envío de evento por Grupo
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task SendMessageAsync(SendEventMessageByGroupRequest request);

}