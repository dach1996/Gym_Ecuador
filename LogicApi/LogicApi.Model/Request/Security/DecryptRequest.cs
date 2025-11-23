using System.ComponentModel.DataAnnotations;
using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.Security;

using Common.WebCommon.Models;
namespace LogicApi.Model.Request.Security;

/// <summary>
/// Modelo Request para desencryptar 
/// </summary>
public class DecryptRequest : IRequest<DecryptResponse>, IApiBaseRequest
{
 /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }

    /// <summary>
    ///  Diccionario de Textos encriptados
    /// </summary>
    /// <value></value>
    [Required]
    public IEnumerable<string> ListTextsEncrypt { get; set; }

    /// <summary>
    /// Tiene Codificación
    /// </summary>
    /// <value></value>
    public bool HasEncode { get; set; }

    /// <summary>
    /// Constructor con diccionario
    /// </summary>
    /// <param name="listTextsEncrypt"></param>
    /// <param name="contextRequest"></param>
    public DecryptRequest(IEnumerable<string> listTextsEncrypt, ContextRequest contextRequest)
    {
        ListTextsEncrypt = listTextsEncrypt;
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Constructor con diccionario
    /// </summary>
    /// <param name="text"></param>
    /// <param name="contextRequest"></param>
    /// <param name="hasEncode"></param>
    public DecryptRequest(string text, ContextRequest contextRequest, bool hasEncode = false)
    {
        ListTextsEncrypt = [text];
        ContextRequest = contextRequest;
        HasEncode = hasEncode;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public DecryptRequest()
    {

    }
}
