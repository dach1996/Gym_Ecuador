namespace Common.UserDocumentation.Models.Response;

/// <summary>
/// Respuesta para verificar documento
/// </summary>
public class VerifyDocumentResponse
{
    /// <summary>
    /// Identificación 
    /// </summary>
    /// <value></value>
    public string Identification { get; set; }

    /// <summary>
    /// Nombres y Apellidos 
    /// </summary>
    /// <value></value>
    public string FullName { get; set; }

    /// <summary>
    /// Nombres 
    /// </summary>
    /// <value></value>
    public string Names { get; set; }

    /// <summary>
    /// Apellidos 
    /// </summary>
    /// <value></value>
    public string LastNames { get; set; }
}