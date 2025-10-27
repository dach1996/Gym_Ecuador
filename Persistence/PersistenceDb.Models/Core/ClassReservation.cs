using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersistenceDb.Models.Authentication;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla Reservas de Clase
/// </summary>
[Table(name: "RESERVAS_CLASE", Schema = "GIMNASIO")]
public class ClassReservation
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("RCL_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Guid
    /// </summary>
    [Required]
    [Column("RCL_GUID")]
    public Guid Guid { get; set; }

    /// <summary>
    /// Id del horario de clase
    /// </summary>
    [Required]
    [Column("HCL_ID")]
    public int ClassScheduleId { get; set; }

    /// <summary>
    /// Id de la persona
    /// </summary>
    [Required]
    [Column("PNA_ID")]
    public int PersonId { get; set; }

    /// <summary>
    /// Fecha de reserva
    /// </summary>
    [Required]
    [Column("RCL_FECHA_RESERVA")]
    public DateTime ReservationDate { get; set; }

    /// <summary>
    /// Estado de la reserva
    /// </summary>
    [Required]
    [StringLength(50)]
    [Column("RCL_ESTADO_RESERVA")]
    public string ReservationStatus { get; set; } // Confirmada, Cancelada, Asistida

    /// <summary>
    /// Fecha de registro
    /// </summary>
    [Column("RCL_FECHA_REGISTRO")]
    public DateTime? DateTimeRegister { get; set; }

    /// <summary>
    /// Id de usuario registrador
    /// </summary>
    [Column("USR_ID_REGISTRADOR")]
    public int? UserIdRegister { get; set; }

    /// <summary>
    /// Navegación al horario de clase
    /// </summary>
    [ForeignKey("ClassScheduleId")]
    public virtual ClassSchedule ClassSchedule { get; set; }

    /// <summary>
    /// Navegación a la persona
    /// </summary>
    [ForeignKey("PersonId")]
    public virtual Person Person { get; set; }
}
