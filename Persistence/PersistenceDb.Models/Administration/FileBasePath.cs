using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceDb.Models.Administration;

/// <summary>
/// Tabla de Rutas Base de Archivos
/// </summary>
[Table(name: "RUTA_BASE_ARCHIVO", Schema = "ADMINISTRACION")]
public class FileBasePath
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("FBP_ID")]
    /// <summary>
    /// Id de la Ruta Base
    /// </summary>
    /// <value></value>
    public byte Id { get; set; }

    [Required]
    [StringLength(50)]
    [Column("FBP_CODIGO")]
    /// <summary>
    /// Código de la Ruta Base
    /// </summary>
    /// <value></value>
    public string Code { get; set; }

    [Required]
    [StringLength(500)]
    [Column("FBP_BASE_URL")]
    /// <summary>
    /// URL Base
    /// </summary>
    /// <value></value>
    public string BaseUrl { get; set; }

    [Required]
    [StringLength(500)]
    [Column("FBP_RUTA_DIRECTORIO_ARCHIVO")]
    /// <summary>
    /// Ruta del Directorio de Archivos
    /// </summary>
    /// <value></value>
    public string FileDirectoryPath { get; set; }

    [Required]
    [StringLength(16)]
    [Column("FBP_IMPLEMENTACION")]
    /// <summary>
    /// Implementación
    /// </summary>
    /// <value></value>
    public string Implementation { get; set; }
}

