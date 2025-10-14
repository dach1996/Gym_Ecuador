namespace Common.Templates.Models.Types;
/// <summary>
/// Respuesta de Notificación Template
/// </summary>
public class NotificationTemplateResponse
{
    /// <summary>
    /// Título
    /// </summary>
    /// <value></value>
    public string Title { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="title"></param>
    /// <param name="body"></param>
    public NotificationTemplateResponse(string title, string body)
    {
        Title = title;
        Body = body;
    }

    /// <summary>
    /// Cuerpo
    /// </summary>
    /// <value></value>
    public string Body { get; set; }
}