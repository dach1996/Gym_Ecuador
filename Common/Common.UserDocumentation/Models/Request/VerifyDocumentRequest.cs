namespace Common.UserDocumentation.Models.Request;
/// <summary>
/// Request para verificar Bin
/// </summary>
/// <remarks>
/// Document Number
/// </remarks>
/// <param name="documentNumber"></param>
public class VerifyDocumentRequest(string documentNumber)
{
    /// <summary>
    /// Bin de petición
    /// </summary>
    /// <value></value>
    public string DocumentNumber { get; set; } = documentNumber;
}