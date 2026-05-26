using Common.WebCommon.Attributes.CustomDataAnnotations;
using Common.WebCommon.Models;
using LogicApi.Model.Response.ProcessTracking;

namespace LogicApi.Model.Request.ProcessTracking;

/// <summary>
/// Solicitud para obtener comparación de seguimiento de proceso por funcionalidad
/// </summary>
public class GetProcessTrackingComparationByFunctionalityRequest : IApiBaseRequest<GetProcessTrackingComparationByFunctionalityResponse>
{

    /// <summary>
    /// Tipo de comparación por funcionalidad
    /// </summary>
    [ValidateEnum]
    public GetProcessTrackingComparationFunctionalityType Type { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Tipo de comparación por funcionalidad
    /// </summary>
    public enum GetProcessTrackingComparationFunctionalityType
    {
        /// <summary>
        /// By functionality
        /// </summary>
        Weight = 1,

        /// <summary>
        /// By measurements
        /// </summary>
        Measurements = 2,
    }
}
