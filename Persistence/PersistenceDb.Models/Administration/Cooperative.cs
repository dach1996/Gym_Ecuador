using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceDb.Models.Administration;
/// <summary>
/// Tabla de cooperativa
/// </summary>
[Table(name: "COOPERATIVA", Schema = "ADMINISTRACION")]
public class Cooperative
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("COP_ID")]
    /// <summary>
    /// Id de Comperativa
    /// </summary>
    /// <value></value>
    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    [Column("COP_CODIGO")]
    /// <summary>
    /// Código Coperativa
    /// </summary>
    /// <value></value>
    public string Code { get; set; }

    /// <summary>
    /// Fecha de Registro
    /// </summary>
    /// <value></value>
    [Column("COP_FECHA_REGISTRO")]
    public DateTime RegisterDate { get; set; }

    /// <summary>
    /// Fecha de última actualización
    /// </summary>
    /// <value></value>
    [Column("COP_FECHA_ULTIMA_ACTUALIZACION")]
    public DateTime LastUpdateDate { get; set; }

    [Required]
    [StringLength(30)]
    [Column("COP_NOMBRE")]
    /// <summary>
    /// Nombre Coperativa
    /// </summary>
    /// <value></value>
    public string Name { get; set; }

    /// <summary>
    /// Descripción de la cooperativa
    /// </summary>
    /// <value></value>
    [StringLength(250)]
    [Column("COP_DESCRIPCION")]
    public string Descripción { get; set; }

    [Column("COP_LOGO_IMAGEN_ID")]
    [ForeignKey(nameof(LogoImage))]
    /// <summary>
    /// Id de logo de la imagen
    /// </summary>
    /// <value></value>
    public int? LogoImageId { get; set; }

    [Column("COP_BUS_IMAGEN_ID")]
    [ForeignKey(nameof(BusImage))]
    /// <summary>
    /// Id de Imagen del Bus
    /// </summary>
    /// <value></value>
    public int? BusImageId { get; set; }

    [Required]
    [Column("COP_ESTADO")]
    /// <summary>
    /// Name
    /// </summary>
    /// <value></value>
    public bool State { get; set; }

    /// <summary>
    /// Archivo
    /// </summary>
    public FilePersistence LogoImage { get; set; }

    /// <summary>
    /// Archivo
    /// </summary>
    public FilePersistence BusImage { get; set; }

    /// <summary>
    /// Lista de cooperativa con Lugares
    /// </summary>
    /// <value></value>
    public ICollection<CooperativeTransportPoint> CooperativeTransportPoints { get; set; }

    /// <summary>
    /// Lista de Buses
    /// </summary>
    /// <value></value>
    public ICollection<CooperativeBus> CooperativeBuses { get; set; }

}
