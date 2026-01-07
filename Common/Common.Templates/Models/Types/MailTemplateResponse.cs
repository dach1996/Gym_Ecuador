namespace Common.Templates.Models.Types;
/// <summary>
/// Respuesta de Template de Mail
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="templateId"></param>
public class MailTemplateResponse(string templateId)
{
    /// <summary>
    /// Id de Template
    /// </summary>
    /// <value></value>
    public string TemplateId { get; set; } = templateId;
}