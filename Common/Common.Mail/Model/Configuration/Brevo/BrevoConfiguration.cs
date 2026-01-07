namespace Common.Mail.Model.Configuration.Brevo;

/// <summary>
/// Configuración de Brevo
/// </summary>
public class BrevoConfiguration
{
    /// <summary>
    /// Api Key de Brevo
    /// </summary>
    /// <value></value>
    public string ApiKey { get; set; }

    /// <summary>
    /// Base URL de la API de Brevo (por defecto: https://api.brevo.com)
    /// </summary>
    /// <value></value>
    public string BaseUrl { get; set; } = "https://api.brevo.com";
}

