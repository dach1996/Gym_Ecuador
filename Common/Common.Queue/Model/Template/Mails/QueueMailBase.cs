namespace Common.Queue.Model.Template.Mails;

/// <summary>
/// Clase base para Queue mail
/// </summary>
public  abstract class QueueMailBase
{
    /// <summary>
    /// Enviar A
    /// </summary>
    /// <value></value>
    public IEnumerable<string> To { get; set; }
    
    /// <summary>
    /// Enviar Oculto A
    /// </summary>
    /// <value></value>
    public IEnumerable<string> ToCco { get; set; }
}