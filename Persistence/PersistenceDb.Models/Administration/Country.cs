using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PersistenceDb.Models.Administration;
/// <summary>
/// Tabla de país
/// </summary>
[Table(name: "PAIS", Schema = "ADMINISTRACION")]
public class Country
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("PAI_ID")]
    /// <summary>
    /// Id de País
    /// </summary>
    /// <value></value>
    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    [Column("PAI_CODIGO")]
    /// <summary>
    /// Código País
    /// </summary>
    /// <value></value>
    public string Code { get; set; }

    [Required]
    [StringLength(30)]
    [Column("PAI_NOMBRE")]
    /// <summary>
    /// Nombre País
    /// </summary>
    /// <value></value>
    public string Name { get; set; }

    [Required]
    [Column("PAI_ESTADO")]
    /// <summary>
    /// Estado
    /// </summary>
    /// <value></value>
    public bool State { get; set; }

    /// <summary>
    /// Regiones
    /// </summary>
    /// <value></value>
    public ICollection<Region> Regions { get; set; }
}
