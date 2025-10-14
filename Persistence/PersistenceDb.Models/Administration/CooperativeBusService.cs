using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace PersistenceDb.Models.Administration;
/// <summary>
/// Tabla Bus Servicio
/// </summary>
[Table(name: "COOPERATIVA_BUS_SERVICIO", Schema = "ADMINISTRACION")]
[PrimaryKey(nameof(CooperativeBusId), nameof(ServiceId))]
public class CooperativeBusService
{
    [Required]
    [Column("CPB_ID")]
    [ForeignKey(nameof(CooperativeBus))]
    /// <summary>
    /// Id de Cooperativa
    /// </summary>
    /// <value></value>
    public int CooperativeBusId { get; set; }

    /// <summary>
    /// Id de Servicio
    /// </summary>
    /// <value></value>
    [Required]
    [ForeignKey(nameof(Service))]
    [Column("SRV_ID")]
    public int ServiceId { get; set; }

    /// <summary>
    /// Código de Servicio relacionado al bus de la cooperativa
    /// </summary>
    /// <value></value>
    [Column("CBS_CODIGO")]
    public string Code { get; set; }

    /// <summary>
    /// Estado  Servicio relacionado al bus de la cooperativa
    /// </summary>
    /// <value></value>
    [Column("CPB_ESTADO")]
    public bool State { get; set; }

    /// <summary>
    /// Cooperativa Bus
    /// </summary>
    /// <value></value>
    public CooperativeBus CooperativeBus { get; set; }

    /// <summary>
    /// Service
    /// </summary>
    /// <value></value>
    public Service Service { get; set; }
}
