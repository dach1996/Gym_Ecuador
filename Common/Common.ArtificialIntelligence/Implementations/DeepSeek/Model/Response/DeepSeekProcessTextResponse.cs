using Newtonsoft.Json;

namespace Common.ArtificialIntelligence.Implementations.DeepSeek.Model.Response;

/// <summary>
/// Response para procesar texto en DeepSeek
/// </summary>
internal class DeepSeekProcessTextResponse
{
    /// <summary>
    /// ID único de la respuesta
    /// </summary>
    [JsonProperty("id")]
    public string Id { get; set; }

    /// <summary>
    /// Objeto tipo (chat.completion)
    /// </summary>
    [JsonProperty("object")]
    public string Object { get; set; }

    /// <summary>
    /// Timestamp de creación
    /// </summary>
    [JsonProperty("created")]
    public long Created { get; set; }

    /// <summary>
    /// Modelo utilizado
    /// </summary>
    [JsonProperty("model")]
    public string Model { get; set; }

    /// <summary>
    /// Opciones de respuesta generadas
    /// </summary>
    [JsonProperty("choices")]
    public Choice[] Choices { get; set; }

    /// <summary>
    /// Información sobre el uso de tokens
    /// </summary>
    [JsonProperty("usage")]
    public UsageInformation Usage { get; set; }

    /// <summary>
    /// ID del sistema
    /// </summary>
    [JsonProperty("system_fingerprint", NullValueHandling = NullValueHandling.Ignore)]
    public string SystemFingerprint { get; set; }

    /// <summary>
    /// Opción de respuesta
    /// </summary>
    public class Choice
    {
        /// <summary>
        /// Índice de la opción
        /// </summary>
        [JsonProperty("index")]
        public int Index { get; set; }

        /// <summary>
        /// Mensaje de respuesta
        /// </summary>
        [JsonProperty("message")]
        public Message Message { get; set; }

        /// <summary>
        /// Log de probabilidades (opcional)
        /// </summary>
        [JsonProperty("logprobs", NullValueHandling = NullValueHandling.Ignore)]
        public object Logprobs { get; set; }

        /// <summary>
        /// Razón por la que terminó la generación
        /// </summary>
        [JsonProperty("finish_reason")]
        public string FinishReason { get; set; }
    }

    /// <summary>
    /// Mensaje de respuesta
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Rol del mensaje
        /// </summary>
        [JsonProperty("role")]
        public string Role { get; set; }

        /// <summary>
        /// Contenido del mensaje
        /// </summary>
        [JsonProperty("content")]
        public string Content { get; set; }

    }

    /// <summary>
    /// Información sobre el uso de tokens
    /// </summary>
    public class UsageInformation
    {
        /// <summary>
        /// Tokens utilizados en el prompt
        /// </summary>
        [JsonProperty("prompt_tokens")]
        public int PromptTokens { get; set; }

        /// <summary>
        /// Tokens generados en la respuesta
        /// </summary>
        [JsonProperty("completion_tokens")]
        public int CompletionTokens { get; set; }

        /// <summary>
        /// Total de tokens utilizados
        /// </summary>
        [JsonProperty("total_tokens")]
        public int TotalTokens { get; set; }

        /// <summary>
        /// Detalles de tokens de prompt (opcional)
        /// </summary>
        [JsonProperty("prompt_tokens_details", NullValueHandling = NullValueHandling.Ignore)]
        public object PromptTokensDetails { get; set; }

        /// <summary>
        /// Detalles de tokens de completión (opcional)
        /// </summary>
        [JsonProperty("completion_tokens_details", NullValueHandling = NullValueHandling.Ignore)]
        public object CompletionTokensDetails { get; set; }
    }
}
