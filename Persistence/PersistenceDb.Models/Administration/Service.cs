using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceDb.Models.Administration;
/// <summary>
/// Tabla Servicios
/// </summary>
[Table(name: "SERVICIOS", Schema = "ADMINISTRACION")]
public class Service
{
    /// <summary>
    /// Identificador de catálogo
    /// </summary>
    /// <value></value>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("SRV_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Fecha de Registro
    /// </summary>
    /// <value></value>
    [Column("SRV_FECHA_REGISTRO")]
    public DateTime RegisterDate { get; set; }

    /// <summary>
    /// Fecha de última actualización
    /// </summary>
    /// <value></value>
    [Column("SRV_FECHA_ULTIMA_ACTUALIZACION")]
    public DateTime LastUpdateDate { get; set; }

    /// <summary>
    /// Código
    /// </summary>
    /// <value></value>
    [Column("SRV_CODIGO")]
    [StringLength(50)]
    [Required]
    public string Code { get; set; }

    /// <summary>
    /// Nombre
    /// </summary>
    /// <value></value>
    [Column("SRV_NOMBRE")]
    [StringLength(50)]
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Id de Imagen
    /// </summary>
    /// <value></value>
    [ForeignKey(nameof(Image))]
    [Column("IMAGEN_ID")]
    [Required]
    public int ImageId { get; set; }

    /// <summary>
    /// Relación con Tabla de Imágenes
    /// </summary>
    /// <value></value>
    public FilePersistence Image { get; set; }

    /// <summary>
    /// Servicios de Bus por Cooperativa
    /// </summary>
    /// <value></value>
    public ICollection<CooperativeBusService> CooperativeBusServices { get; set; }
}
