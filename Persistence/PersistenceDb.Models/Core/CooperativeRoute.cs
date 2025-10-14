using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersistenceDb.Models.Administration;

namespace PersistenceDb.Models.Core;
/// <summary>
/// Tabla de Rutas de Cooperativa
/// </summary>
[Table(name: "RUTA_COOPERATIVA", Schema = "CORE")]
public class CooperativeRoute
{
    /// <summary>
    /// Id de la ruta
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("RUT_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Identificador único de la ruta
    /// </summary>
    [Column("RUT_GUID")]
    public Guid Guid { get; set; }

    /// <summary>
    /// Última fecha de la ruta
    /// </summary>
    [Column("RUT_FECHA_REGISTRO")]
    [Required]
    public DateTime DateTimeRegister { get; set; }

    /// <summary>
    /// Fecha de salida de Ruta
    /// </summary>
    [Column("RUT_FECHA_RUTA")]
    public DateTime DateTimeRoute { get; set; }

    /// <summary>
    /// Hora de salida de la ruta
    /// </summary>
    [Column("RUT_HORA_SALIDA")]
    [Required]
    public DateTime DateTimeRouteTime { get; set; }

    /// <summary>
    /// Hora de llegada de la ruta
    /// </summary>
    [Column("RUT_HORA_LLEGADA")]
    [Required]
    public DateTime DateTimeRouteTimeArrival { get; set; }

    /// <summary>
    /// Id de identificador de Viaje de servicio
    /// </summary>
    [Column("RUT_IDENTIFICADOR_VIAJE")]
    [StringLength(256)]
    [Required]
    public string TicketIdentifier { get; set; }

    /// <summary>
    /// Id de bus relacionado al bus de la cooperativa
    /// </summary>
    [Column("CPB_ID")]
    [ForeignKey(nameof(CooperativeBus))]
    public int CooperativeBusId { get; set; }

    /// <summary>
    /// Id de la cooperativa
    /// </summary>
    [Column("COP_ID")]
    [ForeignKey(nameof(Cooperative))]
    public int CooperativeId { get; set; }

    /// <summary>
    /// Id de punto de transporte de origen
    /// </summary>
    [Column("CPT_ID_ORIGEN")]
    [ForeignKey(nameof(CooperativeTransportPoint))]
    public int OriginTransportPointId { get; set; }

    /// <summary>
    /// Id de punto de transporte de destino
    /// </summary>
    [Column("CPT_ID_DESTINO")]
    [ForeignKey(nameof(CooperativeTransportPoint))]
    public int DestinationTransportPointId { get; set; }

    /// <summary>
    /// Asientos reservados
    /// </summary>
    public ICollection<ReserveSeat> ReserveSeats { get; set; }

    /// <summary>
    /// Coperativa
    /// </summary>
    public Cooperative Cooperative { get; set; }

    /// <summary>
    /// Punto de transporte de origen
    /// </summary>
    public CooperativeTransportPoint OriginTransportPoint { get; set; }

    /// <summary>
    /// Punto de transporte de destino
    /// </summary>
    public CooperativeTransportPoint DestinationTransportPoint { get; set; }

    /// <summary>
    /// Bus de cooperativa
    /// </summary>
    public CooperativeBus CooperativeBus { get; set; }
}
