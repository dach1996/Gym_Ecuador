using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla Servicios de Sucursal de Gimnasio
/// </summary>
[Table(name: "SUCURSAL_GIMNASIO_SERVICIO", Schema = "CORE")]
public class GymBranchService
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("SGS_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Guid
    /// </summary>
    [Required]
    [Column("SGS_GUID")]
    public Guid Guid { get; set; }

    /// <summary>
    /// Id de la sucursal
    /// </summary>
    [Required]
    [Column("SGY_ID")]
    public int GymBranchId { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    [Column("SGS_FECHA_REGISTRO")]
    public DateTime DateTimeRegister { get; set; }

    /// <summary>
    /// Nombre del servicio
    /// </summary>
    [Required]
    [StringLength(200)]
    [Column("SGS_NOMBRE")]
    public string Name { get; set; }

    /// <summary>
    /// Descripción del servicio
    /// </summary>
    [StringLength(1000)]
    [Column("SGS_DESCRIPCION")]
    public string Description { get; set; }

    /// <summary>
    /// Código del servicio
    /// </summary>
    [StringLength(50)]
    [Column("SGS_CODIGO")]
    public string Code { get; set; }

    /// <summary>
    /// Tipo de servicio (Item de Catálogo)
    /// Ej: Entrenamiento Personal, Clases Grupales, Spa, Nutrición, etc.
    /// </summary>
    [Column("ITC_TIPO_SERVICIO")]
    public int? ServiceTypeCatalogId { get; set; }

    /// <summary>
    /// Precio del servicio
    /// </summary>
    [Column("SGS_PRECIO")]
    [Precision(18, 2)]
    public decimal? Price { get; set; }

    /// <summary>
    /// Duración estimada en minutos
    /// </summary>
    [Column("SGS_DURACION_MINUTOS")]
    public int? DurationMinutes { get; set; }

    /// <summary>
    /// Capacidad máxima de personas para el servicio
    /// </summary>
    [Column("SGS_CAPACIDAD_MAXIMA")]
    public int? MaxCapacity { get; set; }

    /// <summary>
    /// Indica si el servicio requiere reserva previa
    /// </summary>
    [Required]
    [Column("SGS_REQUIERE_RESERVA")]
    public bool RequiresReservation { get; set; }

    /// <summary>
    /// Indica si el servicio está incluido en la membresía
    /// </summary>
    [Required]
    [Column("SGS_INCLUIDO_MEMBRESIA")]
    public bool IsIncludedInMembership { get; set; }

    /// <summary>
    /// Estado del servicio
    /// </summary>
    [Required]
    [Column("SGS_ESTADO")]
    public bool IsActive { get; set; }

    /// <summary>
    /// Fecha de inicio de disponibilidad del servicio
    /// </summary>
    [Column("SGS_FECHA_INICIO")]
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// Fecha de fin de disponibilidad del servicio
    /// </summary>
    [Column("SGS_FECHA_FIN")]
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// Observaciones o notas adicionales
    /// </summary>
    [StringLength(500)]
    [Column("SGS_OBSERVACIONES")]
    public string Observations { get; set; }

    /// <summary>
    /// Navegación a la sucursal
    /// </summary>
    [ForeignKey(nameof(GymBranchId))]
    public virtual GymBranch GymBranch { get; set; }
}

