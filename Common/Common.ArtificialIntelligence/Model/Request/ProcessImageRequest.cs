using Common.ArtificialIntelligence.Model.Common;

namespace Common.ArtificialIntelligence.Model.Request;

/// <summary>   
/// Request para procesar una imagen
/// </summary>
public class ProcessDocumentRequest(byte[] document, string behavior, string indications, ProcessResponseType responseType)
{
    /// <summary>
    /// Documento a procesar
    /// </summary>
    public byte[] Document { get; set; } = document;

    /// <summary>
    /// Comportamiento para procesar imagen
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