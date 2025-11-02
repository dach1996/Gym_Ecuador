using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersistenceDb.Models.Core;

/// <summary>
/// Tabla Horarios de Atención de Sucursal de Gimnasio
/// </summary>
[Table(name: "GYM_SUCURSAL_HORARIO", Schema = "CORE")]
public class GymBranchSchedule
{
    /// <summary>
    /// Id
    /// </summary>
    [Key]
    [Required]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("GSH_ID")]
    public int Id { get; set; }

    /// <summary>
    /// Id de la sucursal
    /// </summary>
    [Required]
    [Column("SGY_ID")]
    public int GymBranchId { get; set; }

    /// <summary>
    /// Id del día de la semana (Item de Catálogo)
    /// 1=Lunes, 2=Martes, 3=Miércoles, 4=Jueves, 5=Viernes, 6=Sábado, 7=Domingo
    /// </summary>
    [Required]
    [Column("ITC_DIA_SEMANA")]
    public int DayOfWeekCatalogId { get; set; }

    /// <summary>
    /// Hora de apertura
    /// </summary>
    [Required]
    [Column("GSH_HORA_APERTURA")]
    public TimeSpan OpeningTime { get; set; }

    /// <summary>
    /// Hora de cierre
    /// </summary>
    [Required]
    [Column("GSH_HORA_CIERRE")]
    public TimeSpan ClosingTime { get; set; }

    /// <summary>
    /// Indica si se debe visualizar/mostrar este horario
    /// </summary>
    [Required]
    [Column("GSH_VISUALIZAR")]
    public bool IsVisible { get; set; }

    /// <summary>
    /// Navegación a la sucursal
    /// </summary>
    [ForeignKey(nameof(GymBranchId))]
    public virtual GymBranch GymBranch { get; set; }
}

