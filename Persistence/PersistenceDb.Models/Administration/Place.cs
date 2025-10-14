using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;

namespace PersistenceDb.Models.Administration;
/// <summary>
/// Tabla Usuario
/// </summary>
[Table(name: "LUGAR", Schema = "ADMINISTRACION")]
public class Place
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("LOD_ID")]
    /// <summary>
    /// Identificador de catálogo
    /// </summary>
    /// <value></value>
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    [Column("LOD_CODIGO_LUGAR")]
    /// <summary>
    /// Código de Lugar
    /// </summary>
    /// <value></value>
    public string Code { get; set; }

    [Required]
    [StringLength(20)]
    [Column("LOD_NOMBRE")]
    /// <summary>
    /// Name
    /// </summary>
    /// <value></value>
    public string Name { get; set; }

    [Required]
    [StringLength(20)]
    [Column("LOD_NOMBRE_CORTO")]
    /// <summary>
    /// Name
    /// </summary>
    /// <value></value>
    public string ShortName { get; set; }

    [Required]
    [Column("LOD_ESTADO")]
    /// <summary>
    /// Estado
    /// </summary>
    /// <value></value>
    public bool State { get; set; }


    /// <summary>
    /// Latitud de ubicación
    /// </summary>
    /// <value></value>
    [Column("LOD_UBICACION_LATITUD")]
    public double LocationLatitude { get; set; }

    /// <summary>
    /// Longitud de ubicación
    /// </summary>
    /// <value></value>
    [Column("LOD_UBICACION_LONGITUD")]
    public double LocationLongitude { get; set; }
    /// <summary>
    /// Ubicación calculada
    /// </summary>
    /// <value></value>
    [Column("LOD_UBICACION_CALCULADA")]
    public Point LocationCalculated { get; set; }

    [Column("PRO_ID")]
    /// <summary>
    /// Id de Provincia
    /// </summary>
    /// <value></value>
    public int ProvinceId { get; set; }

    /// <summary>
    /// Provincia
    /// </summary>
    /// <value></value>
    public Province Province { get; set; }

    /// <summary>
    /// Lista de Lugares Favoritos
    /// </summary>
    public ICollection<PlaceUser> PlaceUsers { get; set; }
}
