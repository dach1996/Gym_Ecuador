
using Common.ArtificialIntelligence.Model.Request;
using Common.ArtificialIntelligence.Model.Response;

namespace Common.ArtificialIntelligence;

/// <summary>
/// Interfaz para consultas sobre la IA
/// </summary>
public interface IArtificialIntelligence
{
    /// <summary>
    /// Procesa texto con Inteligencia Artificial
    /// </summary>  
    /// <param name="request">Request de texto a procesar</param>
    /// <returns>Response de texto a procesar</returns>
    Task<string> ProcessTextAsync(ProcessTextRequest request);

    /// <summary>
    /// Procesa documento con Inteligencia Artificial
    /// </summary>
    /// <param name="request">Request de documento a procesar</param>
    /// <returns>Response de documento a procesar</returns>
    Task<string> ProcessDocumentAsync(ProcessDocumentRequest request);

    /// <summary>
    /// Procesa tool calling con Inteligencia Artificial
    /// </summary>
    /// <param name="request">Request de tool calling a procesar</param>
    /// <returns>Response de tool calling procesado</returns>
    Task<ProcessToolCallResponse> ProcessToolCallAsync(ProcessToolCallRequest request);
}
