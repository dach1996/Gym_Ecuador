using Newtonsoft.Json;
namespace Common.ArtificialIntelligence.Implementations.DeepSeek.Model.Request;

/// <summary>
/// Request para procesar texto en DeepSeek
/// </summary>
internal class DeepSeekProcessTextRequest
{
    /// <summary>
    /// Modelo a utilizar
    /// </summary>
    [JsonProperty("model")]
    public string Model { get; set; }

    /// <summary>
    /// Mensajes de la conversación
    /// </summary>
    [JsonProperty("messages")]
    public List<Message> Messages { get; set; }

    /// <summary>
    /// Si debe transmitir la respuesta
    /// </summary>
    [JsonProperty("stream")]
    public bool Stream { get; set; }

    /// <summary>
    /// Mensaje en la conversación
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Rol del mensaje (system, user, assistant)
        /// </summary>
        [JsonProperty("role")]
        public string Role { get; set; }

        /// <summary>
        /// Contenido del mensaje
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }
    }
}
