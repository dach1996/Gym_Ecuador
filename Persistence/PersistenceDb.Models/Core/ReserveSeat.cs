using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersistenceDb.Models.Administration;
using PersistenceDb.Models.Authentication;
using PersistenceDb.Models.Enums;
using PersistenceDb.Models.Models;

namespace PersistenceDb.Models.Core;
/// <summary>
/// Tabla Reserva Asiento
/// </summary>
[Table(name: "RESERVA_ASIENTO", Schema = "CORE")]
public class ReserveSeat : IEntityControlRow
{
    /// <summary>
    /// Id de Reserva Asiento
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("RSA_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Guid de Reserva Asiento
    /// </summary>
    [Column("RSA_GUID")]
    [Required]
    public Guid Guid { get; set; }

    /// <summary>
    /// Id de Ruta
    /// </summary>
    [Column("RUT_ID")]
    [ForeignKey(nameof(CooperativeRoute))]
    public int CooperativeRouteId { get; set; }

    /// <summary>
    /// Id de Usuario
    /// </summary>
    [Column("USR_ID")]
    [ForeignKey(nameof(User))]
    public int? UserId { get; set; }

    /// <summary>
    /// Fecha de Registro
    /// </summary>
    [Column("RSA_FECHA_REGISTRO")]
    [Required]
    public DateTime DateTimeRegister { get; set; }

    /// <summary>
    /// Estado
    /// </summary>
    [Column("RSA_ESTADO")]
    public SeatState State { get; set; }

    /// <summary>
    /// Identificador de Asiento
    /// </summary>
    [Required]
    [Column("RSA_IDENTIFICADOR_ASIENTO")]
    public string SeatIdentifier { get; set; }

    /// <summary>
    /// Identificador de Piso
    /// </summary>
    [Column("PDB_ID")]
    [Required]
    [ForeignKey(nameof(FloorDiagramBusCooperative))]
    public int FloorBusCooperativeId { get; set; }

    /// <summary>
    /// Fecha de expiración de reserva
    /// </summary>
    [Column("RSA_FECHA_EXPIRACION")]
    public DateTime? DateTimeExpiration { get; set; }

    /// <summary>
    /// Control de Versión
    /// </summary>
    [Column("RSA_CONTROL_VERSION")]
    public DateTime RowControl { get; set; }

    /// <summary>
    /// Id de Mecanismo
    /// </summary>
    [Column("MEC_ID")]
    [ForeignKey(nameof(QueueMessage))]
    public int? QueueMessageId { get; set; }

    /// <summary>
    /// Verifica si está reservado
    /// </summary>
    [NotMapped]
    public bool IsReserved => State == SeatState.Reserved;

    /// <summary>
    /// Piso de cooperativa
    /// </summary>
    public FloorDiagramBusCooperative FloorDiagramBusCooperative { get; set; }

    /// <summary>
    /// Usuario asociado
    /// </summary>
    public User User { get; set; }

    /// <summary>
    /// Ruta
    /// </summary>
    public CooperativeRoute CooperativeRoute { get; set; }

    /// <summary>
    /// Mecanismo
    /// </summary>
    public QueueMessage QueueMessage { get; set; }
}
