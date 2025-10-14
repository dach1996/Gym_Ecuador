using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceDb.Models.Administration;
/// <summary>
/// Tabla Relación tabla de Cooperativa Bus
/// </summary>
[Table(name: "COOPERATIVA_BUS", Schema = "ADMINISTRACION")]
public class CooperativeBus
{
    /// <summary>
    /// Id 
    /// </summary>
    /// <value></value>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("CPB_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Id de Cooperativa
    /// </summary>
    /// <value></value>
    [Required]
    [ForeignKey(nameof(Cooperative))]
    [Column("COP_ID")]
    public int CooperativeId { get; set; }
    
    /// <summary>
    /// Cógido relacionado a la cooperativa y Bus
    /// </summary>
    /// <value></value>
    [Required]
    [Column("CPB_CODIGO")]
    public string Code { get; set; }

    /// <summary>
    /// Estado del bus en relación a la cooperativa
    /// </summary>
    /// <value></value>
    [Required]
    [Column("CPB_ESTADO")]
    public bool State { get; set; }

    /// <summary>
    /// Cooperativa
    /// </summary>
    /// <value></value>
    public Cooperative Cooperative { get; set; }

    /// <summary>
    /// Servicios de Bus de Cooperativa
    /// </summary>
    /// <value></value>
    public ICollection<CooperativeBusService> CooperativeBusServices { get; set; }

    /// <summary>
    /// Servicios de Diagramas de Bus
    /// </summary>
    /// <value></value>
    public ICollection<FloorDiagramBusCooperative> FloorDiagramBusCooperatives { get; set; }

}
