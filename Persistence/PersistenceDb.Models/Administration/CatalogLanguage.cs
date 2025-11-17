using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersistenceDb.Models.Enums;

namespace PersistenceDb.Models.Administration;
/// <summary>
/// Tabla Catálogo Lenguaje
/// </summary>
[Table(name: "CATALOGO_LENGUAJE", Schema = "ADMINISTRACION")]
public class CatalogLanguage
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("CAL_ID")]
    /// <summary>
    /// Identificador de catálogo
    /// </summary>
    /// <value></value>
    public int Id { get; set; }

    [Required]
    [Column("CAL_CODIGO_IDIOMA")]
    [ForeignKey(nameof(Language))]
    public LanguageType Language { get; set; }
    
    [Required]
    [Column("CAT_ID")]
    [ForeignKey(nameof(Catalog))]
    public int CatalogId { get; set; }

    [Required]
    [Column("CAL_NOMBRE")]
    /// <summary>
    /// Nombre de catálogo en el idioma
    /// </summary>
    /// <value></value>
    public string Name { get; set; }

    /// <summary>
    /// Descripción de catálogo
    /// </summary>
    /// <value></value>
    [Column("CAL_DESCRIPCION")]
    public string Description { get; set; }

    /// <summary>
    /// Padre del catálogo
    /// </summary>
    /// <value></value>
    public Catalog Catalog { get; set; }
}
