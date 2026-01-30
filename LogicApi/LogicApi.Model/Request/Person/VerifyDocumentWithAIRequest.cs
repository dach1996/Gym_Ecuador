using Common.WebCommon.Json;
using Common.WebCommon.Models;
using LogicApi.Model.Response.Person;

namespace LogicApi.Model.Request.Person;

/// <summary>
/// Request para verificar documento con IA
/// </summary>
public class VerifyDocumentWithAIRequest : IApiBaseRequest<VerifyDocumentWithAIResponse>
{
    /// <summary>
    /// Imagen del documento de identidad en Base64
    /// </summary>
    [Required(ErrorMessage = "La imagen de frente del documento es requerida")]
    [JsonCompresse]
    public string FrontImageBase64 { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }

}
