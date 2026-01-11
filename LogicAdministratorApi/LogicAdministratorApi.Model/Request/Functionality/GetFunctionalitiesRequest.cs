using Common.WebCommon.Models;
using LogicAdministratorApi.Model.Response.Functionality;

namespace LogicAdministratorApi.Model.Request.Functionality;

/// <summary>
/// Solicitud para obtener todas las funcionalidades
/// </summary>
public class GetFunctionalitiesRequest : IApiBaseRequest<GetFunctionalitiesResponse>
{
    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}
