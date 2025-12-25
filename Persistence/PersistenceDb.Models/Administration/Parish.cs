using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PersistenceDb.Models.Administration;
/// <summary>
/// Tabla de parroquia
/// </summary>
[Table(name: "PARROQUIA", Schema = "ADMINISTRACION")]
public class Parish
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("PAR_ID")]
    /// <summary>
    /// Id de Parroquia
    /// </summary>
    /// <value></value>
    public int Id { get; set; }

    [Required]
    [StringLength(10)]
    [Column("PAR_CODIGO")]
    /// <summary>
    /// Código Parroquia
    /// </summary>
    /// <value></value>
    public string Code { get; set; }

    [Required]
    [StringLength(100)]
    [Column("PAR_NOMBRE")]
    /// <summary>
    /// Nombre Parroquia
    /// </summary>
    /// <value></value>
    public string Name { get; set; }

    [Required]
    [Column("CIU_ID")]
    [ForeignKey(nameof(City))]
    /// <summary>
    /// Id de Ciudad
    /// </summary>
    /// <value></value>
    public int CityId { get; set; }

    [Required]
    [Column("PAR_ESTADO")]
    /// <summary>
    /// Estado
    /// </summary>
    /// <value></value>
    public bool State { get; set; }
    
    /// <summary>
    /// Ciudad
    /// </summary>
    /// <value></value>
    public City City { get; set; }
}

