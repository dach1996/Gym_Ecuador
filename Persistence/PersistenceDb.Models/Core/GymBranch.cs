using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla Sucursal de Gimnasio
/// </summary>
[Table(name: "SUCURSAL_GIMNASIO", Schema = "CORE")]
public class GymBranch
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("SGY_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Guid
    /// </summary>
    [Required]
    [Column("SGY_GUID")]
    public Guid Guid { get; set; }

    /// <summary>
    /// Id del gimnasio principal
    /// </summary>
    [Required]
    [Column("GYM_ID")]
    public int GymId { get; set; }

    /// <summary>
    /// Fecha de registro
    /// </summary>
    [Column("SGY_FECHA_REGISTRO")]
    public DateTime DateTimeRegister { get; set; }

    /// <summary>
    /// Nombre de la sucursal
    /// </summary>
    [Required]
    [StringLength(200)]
    [Column("SGY_NOMBRE")]
    public string Name { get; set; }

    /// <summary>
    /// Código de la sucursal
    /// </summary>
    [StringLength(50)]
    [Required]
    [Column("SGY_CODIGO")]
    public string Code { get; set; }

    /// <summary>
    /// Descripción de la sucursal
    /// </summary>
    [StringLength(1000)]
    [Column("SGY_DESCRIPCION")]
    public string Description { get; set; }

    /// <summary>
    /// Dirección de la sucursal
    /// </summary>
    [Required]
    [StringLength(500)]
    [Column("SGY_DIRECCION")]
    public string Address { get; set; }

    /// <summary>
    /// Teléfono de la sucursal
    /// </summary>
    [StringLength(50)]
    [Column("SGY_TELEFONO")]
    public string Phone { get; set; }

    /// <summary>
    /// Email de la sucursal
    /// </summary>
    [StringLength(200)]
    [Column("SGY_EMAIL")]
    public string Email { get; set; }

    /// <summary>
    /// Latitud para localización
    /// </summary>
    [Column("SGY_LATITUD")]
    public double Latitude { get; set; }

    /// <summary>
    /// Longitud para localización
    /// </summary>
    [Column("SGY_LONGITUD")]
    public double Longitude { get; set; }

    /// <summary>
    /// Capacidad máxima de personas
    /// </summary>
    [Column("SGY_CAPACIDAD_MAXIMA")]
    public int MaxCapacity { get; set; }


    /// <summary>
    /// Número de pisos/plantas
    /// </summary>
    [Column("SGY_NUMERO_PISOS")]
    public byte FloorCount { get; set; }

    /// <summary>
    /// Estado de la sucursal
    /// </summary>
    [Required]
    [Column("SGY_ESTADO")]
    public bool IsActive { get; set; }

    /// <summary>
    /// Fecha de apertura de la sucursal
    /// </summary>
    [Column("SGY_FECHA_APERTURA")]
    public DateTime OpeningDate { get; set; }


    /// <summary>
    /// Geographical Location
    /// </summary>
    [Column("SGY_UBICACION_CALCULADA")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [Required]
    public Point Location { get; set; }

    /// <summary>
    /// Navegación al gimnasio principal
    /// </summary>
    [ForeignKey(nameof(GymId))]
    public virtual Gym Gym { get; set; }

    /// <summary>
    /// Navegación a los horarios de atención de la sucursal
    /// </summary>
    public ICollection<GymBranchSchedule> GymBranchSchedules { get; set; }

    /// <summary>
    /// Navegación a las imágenes de la sucursal
    /// </summary>
    public ICollection<GymBranchImage> GymBranchImages { get; set; }

    /// <summary>
    /// Navegación a los clientes de la sucursal
    /// </summary>
    public ICollection<ClientGymBranch> ClientGymBranches { get; set; }
}

