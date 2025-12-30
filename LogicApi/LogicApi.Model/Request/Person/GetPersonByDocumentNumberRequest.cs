using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.Person;

using Common.WebCommon.Models;
namespace LogicApi.Model.Request.Person;

/// <summary>
/// Solicitud para obtener una persona por número de cédula
/// </summary>
public class GetPersonByDocumentNumberRequest : IApiBaseRequest<GetPersonByDocumentNumberResponse>
{
    /// <summary>
    /// Número de documento de la persona
    /// </summary>
    [Required]
    [StringLength(50)]
    public string DocumentNumber { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    /// <param name="documentNumber"></param>
    public GetPersonByDocumentNumberRequest(string documentNumber, ContextRequest contextRequest)
    {
        ContextRequest = contextRequest; 
        DocumentNumber = documentNumber;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetPersonByDocumentNumberRequest()
    {
    }
}

