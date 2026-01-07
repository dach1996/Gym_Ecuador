using LogicAdministratorApi.Model.Response.NotificationPush;
using Common.WebCommon.Models;

namespace LogicAdministratorApi.Model.Request.NotificationPush;

/// <summary>
/// Solicitud para obtener notificaciones push enviadas de manera paginada
/// </summary>
public class GetNotificationPushesRequest : IPaginatorApiRequest<GetNotificationPushesResponse>
{
    /// <summary>
    /// Filtro por título (opcional)
    /// </summary>
    public string TitleFilter { get; set; }

    /// <summary>
    /// Filtro por fecha desde (opcional)
    /// </summary>
    public DateTime? DateFrom { get; set; }

    /// <summary>
    /// Filtro por fecha hasta (opcional)
    /// </summary>
    public DateTime? DateTo { get; set; }

    /// <summary>
    /// Número de página
    /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    public int PageNumber { get; set; }

    /// <summary>
    /// Tamaño de página
    /// </summary>
    [Required]
    [Range(1, int.MaxValue)]
    public int PageSize { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    public GetNotificationPushesRequest()
    {
    }
}

