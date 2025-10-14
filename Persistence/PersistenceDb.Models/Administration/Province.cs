using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PersistenceDb.Models.Administration;
/// <summary>
/// Tabla de provincia
/// </summary>
[Table(name: "PROVINCIA", Schema = "ADMINISTRACION")]
public class Province
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("PRO_ID")]
    /// <summary>
    /// Id de Provincia
    /// </summary>
    /// <value></value>
    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    [Column("PRO_CODIGO")]
    /// <summary>
    /// Código Provincia
    /// </summary>
    /// <value></value>
    public string Code { get; set; }

    [Required]
    [StringLength(30)]
    [Column("PRO_NOMBRE")]
    /// <summary>
    /// Nombre Provincia
    /// </summary>
    /// <value></value>
    public string Name { get; set; }

    [Required]
    [Column("REG_ID")]
    [ForeignKey(nameof(Region))]
    /// <summary>
    /// Id de Registro
    /// </summary>
    /// <value></value>
    public int RegionId { get; set; }

    [Required]
    [Column("PRO_ESTADO")]
    /// <summary>
    /// Estado
    /// </summary>
    /// <value></value>
    public bool State { get; set; }

    /// <summary>
    /// Región
    /// </summary>
    /// <value></value>
    public Region Region { get; set; }

    /// <summary>
    /// Puntos de transporte
    /// </summary>
    /// <value></value>
    public ICollection<TransportPoint> TransportPoints { get; set; }

    /// <summary>
    /// Lugares
    /// </summary>
    /// <value></value>
    public ICollection<Place> Places { get; set; }  
}
