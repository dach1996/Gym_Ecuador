using Common.Utils.Extensions;
using LogicApi.Model.Enum;

namespace LogicApi.Model.Common;
/// <summary>
/// Modelo de datos para la biometría
/// </summary>
public class StatisticComparisonModel
{
    /// <summary>
    /// Label
    /// </summary>
    public string Label { get; set; }

    /// <summary>
    /// Code
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// Valor actual
    /// </summary>
    public decimal? Value { get; set; }

    /// <summary>
    /// Valor anterior 
    /// </summary>
    public decimal? PreviousValue { get; set; }

    /// <summary>
    /// Valor de comparación
    /// </summary>
    public decimal? ComparisonValue => Value.CalculatePercentageDifference(PreviousValue);

    /// <summary>
    /// Tipo de diferencia
    /// </summary>
    public DifferenceValueType DifferenceValueType { get; set; }

}