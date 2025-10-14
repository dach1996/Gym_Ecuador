namespace LogicApi.Model.Common;

/// <summary>
/// Información de Ruta en Cache
/// </summary>
public class RouteInformationCacheModel
{
    /// <summary>   
    /// Id de Ruta
    /// </summary>
    public int RouteId { get; set; }

    /// <summary>
    /// Guid de Ruta
    /// </summary>
    public Guid RouteGuid { get; set; }

    /// <summary>
    /// Id de Cooperativa
    /// </summary>
    public int CooperativeId { get; set; }

    /// <summary>
    /// Identificador de Ruta
    /// </summary>
    public string RouteIdentifier { get; set; }

    /// <summary>
    /// Fecha  de Ruta
    /// </summary>
    public DateTime DateRoute { get; set; }

    /// <summary>
    /// Id de Punto de Transporte de Origen
    /// </summary>
    public int OriginTransportPointId { get; set; }

    /// <summary>
    /// Id de Punto de Transporte de Destino
    /// </summary>
    public int DestinationTransportPointId { get; set; }

    /// <summary>
    /// Hora de salida de la ruta
    /// </summary>
    public DateTime DateTimeRouteTime { get; set; }

    /// <summary>
    /// Hora de llegada de la ruta
    /// </summary>
    public DateTime DateTimeRouteTimeArrival { get; set; }
}