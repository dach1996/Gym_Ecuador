using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla de Servicios
/// </summary>
[Table(name: "SERVICIO", Schema = "CORE")]
public class Service
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("SER_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Nombre del servicio
    /// </summary>
    [Required]
    [StringLength(200)]
    [Column("SER_NOMBRE")]
    public string Name { get; set; }

    /// <summary>
    /// Descripción del servicio
    /// </summary>
    [StringLength(1000)]
    [Column("SER_DESCRIPCION")]
    public string Description { get; set; }

    /// <summary>
    /// Indica si el servicio requiere reserva previa
    /// </summary>
    [Required]
    [Column("SER_REQUIERE_RESERVA")]
    public bool RequiresReservation { get; set; }

    /// <summary>
    /// Estado del servicio
    /// </summary>
    [Required]
    [Column("SER_ESTADO")]
    public bool IsActive { get; set; }

}

