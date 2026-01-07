namespace Common.Mail.Model.Configuration.Mailgun;
public class MailgunConfiguration
{
    /// <summary>
    /// Api Key de Mailgun
    /// </summary>
    /// <value></value>
    public string ApiKey { get; set; }

    /// <summary>
    /// Dominio de Mailgun
    /// </summary>
    /// <value></value>
    public string Domain { get; set; }

    /// <summary>
    /// Base URL de la API de Mailgun (por defecto: https://api.mailgun.net)
    /// </summary>
    /// <value></value>
    public string BaseUrl { get; set; } = "https://api.mailgun.net";
}

