using Newtonsoft.Json;

namespace Common.ArtificialIntelligence.Implementations.OpenAI.Model.Response;

/// <summary>
/// Response para procesar texto en OpenAI
/// </summary>
internal class OpenAIProcessTextResponse
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

        /// <summary>
        /// Llamadas a herramientas
        /// </summary>
        [JsonProperty("tool_calls")]
        public ToolCall[] ToolCalls { get; set; }
    }

    /// <summary>
    /// Llamada a una herramienta
    /// </summary>
    public class ToolCall
    {
        /// <summary>
        /// ID de la llamada
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// Tipo (function)
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// Función
        /// </summary>
        [JsonProperty("function")]
        public FunctionInfo Function { get; set; }
    }

    /// <summary>
    /// Información de la función
    /// </summary>
    public class FunctionInfo
    {
        /// <summary>
        /// Nombre de la función
        /// </summary>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// Argumentos (JSON string)
        /// </summary>
        [JsonProperty("arguments")]
        public string Arguments { get; set; }
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
    }
}
