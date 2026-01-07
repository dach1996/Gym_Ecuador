namespace Common.PushNotification.Configuration.Indigitall;
/// <summary>
/// Configuración de Indigital
/// </summary>
public class IndigitallConfiguration
{
    /// <summary>
    /// Url Base
    /// </summary>
    /// <value></value>
    public int ApplicationId { get; set; }

    /// <summary>
    /// TimeOut
    /// </summary>
    /// <value></value>
    public double Timeout { get; set; }

    /// <summary>
    /// Server Key
    /// </summary>
    /// <value></value>
    public string ServerKey { get; set; }

    /// <summary>
    /// Información Adicional
    /// </summary>
    /// <value></value>
    public IDictionary<string, string> AdditionalData { get; set; }

    /// <summary>
    /// Configuración segura
    /// </summary>
    /// <value></value>
    public TypeConfiguration SecuryConfiguration { get; set; }

    /// <summary>
    /// Configuración de Campaña
    /// </summary>
    /// <value></value>
    public TypeConfiguration CampaignConfiguration { get; set; }
}

/// <summary>
/// Clase base para configuración
/// </summary>
public class TypeConfiguration
{
    /// <summary>
    /// Url Base
    /// </summary>
    /// <value></value>
    public string BaseUrl { get; set; }

    /// <summary>
    /// Id de Campaña para dispositivos
    /// </summary>
    /// <value></value>
    public string CampaignId { get; set; }
}

/// <summary>
/// Tópicos
/// </summary>
public class CampaignTypeConfiguration : TypeConfiguration
{
    /// <summary>
    /// Tópicos
    /// </summary>
    /// <value></value>
    public IDictionary<string, string> Topics { get; set; }
}
