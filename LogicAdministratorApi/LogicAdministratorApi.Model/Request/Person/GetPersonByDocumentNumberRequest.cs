using Common.WebCommon.Models;
using LogicCommon.Model.Request.Person;
using LogicCommon.Model.Response.Person;

namespace LogicAdministratorApi.Model.Request.Person;

/// <summary>
/// Solicitud para obtener una persona por número de cédula
/// </summary>
public class GetPersonByDocumentNumberRequest : GetPersonByDocumentNumberCommonRequest, IApiBaseRequest<GetPersonByDocumentNumberResponse>
{
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
    public GetPersonByDocumentNumberRequest(CommonContextRequest contextRequest, string documentNumber) 
        : base(contextRequest, documentNumber)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetPersonByDocumentNumberRequest() : base()
    {
    }
}

