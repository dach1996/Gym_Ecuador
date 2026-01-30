using Common.ArtificialIntelligence.JsonIgnore;
using Newtonsoft.Json;

namespace Common.ArtificialIntelligence.Implementations.OpenAI.Model.Request;

/// <summary>
/// Request para procesar texto en OpenAI
/// </summary>
internal class OpenAIProcessTextRequest
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
    public Message[] Messages { get; set; }

    /// <summary>
    /// Temperatura para la generación (0.0 - 2.0)
    /// </summary>
    [JsonProperty("temperature", NullValueHandling = NullValueHandling.Ignore)]
    public double? Temperature { get; set; }

    /// <summary>
    /// Número máximo de tokens a generar
    /// </summary>
    [JsonProperty("max_tokens", NullValueHandling = NullValueHandling.Ignore)]
    public int? MaxTokens { get; set; }

    /// <summary>
    /// Formato de respuesta
    /// </summary>
    [JsonProperty("response_format", NullValueHandling = NullValueHandling.Ignore)]
    public ResponseFormat Response { get; set; }

    /// <summary>
    /// Herramientas disponibles para tool calling
    /// </summary>
    [JsonProperty("tools", NullValueHandling = NullValueHandling.Ignore)]
    public object[] Tools { get; set; }

    /// <summary>
    /// Mensaje en la conversación
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Rol del mensaje (system, user, assistant, tool)
        /// </summary>
        [JsonProperty("role")]
        public string Role { get; set; }

        /// <summary>
        /// Contenido del mensaje
        /// </summary>
        [JsonProperty("content", NullValueHandling = NullValueHandling.Include)]
        public object Content { get; set; }

        /// <summary>
        /// Llamadas a herramientas (solo para assistant)
        /// </summary>
        [JsonProperty("tool_calls", NullValueHandling = NullValueHandling.Ignore)]
        public object[] ToolCalls { get; set; }

        /// <summary>
        /// ID de la tool call (solo para role=tool)
        /// </summary>
        [JsonProperty("tool_call_id", NullValueHandling = NullValueHandling.Ignore)]
        public string ToolCallId { get; set; }

        /// <summary>
        /// Nombre de la herramienta (solo para role=tool)
        /// </summary>
        [JsonProperty("name", NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
    }


    /// <summary>
    /// Contenido del mensaje
    /// </summary>
    /// <remarks>
    /// Constructor
    /// </remarks>
    public class ContentTextInfo(string text)
    {
        /// <summary>
        /// Tipo de contenido
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; } = "text";

        /// <summary>
        /// Texto del mensaje
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; } = text;

    }

    /// <summary>
    /// Contenido de la imagen
    /// </summary>
    public class ContentImageInfo
    {
        /// <summary>
        /// Tipo de contenido
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ContentImageInfo(string url)
        {
            Type = "image_url";
            Url = new ImageUrl { Url = url };
        }

        /// <summary>
        /// URL de la imagen
        /// </summary>
        [JsonProperty("image_url")]
        public ImageUrl Url { get; set; }

        /// <summary>
        /// URL de la imagen
        /// </summary>
        public class ImageUrl
        {
            /// <summary>
            /// URL de la imagen
            /// </summary>
            [JsonProperty("url")]
            [IgnoreSensitve]
            public string Url { get; set; }
        }
    }


    /// <summary>
    /// Formato de respuesta
    /// </summary>
    public class ResponseFormat(string type)
    {
        /// <summary>
        /// Tipo de formato (text, json_object)
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; } = type;
    }
}
