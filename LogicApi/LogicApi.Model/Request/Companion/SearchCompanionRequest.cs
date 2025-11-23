using Common.WebApi.Models.ContextRequestModel;
using LogicApi.Model.Response.Companion;

using Common.WebCommon.Models;
namespace LogicApi.Model.Request.Companion;
/// <summary>
/// Request para buscar compañero de viaje
/// </summary>
public class SearchCompanionRequest : IApiBaseRequest<SearchCompanionResponse>
{
    /// <summary>
    /// Número de Identificación
    /// </summary>
    /// <value></value>
    [Required]
    [StringLength(50)]
    public string DocumentNumber { get; set; }

 /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}