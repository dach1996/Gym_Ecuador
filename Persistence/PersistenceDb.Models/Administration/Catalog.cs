using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceDb.Models.Administration;
/// <summary>
/// Tabla Usuario
/// </summary>
[Table(name: "CATALOGO", Schema = "ADMINISTRACION")]
public class Catalog
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("CAT_ID")]
    /// <summary>
    /// Identificador de catálogo
    /// </summary>
    /// <value></value>
    public int Id { get; set; }

    [Required]
    [Column("CAT_CODIGO")]
    /// <summary>
    /// Código de catálogo
    /// </summary>
    /// <value></value>
    public string Code { get; set; }

    [Required]
    [Column("CAT_NOMBRE")]
    /// <summary>
    /// Nombre de catálogo
    /// </summary>
    /// <value></value>
    public string Name { get; set; }

    /// <summary>
    /// Descripción de catálogo
    /// </summary>
    /// <value></value>
    [Column("CAT_DESCRIPCION")]
    public string Description { get; set; }

    /// <summary>
    /// Código de catálogo padre
    /// </summary>
    /// <value></value>
    [Column("CAT_ID_PADRE")]
    [ForeignKey(nameof(CatalogueFather))]
    public int? ParentId { get; set; }

    [Column("CAT_ESTADO")]
    /// <summary>
    /// Indica si el catálogo esta Activo o Inactivo
    /// </summary>
    /// /// <value></value>
    public bool Status { get; set; }

    /// <summary>
    /// Valor del catálogo
    /// </summary>
    /// <value></value>
    [Column("CAT_VALOR")]
    public string Value { get; set; }

    /// <summary>
    /// Items de Catálogo
    /// </summary>
    /// <value></value>
    public ICollection<Catalog> Catalogues { get; set; }

    /// <summary>
    /// Padre del catálogo
    /// </summary>
    /// <value></value>
    public Catalog CatalogueFather { get; set; }
}
