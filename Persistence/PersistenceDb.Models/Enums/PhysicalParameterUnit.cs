using System.Runtime.Serialization;

namespace PersistenceDb.Models.Enums;

/// <summary>
/// Unidad de medida del parámetro físico
/// </summary>
public enum PhysicalParameterUnit : byte
{
    /// <summary>
    /// Centímetro
    /// </summary>
    [EnumMember(Value = "Cm")]
    Centimeter = 1,

    /// <summary>
    /// Kilogramo
    /// </summary>
    [EnumMember(Value = "Kg")]
    Kilogram = 2,

    /// <summary>
    /// Punto (porcentaje u otra escala puntual)
    /// </summary>
    [EnumMember(Value = "%")]
    Point = 3,
}
