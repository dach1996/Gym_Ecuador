using Common.WebCommon.Models.Enum;
using LogicApi.Model.Response.Order;
namespace LogicApi.Model.Request.Order;
/// <summary>
/// Request para generar Orden
/// </summary>
public class GetMyOrdersPaginatedRequest : IPaginatorSortApiRequest<GetMyOrdersPaginatedResponse>
{
    /// <summary>
    /// Tamaño de la página
    /// </summary>
    /// <value></value>
    [Required]
    [Range(1, long.MaxValue)]
    public int PageSize { get; set; }

    /// <summary>
    /// Número de Página
    /// </summary>
    /// <value></value>
    [Required]
    [Range(1, long.MaxValue)]
    public int PageNumber { get; set; }

    /// <summary>
    /// Tipo de ordenamiento
    /// </summary>
    /// <value></value>
    [Required]
    public SortableType SortableType { get; set; } = SortableType.Desc;

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    /// <param name="pageSize"></param>
    public GetMyOrdersPaginatedRequest(
        ContextRequest contextRequest,
        int pageSize)
    {
        ContextRequest = contextRequest;
        PageNumber = 1;
        PageSize = pageSize;
        SortableType = SortableType.Desc;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public GetMyOrdersPaginatedRequest()
    {

    }
}
