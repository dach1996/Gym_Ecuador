using Common.ArtificialIntelligence.Model.Request;

namespace Common.ArtificialIntelligence.Model.Response;

/// <summary>
/// Response para procesar tool calling con IA
/// </summary>
public class ProcessToolCallResponse
{
    /// <summary>
    /// Mensaje de respuesta del asistente
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// Llamadas a herramientas solicitadas por el asistente
    /// </summary>
    public List<ToolCall>? ToolCalls { get; set; }

    /// <summary>
    /// Razón por la que terminó la generación (stop, tool_calls, length, etc.)
    /// </summary>
    public string FinishReason { get; set; }

    /// <summary>
    /// Indica si el asistente solicita ejecutar herramientas
    /// </summary>
    public bool RequiresToolExecution => ToolCalls?.Any() == true;
}
