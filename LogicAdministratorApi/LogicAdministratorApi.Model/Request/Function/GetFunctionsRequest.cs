using System.Text.Json.Serialization;
using Common.WebCommon.Models;
using LogicAdministratorApi.Model.Response.Function;

namespace LogicAdministratorApi.Model.Request.Function;

/// <summary>
/// Solicitud para obtener todas las funciones
/// </summary>
public class GetFunctionsRequest : IApiBaseRequest<GetFunctionsResponse>
{
    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}
