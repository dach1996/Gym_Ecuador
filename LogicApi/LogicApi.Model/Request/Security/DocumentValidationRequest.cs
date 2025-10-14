using LogicApi.Model.Response;
namespace LogicApi.Model.Request.Security;

/// <summary>
/// Modelo Request validar documento
/// </summary>
public class DocumentValidationRequest : IRequest<HandlerResponse>, IApiBaseRequest
{
    /// <summary>
    /// Código de Tipo de Documento
    /// </summary>
    /// <value></value>
    public string DocumentTypeCode { get; set; }
    
    /// <summary>
    /// Número de Documento
    /// </summary>
    /// <value></value>
    public string DocumentNumber { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }
}
