using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersistenceDb.Models.Authentication;

namespace PersistenceDb.Models.Administration;

/// <summary>
/// Tabla de Foros
/// </summary>
[Table(name: "FORO", Schema = "ADMINISTRACION")]
public class Forum
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("FOR_ID")]
    /// <summary>
    /// Id del Foro
    /// </summary>
    /// <value></value>
    public int Id { get; set; }

    [Required]
    [Column("FOR_GUID")]
    /// <summary>
    /// Guid del Foro
    /// </summary>
    /// <value></value>
    public Guid Guid { get; set; }

    [Required]
    [StringLength(200)]
    [Column("FOR_TITULO")]
    /// <summary>
    /// Título del Foro
    /// </summary>
    /// <value></value>
    public string Title { get; set; }

    [Required]
    [Column("FOR_CONTENIDO")]
    /// <summary>
    /// Contenido del Foro
    /// </summary>
    /// <value></value>
    public string Content { get; set; }

    [Required]
    [Column("FOR_FECHA_CREACION")]
    /// <summary>
    /// Fecha de Creación
    /// </summary>
    /// <value></value>
    public DateTime CreationDate { get; set; }

    [Required]
    [Column("FOR_ESTADO")]
    /// <summary>
    /// Estado del Foro (Activo/Inactivo)
    /// </summary>
    /// <value></value>
    public bool IsActive { get; set; }

    [Column("USR_ID_REGISTRADOR")]
    [ForeignKey(nameof(Authentication.User))]
    /// <summary>
    /// Id del Usuario que registró el Foro
    /// </summary>
    /// <value></value>
    public int UserIdRegister { get; set; }

    /// <summary>
    /// Usuario Creador
    /// </summary>
    /// <value></value>
    public User User { get; set; }

    /// <summary>
    /// Comentarios del Foro
    /// </summary>
    /// <value></value>
    public ICollection<ForumComment> ForumComments { get; set; }
}

