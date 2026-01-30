namespace Common.WebCommon.IaTemplateModel;
/// <summary>
/// Respuesta de la plantilla de la Artificial Intelligence
/// </summary>
public class IaTemplateResponse
{
    /// <summary>
    /// Behavior
    /// </summary>
    public string Behavior { get; set; }

    /// <summary>
    /// Indications
    /// </summary>
    public string Indications { get; set; }

    /// <summary>
    /// Template
    /// </summary>
    public string AiImplementation { get; set; }

    /// <summary>
    /// ResponseType
    /// </summary>
    public string ResponseType { get; set; }
}