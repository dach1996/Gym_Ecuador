using Newtonsoft.Json;
namespace Common.Messages.Models;

/// <summary>
/// Modelos de mensaje
/// </summary>
public class MessageModel
{
    /// <summary>
    /// Mensajes
    /// </summary>
    /// <value></value>
    public List<Message> Messages { get; set; }
}

/// <summary>
/// Mensaje
/// </summary>
public class Message
{
    /// <summary>
    /// Código
    /// </summary>
    /// <value></value>
    public int Code { get; set; }

    /// <summary>
    /// Item
    /// </summary>
    /// <value></value>
    [JsonProperty("Message")]
    public IDictionary<UserLanguage, string> ItemMessage { get; set; }

}
