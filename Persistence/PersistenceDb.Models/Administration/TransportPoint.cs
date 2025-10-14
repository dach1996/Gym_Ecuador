using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Geometries;

namespace PersistenceDb.Models.Administration;
/// <summary>
/// Transport Point Table
/// </summary>
[Table(name: "PUNTO_TRANSPORTE", Schema = "ADMINISTRACION")]
public class TransportPoint
{
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("PTT_ID")]
    /// <summary>
    /// Transport point identifier
    /// </summary>
    /// <value></value>
    public int Id { get; set; }

    [Required]
    [StringLength(50)]
    [Column("PTT_CODIGO")]
    /// <summary>
    /// Transport point code
    /// </summary>
    /// <value></value>
    public string Code { get; set; }

    [Required]
    [StringLength(50)]
    [Column("PTT_NOMBRE")]
    /// <summary>
    /// Transport point name
    /// </summary>
    /// <value></value>
    public string Name { get; set; }

    [Column("PTT_ESTADO")]
    /// <summary>
    /// Indicates if the transport point is Active or Inactive
    /// </summary>
    /// <value></value>
    public bool Status { get; set; }

    [Column("PTT_TIPO")]
    /// <summary>
    /// Type of transport point
    /// </summary>
    /// <value></value>
    public byte Type { get; set; }

    [Column("PRO_ID")]
    /// <summary>
    /// Province identifier
    /// </summary>
    /// <value></value>
    [ForeignKey(nameof(Province))]
    public int ProvinceId { get; set; }

    [Column("PTT_UBICACION_LATITUD")]
    /// <summary>
    /// Latitude of location
    /// </summary>
    /// <value></value>
    public double LocationLatitude { get; set; }

    [Column("PTT_UBICACION_LONGITUD")]
    /// <summary>
    /// Longitude of location
    /// </summary>
    /// <value></value>
    public double LocationLongitude { get; set; }

    /// <summary>
    /// Geographical Location
    /// </summary>
    [Column("EES_UBICACION_CALCULADA")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    [Required]
    public Point Location { get; set; }

    /// <summary>
    /// Provincia
    /// </summary>
        /// <value></value>
    public Province Province { get; set; }

    /// <summary>
    /// Cooperativas
    /// </summary>
    /// <value></value>
    public ICollection<CooperativeTransportPoint> CooperativeTransportPoints { get; set; }
}
