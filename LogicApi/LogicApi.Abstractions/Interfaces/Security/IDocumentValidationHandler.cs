using LogicApi.Model.Request.Security;
using LogicApi.Model.Response;

namespace LogicApi.Abstractions.Interfaces.Security;
public interface IDocumentValidationHandler
{
    /// <summary>
    /// Formas de Logín
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<HandlerResponse> Handle(DocumentValidationRequest request);
}
