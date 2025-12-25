using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PersistenceDb.Models.Administration;
/// <summary>
/// Tabla de tipo de identificación
/// </summary>
[Table(name: "TIPO_IDENTIFICACION", Schema = "ADMINISTRACION")]
public class TypeIdentification
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("TID_ID")]
    /// <summary>
    /// Id de Tipo de Identificación
    /// </summary>
    /// <value></value>
    public byte Id { get; set; }

    [Required]
    [StringLength(10)]
    [Column("TID_CODIGO")]
    /// <summary>
    /// Código Tipo de Identificación
    /// </summary>
    /// <value></value>
    public string Code { get; set; }

    [Required]
    [StringLength(100)]
    [Column("TID_NOMBRE")]
    /// <summary>
    /// Nombre Tipo de Identificación
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
    public byte CountryId { get; set; }

    [Required]
    [Column("TID_ESTADO")]
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
}

