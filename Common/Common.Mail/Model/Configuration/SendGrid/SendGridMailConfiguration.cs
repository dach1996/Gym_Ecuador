namespace Common.Mail.Model.Configuration.SendGrid;
public class SendGridConfiguration
{
    /// <summary>
    /// Api Key
    /// </summary>
    /// <value></value>
    public string ApiKey { get; set; }

    /// <summary>
    /// Templates
    /// </summary>
    /// <value></value>
    public IEnumerable<Template> Templates { get; set; }
}

/// <summary>
/// Modelo de Template
/// </summary>
public class Template
{
    /// <summary>
    /// Id de Template
    /// </summary>
    /// <value></value>
    public string Identifier { get; set; }

    /// <summary>
    /// Origen del Correo
    /// </summary>
    /// <value></value>
    public string From { get; set; }

    /// <summary>
    /// Código
    /// </summary>
    /// <value></value>
    public string Code { get; set; }
}