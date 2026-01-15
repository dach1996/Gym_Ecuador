using Common.WebCommon.Models;
using LogicCommon.Model.Response.File;

namespace LogicApi.Model.Response.ProcessTracking;

/// <summary>
/// Respuesta de obtener seguimientos de procesos
/// </summary>
public class GetProcessTrackingsResponse : IPaginatorResponse<ProcessTrackingItem>
{
    /// <summary>
    /// Lista de seguimientos de procesos
    /// </summary>
    public IEnumerable<ProcessTrackingItem> Registers { get; set; }

    /// <summary>
    /// Total de registros
    /// </summary>
    public int TotalRegister { get; set; }

}

/// <summary>
/// Item de seguimiento de proceso
/// </summary>
public class ProcessTrackingItem
{
    /// <summary>
    /// Guid del seguimiento de proceso
    /// </summary>
    public Guid Guid { get; set; }

     /// <summary>
     /// Fecha de registro
     /// </summary>
     public DateTime RegistrationDate { get; set; }

    /// <summary>
    /// Peso corporal actual (en kg o la unidad estándar)
    /// </summary>
    public decimal Weight { get; set; }

    /// <summary>
    /// Porcentaje de grasa corporal medido en el seguimiento.
    /// </summary>
    public decimal? FatPercentage { get; set; }

    /// <summary>
    /// Medida del pecho (en cm)
    /// </summary>
    public decimal? ChestMeasurement { get; set; }

    /// <summary>
    /// Medida de la cintura (en cm)
    /// </summary>
    public decimal? WaistMeasurement { get; set; }

    /// <summary>
    /// Medida del brazo (en cm)
    /// </summary>
    public decimal? ArmRightMeasurement { get; set; }

    /// <summary>
    /// Medida de la pierna (en cm)
    /// </summary>
    public decimal? ThighRightMeasurement { get; set; }

    /// <summary>
    /// Lista de imágenes asociadas al seguimiento de proceso.
    /// </summary>
    public List<FileUrlResponse> Images { get; set; } = new();
}
