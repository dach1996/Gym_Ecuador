using LogicApi.Model.Response.Ticket;
namespace LogicApi.Model.Request.Ticket;
/// <summary>
/// Obtiene los Tickets
/// </summary>
public class GetTicketsAvailableRequest : IApiBaseRequest<GetTicketsAvailableResponse>
{
    /// <summary>
    /// Fecha Desde la cuál buscar
    /// </summary>
    /// <value></value>
    [Required]
    public DateTime DateTimeFrom { get; set; }

    /// <summary>
    /// Fecha hasta la cuál buscar
    /// </summary>
    /// <value></value>
    [Required]
    public DateTime DateTimeTo { get; set; }

    /// <summary>
    /// Código Lugar Origen
    /// </summary>
    /// <value></value>
    [Required]
    public string PlaceCodeOrigin { get; set; }

    /// <summary>
    /// Código Lugar Destino
    /// </summary>
    /// <value></value>
    [Required]
    public string PlaceCodeDestination { get; set; }

    /// <summary>
    /// Tipos de buses
    /// </summary>
    /// <value></value>
    public IEnumerable<string> BusTypes { get; set; }

    /// <summary>
    /// Códigos de companias a buscar
    /// </summary>
    /// <returns></returns>
    public IEnumerable<string> CompanyCodes { get; set; }

    /// <summary>
    /// Códigos de Estaciones
    /// </summary>
    /// <returns></returns>
    public IEnumerable<string> StationCodes { get; set; }

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

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }
}
