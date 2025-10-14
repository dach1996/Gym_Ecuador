using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersistenceDb.Models.Enums;

namespace PersistenceDb.Models.Administration;

/// <summary>
/// Tabla Usuario
/// </summary>
[Table(name: "PARAMETRO", Schema = "ADMINISTRACION")]
public class Parameter
{
    /// <summary>
    /// Identificador del parámetro del sistema
    /// </summary>
    /// <value></value>
    [Column("PAR_ID")]
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    /// <summary>
    /// Código del parámetro del sistema
    /// </summary>
    /// <value></value>
    [Column("PAR_CODIGO")]
    [Required]
    [StringLength(100)]
    public string Code { get; set; }

    /// <summary>
    /// Nombre del parámetro del sistema
    /// </summary>
    /// <value></value>
    [Column("PAR_NOMBRE")]
    [Required]
    [StringLength(200)]
    public string Name { get; set; }

    /// <summary>
    /// Valor del parámetro del sistema
    /// </summary>
    /// <value></value>
    [Column("PAR_VALOR")]
    [Required]
    [StringLength(500)]
    public string Value { get; set; }

    /// <summary>
    /// Descripción del parámetro del sistema
    /// </summary>
    /// <value></value>
    [Column("PAR_DESCRIPCION")]
    [Required]
    [StringLength(200)]
    public string Description { get; set; }

    /// <summary>
    /// Indica si esta Activo o Inactivo
    /// </summary>
    /// <value></value>
    [Column("PAR_ESTADO")]
    [Required]
    public bool Status { get; set; }

    /// <summary>
    /// Versión del parámetro del sistema
    /// </summary>
    /// <value></value>
    [Column("PAR_VERSION")]
    [Required]
    public int Version { get; set; }

    /// <summary>
    /// Nombre de item de catálogo
    /// </summary>
    /// <value></value>
    [Column("PAR_FECHA_REGISTRO")]
    [Required]
    public DateTime RegistrationDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Versión del parámetro del sistema
    /// </summary>
    /// <value></value>
    [Column("PAR_FECHA_MOFICACION")]
    public DateTime? ModificationDate { get; set; }

}
