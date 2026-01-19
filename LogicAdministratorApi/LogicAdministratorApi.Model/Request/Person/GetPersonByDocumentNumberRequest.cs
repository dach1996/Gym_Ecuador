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
}

