using Common.ArtificialIntelligence.Model.Configuration;

namespace Common.ArtificialIntelligence.Implementations.OpenAI.Model.Configuration;

/// <summary>
/// Configuración de OpenAI
/// </summary>
public class OpenAIConfiguration : ArtificialIntelligenceImplementation
{
    /// <summary>
    /// URL base para la API de OpenAI
    /// </summary>
    public string BaseUrl { get; set; }

    /// <summary>
    /// Api Key de OpenAI
    /// </summary>
    public string ApiKey { get; set; }

    /// <summary>
    /// Modelo de OpenAI (ej: gpt-3.5-turbo, gpt-4, etc.)
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    /// Organización de OpenAI (opcional)
    /// </summary>
    public string Organization { get; set; }

    /// <summary>
    /// Temperatura para la generación (0.0 - 2.0)
    /// </summary>
    public double Temperature { get; set; }

    /// <summary>
    /// Número máximo de tokens a generar
    /// </summary>
    public int? MaxTokens { get; set; }
}
