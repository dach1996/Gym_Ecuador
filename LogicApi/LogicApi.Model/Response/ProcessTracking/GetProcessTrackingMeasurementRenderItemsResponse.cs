using Common.WebCommon.Models;

namespace LogicApi.Model.Response.ProcessTracking;

/// <summary>
/// Respuesta con items de medidas para renderizado del formulario
/// </summary>
public class GetProcessTrackingMeasurementRenderItemsResponse
{
    /// <summary>
    /// Listado de medidas configurables para el formulario
    /// </summary>
    public List<ProcessTrackingMeasurementRenderItem> Items { get; set; } = [];
}
