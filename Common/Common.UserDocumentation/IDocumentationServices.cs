using Common.UserDocumentation.Models.Request;
using Common.UserDocumentation.Models.Response;

namespace Common.UserDocumentation;
/// <summary>
/// Servicios de Documentación
/// </summary>
public interface IDocumentationServices
{
    /// <summary>
    /// Verificar documentación
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<VerifyDocumentResponse> GetPersonInformationAsync(VerifyDocumentRequest request);
}