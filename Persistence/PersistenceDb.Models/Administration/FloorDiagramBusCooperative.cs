using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PersistenceDb.Models.Administration;
/// <summary>
/// Tabla Diagrama de Pisos
/// </summary>
[Table(name: "PISO_DIAGRAMA_COOPERATIVA_BUS", Schema = "ADMINISTRACION")]
public class FloorDiagramBusCooperative
{

    /// <summary>
    /// Identificador de Archivos
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("PDB_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Identificador de Tabla
    /// </summary>
    /// <value></value>
    [Column("PDB_GUID")]
    public Guid Guid { get; set; }

    /// <summary>
    /// Identificador de Tabla
    /// </summary>
    /// <value></value>
    [Column("CPB_ID")]
    [ForeignKey(nameof(Administration.CooperativeBus))]
    public int CooperativeBusId { get; set; }

    /// <summary>
    ///  Número de Piso
    /// </summary>
    /// <value></value>
    [Column("PDB_NUMERO_PISO")]
    public int FloorNumber { get; set; }

    /// <summary>
    ///  Diagrama de Piso
    /// </summary>
    /// <value></value>
    [Column("PDB_DIAGRAMA")]
    public string Diagram { get; set; }

    /// <summary>
    ///  Diagrama de Piso
    /// </summary>
    /// <value></value>
    [Column("PDB_IDENTIFICADOR_PISO")]
    public string FloorCooperativeIdentifier { get; set; }

    /// <summary>
    /// Servicios de Bus
    /// </summary>
    /// <value></value>
    public CooperativeBus CooperativeBus { get; set; }
}
