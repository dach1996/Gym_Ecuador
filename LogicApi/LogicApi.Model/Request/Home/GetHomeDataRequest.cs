using LogicApi.Model.Response.Home;
using Common.WebCommon.Models;

namespace LogicApi.Model.Request.Home;

/// <summary>
/// Solicitud para obtener datos del home/dashboard
/// </summary>
public class GetHomeDataRequest : IApiBaseRequest<GetHomeDataResponse>
{
    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}

