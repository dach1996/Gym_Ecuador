using Common.WebCommon.Models;

namespace LogicApi.Model.Response.ProcessTracking;

/// <summary>
/// Respuesta de seguimientos de proceso paginados
/// </summary>
public class GetProcessTrackingsPaginatedResponse : IPaginatorResponse<ProcessTrackingItem>
{
    /// <summary>
    /// Lista de seguimientos de procesos
    /// </summary>
    public IEnumerable<ProcessTrackingItem> Registers { get; set; }

    /// <summary>
    /// Total de registros
    /// </summary>
    public int TotalRegister { get; set; }
}
