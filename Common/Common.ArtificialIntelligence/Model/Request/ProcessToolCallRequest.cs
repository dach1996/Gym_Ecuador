using Common.ArtificialIntelligence.Model.Common;

namespace Common.ArtificialIntelligence.Model.Request;

/// <summary>
/// Request para procesar tool calling con IA
/// </summary>
public class ProcessToolCallRequest
{
    /// <summary>
    /// Constructor
    /// </summary>
    public ProcessToolCallRequest(
        string behavior,
        List<ToolCallMessage> messages,
        List<ToolDefinition> tools)
    {
        Behavior = behavior;
        Messages = messages;
        Tools = tools;
    }

    /// <summary>
    /// Comportamiento del sistema
    /// </summary>
    public string Behavior { get; set; }

    /// <summary>
    /// Mensajes de la conversación
    /// </summary>
    public List<ToolCallMessage> Messages { get; set; }

    /// <summary>
    /// Definiciones de herramientas disponibles
    /// </summary>
    public List<ToolDefinition> Tools { get; set; }
}

/// <summary>
/// Mensaje en una conversación con tool calling
/// </summary>
public class ToolCallMessage
{
    /// <summary>
    /// Rol del mensaje (system, user, assistant, tool)
    /// </summary>
    public string Role { get; set; }

    /// <summary>
    /// Contenido del mensaje (solo para roles: system, user, assistant)
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// Llamadas a herramientas (solo para rol: assistant)
    /// </summary>
    public List<ToolCall>? ToolCalls { get; set; }

    /// <summary>
    /// ID de la tool call (solo para rol: tool)
    /// </summary>
    public string? ToolCallId { get; set; }

    /// <summary>
    /// Nombre de la herramienta (solo para rol: tool)
    /// </summary>
    public string? Name { get; set; }
}

/// <summary>
/// Llamada a una herramienta
/// </summary>
public class ToolCall
{
    /// <summary>
    /// ID único de la llamada
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    /// Tipo (siempre "function")
    /// </summary>
    public string Type { get; set; } = "function";

    /// <summary>
    /// Función a llamar
    /// </summary>
    public FunctionCall Function { get; set; }
}

/// <summary>
/// Información de la función a llamar
/// </summary>
public class FunctionCall
{
    /// <summary>
    /// Nombre de la función
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Argumentos de la función (JSON string)
    /// </summary>
    public string Arguments { get; set; }
}

/// <summary>
/// Definición de una herramienta
/// </summary>
public class ToolDefinition
{
    /// <summary>
    /// Tipo (siempre "function")
    /// </summary>
    public string Type { get; set; } = "function";

    /// <summary>
    /// Definición de la función
    /// </summary>
    public FunctionDefinition Function { get; set; }
}

/// <summary>
/// Definición de una función
/// </summary>
public class FunctionDefinition
{
    /// <summary>
    /// Nombre de la función
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Descripción de la función
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    /// Schema de los parámetros (JSON object)
    /// </summary>
    public object Parameters { get; set; }
}
