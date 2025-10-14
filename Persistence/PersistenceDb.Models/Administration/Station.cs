using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceDb.Models.Administration;
/// <summary>
/// Tabla Estación
/// </summary>
[Table(name: "ESTACION", Schema = "ADMINISTRACION")]
public class Station
{
    /// <summary>
    /// Identificador de catálogo
    /// </summary>
    /// <value></value>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ESC_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Fecha de Registro
    /// </summary>
    /// <value></value>
    [Column("ESC_FECHA_REGISTRO")]
    public DateTime RegisterDate { get; set; }

    /// <summary>
    /// Código
    /// </summary>
    /// <value></value>
    [Column("ESC_CODIGO")]
    [StringLength(50)]
    [Required]
    public string Code { get; set; }

    /// <summary>
    /// Nombre
    /// </summary>
    /// <value></value>
    [Column("ESC_NOMBRE")]
    [StringLength(50)]
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Id de Imagen
    /// </summary>
    /// <value></value>
    [Column("ESC_ESTADO")]
    [Required]
    public bool State { get; set; }
}
