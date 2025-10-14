using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PersistenceDb.Models.Administration;
/// <summary>
/// Tabla de región
/// </summary>
[Table(name: "REGION", Schema = "ADMINISTRACION")]
public class Region
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("REG_ID")]
    /// <summary>
    /// Id de Región
    /// </summary>
    /// <value></value>
    public int Id { get; set; }

    [Required]
    [StringLength(20)]
    [Column("REG_CODIGO")]
    /// <summary>
    /// Código Región
    /// </summary>
    /// <value></value>
    public string Code { get; set; }

    [Required]
    [StringLength(30)]
    [Column("REG_NOMBRE")]
    /// <summary>
    /// Nombre Región
    /// </summary>
    /// <value></value>
    public string Name { get; set; }

    [Required]
    [Column("PAI_ID")]
    [ForeignKey(nameof(Country))]
    /// <summary>
    /// Id de País
    /// </summary>
    /// <value></value>
    public int CountryId { get; set; }

    [Required]
    [Column("REG_ESTADO")]
    /// <summary>
    /// Estado
    /// </summary>
    /// <value></value>
    public bool State { get; set; }
    
    /// <summary>
    /// País
    /// </summary>
    /// <value></value>
    public Country Country { get; set; }

    /// <summary>
    /// Provincias
    /// </summary>
    /// <value></value>
    public ICollection<Province> Provinces { get; set; }
}
