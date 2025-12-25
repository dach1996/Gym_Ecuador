using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceDb.Models.Administration;

/// <summary>
/// Tabla de Artículos
/// </summary>
[Table(name: "ARTICULO", Schema = "ADMINISTRACION")]
public class Article
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("ART_ID")]
    /// <summary>
    /// Id del Artículo
    /// </summary>
    /// <value></value>
    public int Id { get; set; }

    [Required]
    [Column("ART_GUID")]
    /// <summary>
    /// Guid del Artículo
    /// </summary>
    /// <value></value>
    public Guid Guid { get; set; }

    [Required]
    [StringLength(200)]
    [Column("ART_TITULO")]
    /// <summary>
    /// Título del Artículo
    /// </summary>
    /// <value></value>
    public string Title { get; set; }

    [Required]
    [Column("ART_CONTENIDO")]
    /// <summary>
    /// Contenido del Artículo
    /// </summary>
    /// <value></value>
    public string Content { get; set; }

    [StringLength(500)]
    [Column("ART_RESUMEN")]
    /// <summary>
    /// Resumen del Artículo
    /// </summary>
    /// <value></value>
    public string Summary { get; set; }

    [StringLength(200)]
    [Column("ART_IMAGEN_URL")]
    /// <summary>
    /// URL de la imagen del Artículo
    /// </summary>
    /// <value></value>
    public string ImageUrl { get; set; }

    [Required]
    [Column("ART_FECHA_PUBLICACION")]
    /// <summary>
    /// Fecha de Publicación
    /// </summary>
    /// <value></value>
    public DateTime PublicationDate { get; set; }

    [Required]
    [Column("ART_FECHA_MAXIMA")]
    /// <summary>
    /// Fecha Máxima de Publicación
    /// </summary>
    /// <value></value>
    public DateTime MaximumPublicationDate { get; set; }

    [Required]
    [Column("ART_FECHA_CREACION")]
    /// <summary>
    /// Fecha de Creación
    /// </summary>
    /// <value></value>
    public DateTime CreationDate { get; set; }

    [Required]
    [Column("ART_ESTADO")]
    /// <summary>
    /// Estado del Artículo (Activo/Inactivo)
    /// </summary>
    /// <value></value>
    public bool IsActive { get; set; }
}

