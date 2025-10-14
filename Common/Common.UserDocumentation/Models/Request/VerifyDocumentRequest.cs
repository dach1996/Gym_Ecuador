namespace Common.UserDocumentation.Models.Request;
/// <summary>
/// Request para verificar Bin
/// </summary>
public class VerifyDocumentRequest
{
    /// <summary>
    /// Bin de petición
    /// </summary>
    /// <value></value>
    public string DocumentNumber { get; set; }

    /// <summary>
    /// Document Number
    /// </summary>
    /// <param name="documentNumber"></param>
    public VerifyDocumentRequest(string documentNumber) => DocumentNumber = documentNumber;
}