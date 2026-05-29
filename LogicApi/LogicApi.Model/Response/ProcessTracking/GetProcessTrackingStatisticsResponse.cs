using LogicApi.Model.Enum;

namespace LogicApi.Model.Response.ProcessTracking;

/// <summary>
/// Respuesta de obtener estadísticas de seguimientos de procesos
/// </summary>
public class GetProcessTrackingStatisticsResponse : IApiBaseResponse
{
    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    public string UserMessage { get; set; }

    /// <summary>
    /// Mostrar Mensaje?
    /// </summary>
    public bool ShowMessage { get; set; }

    /// <summary>
    /// Estadísticas generales
    /// </summary>
    public List<CartesianPoint> StatisticsControl { get; set; }
}

/// <summary>
/// Represents a point in the weight control Cartesian plane
/// </summary>
public class CartesianPoint
{

    /// <summary>
    /// Label of the Cartesian point
    /// </summary>
    /// <value></value>
    public string Label { get; set; }

    /// <summary>
    /// Value of the Cartesian point
    /// </summary>
    /// <value></value>
    public string CurrentValue { get; set; }

    /// <summary>
    /// Value type of the Cartesian point
    /// </summary>
    /// <value></value>
    public string ValueType { get; set; }

    /// <summary>
    /// Icon code of the Cartesian point
    /// </summary>
    /// <value></value>
    public string IconCode { get; set; }

    /// <summary>
    /// Difference value of the Cartesian point
    /// </summary>
    /// <value></value>
    public string DifferenceValue { get; set; }

    /// <summary>
    /// Difference value of the Cartesian point
    /// </summary>
    /// <value></value>
    public DifferenceValueType DifferenceValueType { get; set; }

    /// <summary>
    /// Minimum value of the X axis
    /// </summary>
    /// <value></value>
    public decimal MinXAxisValue { get; set; }
    /// <summary>
    /// Maximum value of the X axis
    /// </summary>
    /// <value></value>
    public decimal MaxXAxisValue { get; set; }
    /// <summary>
    /// Minimum value of the Y axis
    /// </summary>
    /// <value></value>
    public decimal MinYAxisValue { get; set; }
    /// <summary>
    /// Maximum value of the Y axis
    /// </summary>
    /// <value></value>
    public decimal MaxYAxisValue { get; set; }

    /// <summary>
    /// Represents a list of points in the Cartesian plane
    /// </summary>
    /// <value></value>
    public List<CartesianPoin> CartesianPoints { get; set; }
}

/// <summary>
/// Represents a point in the Cartesian plane
/// </summary>
public class CartesianPoin
{
    /// <summary>
    /// The X coordinate (for example: date or time, as ticks or unix timestamp)
    /// </summary>
    public string XLabel { get; set; }

    /// <summary>
    /// The Y coordinate (weight in kilograms)
    /// </summary>
    public string YLabel { get; set; }

    /// <summary>
    /// The value of the Y coordinate
    /// </summary>
    public decimal YValue { get; set; }

    /// <summary>
    /// The value of the X coordinate
    /// </summary>
    public decimal XValue { get; set; }
}
