namespace PersistenceDb.Models.Enums;

/// <summary>
/// Tipo de interpretación de la diferencia de valor (alineado a LogicApi.Model.Enum.DifferenceValueType)
/// </summary>
public enum PhysicalParameterDifferenceValueType : byte
{
    /// <summary>
    /// Diferencia positiva deseable
    /// </summary>
    Positive = 0,

    /// <summary>
    /// Diferencia negativa deseable
    /// </summary>
    Negative = 1,

    /// <summary>
    /// Sin diferencia relevante
    /// </summary>
    Zero = 2,
}
