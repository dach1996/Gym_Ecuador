using Common.ArtificialIntelligence.Model.Configuration;

namespace Common.ArtificialIntelligence.Implementations.DeepSeek.Model.Configuration;

/// <summary>
/// Configuración de DeepSeek
/// </summary>
public class DeepSeekConfiguration : ArtificialIntelligenceImplementation
{
    /// <summary>
    /// URL base para la API de DeepSeek
    /// </summary>
    public string BaseUrl { get; set; }

    /// <summary>
    /// Api Key de DeepSeek
    /// </summary>
    public string ApiKey { get; set; }

    /// <summary>
    /// Modelo de DeepSeek (ej: deepseek-chat, deepseek-coder, etc.)
    /// </summary>
    public string Model { get; set; }
}
