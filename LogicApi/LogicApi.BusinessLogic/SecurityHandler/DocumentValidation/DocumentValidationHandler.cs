using LogicApi.Abstractions.Interfaces.Security;
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
        ContextRequest = request.ContextRequest;
        var implementationName = (await GetItemsCatalogCodesResponseByFile(EnumLogicApi.CatalogsTypeItemsCodes.DocumentType.GetEnumMember()).ConfigureAwait(false))
            ?.FirstOrDefault(t => t.Code == request.DocumentTypeCode)?.Enum?.ToUpper()
            ?? throw new InvalidOperationException($"No se encuentra el Item de Catálogo de archivo: '{request.DocumentTypeCode}'");
        return await PluginFactory.GetPlugin<IDocumentValidationHandler>(implementationName).Handle(request).ConfigureAwait(false);
    }
}