using Common.ArtificialIntelligence.Model.Common;

namespace Common.ArtificialIntelligence.Model.Request;

/// <summary>   
/// Request para procesar texto
/// </summary>
public class ProcessTextRequest(string behavior, string indications, ProcessResponseType responseType)
{
    /// <summary>
    /// Texto a procesar
    /// </summary>
    public string Behavior { get; set; } = behavior;

    /// <summary>
    /// Indicaciones para procesar imagen
    /// </summary>
    public string Indications { get; set; } = indications;

    /// <summary>
    /// Tipo de respuesta
    /// </summary>
    public ProcessResponseType ResponseType { get; set; } = responseType;
}