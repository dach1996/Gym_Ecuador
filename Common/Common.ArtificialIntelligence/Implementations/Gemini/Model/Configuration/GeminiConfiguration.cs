using Common.ArtificialIntelligence.Model.Configuration;

namespace Common.ArtificialIntelligence.Implementations.Gemini.Model.Configuration;

/// <summary>
/// Configuración de Gemini
/// </summary>
public class GeminiConfiguration : ArtificialIntelligenceImplementation
{
    /// <summary>
    /// Path para la generación de texto
    /// </summary>
    public string BaseUrl { get; set; }

    /// <summary>
    /// Api Key de Gemini
    /// </summary>
    public string ApiKey { get; set; }

    /// <summary>
    /// Modelo de Gemini
    /// </summary>
    public string Model { get; set; }
}