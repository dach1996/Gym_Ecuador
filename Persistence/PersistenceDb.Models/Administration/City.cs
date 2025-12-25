using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PersistenceDb.Models.Administration;
/// <summary>
/// Tabla de ciudad
/// </summary>
[Table(name: "CIUDAD", Schema = "ADMINISTRACION")]
public class City
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("CIU_ID")]
    /// <summary>
    /// Id de Ciudad
    /// </summary>
    /// <value></value>
    public int Id { get; set; }

    [Required]
    [StringLength(10)]
    [Column("CIU_CODIGO")]
    /// <summary>
    /// Código Ciudad
    /// </summary>
    /// <value></value>
    public string Code { get; set; }

    [Required]
    [StringLength(100)]
    [Column("CIU_NOMBRE")]
    /// <summary>
    /// Nombre Ciudad
    /// </summary>
    /// <value></value>
    public string Name { get; set; }

    [Required]
    [Column("PRO_ID")]
    [ForeignKey(nameof(Province))]
    /// <summary>
    /// Id de Provincia
    /// </summary>
    /// <value></value>
    public short ProvinceId { get; set; }

    [Required]
    [Column("CIU_ESTADO")]
    /// <summary>
    /// Estado
    /// </summary>
    /// <value></value>
    public bool State { get; set; }
    
    /// <summary>
    /// Provincia
    /// </summary>
    /// <value></value>
    public Province Province { get; set; }

    /// <summary>
    /// Parroquias
    /// </summary>
    /// <value></value>
    public ICollection<Parish> Parishes { get; set; }
}

