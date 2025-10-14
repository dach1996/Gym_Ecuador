using LogicApi.Model.Response.Companion;

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
    public ContextRequest ContextRequest { get; set; }
}