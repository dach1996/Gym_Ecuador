using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersistenceDb.Models.Authentication;

namespace PersistenceDb.Models.Administration;

/// <summary>
/// Tabla de Comentarios del Foro
/// </summary>
[Table(name: "COMENTARIO_FORO", Schema = "ADMINISTRACION")]
public class ForumComment
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("CMF_ID")]
    /// <summary>
    /// Id del Comentario
    /// </summary>
    /// <value></value>
    public int Id { get; set; }

    [Required]
    [Column("CMF_GUID")]
    /// <summary>
    /// Guid del Comentario
    /// </summary>
    /// <value></value>
    public Guid Guid { get; set; }

    [Required]
    [Column("FOR_ID")]
    [ForeignKey(nameof(Forum))]
    /// <summary>
    /// Id del Foro
    /// </summary>
    /// <value></value>
    public int ForumId { get; set; }

    [Required]
    [Column("CMF_COMENTARIO")]
    /// <summary>
    /// Comentario del Comentario
    /// </summary>
    /// <value></value>
    public string Comment { get; set; }

    [Required]
    [Column("CMF_ESTADO")]
    /// <summary>
    /// Estado del Comentario (Activo/Inactivo)
    /// </summary>
    /// <value></value>
    public bool IsActive { get; set; }

    [Column("USR_ID_REGISTRADOR")]
    [ForeignKey(nameof(User))]
    /// <summary>
    /// Id del Usuario que registró el Comentario
    /// </summary>
    /// <value></value>
    public int UserIdRegister { get; set; }

    [Required]
    [Column("CMF_FECHA_REGISTRO")]
    /// <summary>
    /// Fecha de Registro
    /// </summary>
    /// <value></value>
    public DateTime DateTimeRegister { get; set; }

    /// <summary>
    /// Foro al que pertenece el Comentario
    /// </summary>
    /// <value></value>
    public Forum Forum { get; set; }

    /// <summary>
    /// Usuario Autor del Comentario
    /// </summary>
    /// <value></value>
    public User Author { get; set; }
}

