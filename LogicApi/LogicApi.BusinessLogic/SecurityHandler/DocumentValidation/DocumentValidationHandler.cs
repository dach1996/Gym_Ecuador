using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Request.Security;

namespace LogicApi.BusinessLogic.SecurityHandler.DocumentValidation;
/// <summary>
/// Validación de Documento
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public abstract class DocumentValidationHandler(
    ILogger<DocumentValidationHandler> logger,
    IPluginFactory pluginFactory) : SecurityBase<DocumentValidationRequest, HandlerResponse>(logger, pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<HandlerResponse> Handle(DocumentValidationRequest request, CancellationToken cancellationToken)
    {
        ContextRequest = request.ContextRequest as ContextRequest;
        return await Task.FromResult(HandlerResponse.Complete());
    }
}