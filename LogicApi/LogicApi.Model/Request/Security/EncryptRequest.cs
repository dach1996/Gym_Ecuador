using LogicApi.Model.Response.Security;

namespace LogicApi.Model.Request.Security;

/// <summary>
/// Modelo Request para encriptar
/// </summary>
public class EncryptRequest : IRequest<EncryptResponse>, IApiBaseRequest
{
    /// <summary>
    ///  Diccionario de Textos
    /// </summary>
    /// <value></value>
    [Required]
    public IEnumerable<string> ListTexts { get; set; }

    /// <summary>
    /// Aplicar Codificación
    /// </summary>
    /// <value></value>
    public bool ApplyEncode { get; set; }

    /// <summary>
    /// Constructor con diccionario
    /// </summary>
    /// <param name="listTexts"></param>
    /// <param name="contextRequest"></param>
    public EncryptRequest(IEnumerable<string> listTexts, ContextRequest contextRequest)
    {
        ListTexts = listTexts;
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Constructor con un dato
    /// </summary>
    /// <param name="value"></param>
    /// <param name="contextRequest"></param>
    /// <param name="applyEncode"></param>
    public EncryptRequest(string value, ContextRequest contextRequest, bool applyEncode)
    {
        ListTexts = [value];
        ApplyEncode = applyEncode;
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public EncryptRequest()
    {

    }


    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }
}
