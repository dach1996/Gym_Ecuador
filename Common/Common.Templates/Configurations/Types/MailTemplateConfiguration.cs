namespace Common.Templates.Configurations.Types;

/// <summary>
/// Configuración de Template de Mail
/// </summary>
public class MailTemplateConfiguration : ConfigurationBase
{
    /// <summary>
    /// Información
    /// </summary>
    /// <value></value>
   public MailInformation Information { get; set; }
}

/// <summary>
/// Información de Mail
/// </summary>
public class MailInformation
{
    /// <summary>
    /// Items
    /// </summary>
    /// <value></value>
    public List<MailTemplateItem> Templates { get; set; }
}

/// <summary>
/// Item de Template de Mail
/// </summary>
public class MailTemplateItem
{
    /// <summary>
    /// Identificador
    /// </summary>
    /// <value></value>
    public string Identifier { get; set; }

    /// <summary>
    /// Código
    /// </summary>
    /// <value></value>
    public string Code { get; set; }
}