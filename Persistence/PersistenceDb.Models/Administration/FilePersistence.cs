using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PersistenceDb.Models.Administration;
/// <summary>
/// Tabla de Archivos
/// </summary>
[Table(name: "ARCHIVOS_GUARDADOS", Schema = "ADMINISTRACION")]
public class FilePersistence
{
    /// <summary>
    /// Identificador de Archivos
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ARG_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Fecha de Registro
    /// </summary>
    [Column("ARG_FECHA_REGISTRO")]
    [Required]
    public DateTime DateRegister { get; set; }

    /// <summary>
    /// Nombre de archivo
    /// </summary>
    [StringLength(50)]
    [Required]
    [Column("ARG_NOMBRE")]
    public string Name { get; set; }

    /// <summary>
    /// Ruta
    /// </summary>
    [StringLength(300)]
    [Required]
    [Column("ARG_RUTA")]
    public string Path { get; set; }

    /// <summary>
    /// State
    /// </summary>
    [Required]
    [Column("ARG_ESTADO")]
    public bool State { get; set; }

    /// <summary>
    /// Id de la Ruta Base del Archivo
    /// </summary>
    /// <value></value>
    [Column("FBP_ID")]
    [ForeignKey(nameof(FileBasePath))]
    public byte FileBasePathId { get; set; }

    public FileBasePath FileBasePath { get; set; }
}