namespace Common.Cooperative.Models.Request;
/// <summary>
/// Request obtener los viajes disponibles 
/// </summary>
public class GetAvailableTripsRequest
{
    /// <summary>
    /// Fecha Desde la cuál buscar
    /// </summary>
    /// <value></value>
    public DateTime DateTimeFrom { get; set; }

    /// <summary>
    /// Fecha hasta la cuál buscar
    /// </summary>
    /// <value></value>
    public DateTime DateTimeTo { get; set; }

    /// <summary>
    /// Código Lugar Origen
    /// </summary>
    /// <value></value>
    public string PlaceCodeOrigin { get; set; }

    /// <summary>
    /// Código Lugar Destino
    /// </summary>
    /// <value></value>
    public string PlaceCodeDestination { get; set; }

    /// <summary>
    /// Tipos de buses
    /// </summary>
    /// <value></value>
    public IEnumerable<string> BusTypes { get; set; }

    /// <summary>
    /// Precio mínimo
    /// </summary>
    /// <value></value>
    public decimal? MinPrice { get; set; }

    /// <summary>
    /// Precio máximo
    /// </summary>
    /// <value></value>
    public decimal? MaxPrice { get; set; }

}