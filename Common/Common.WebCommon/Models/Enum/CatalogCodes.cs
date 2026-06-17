using System.Runtime.Serialization;

namespace Common.WebCommon.Models.Enum;
/// <summary>
/// Forma de ordenar catálogos
/// </summary>
public enum CatalogCodes
{
    /// <summary>
    /// Código de Género
    /// </summary>
    [EnumMember(Value = "GENERO")]
    Gender = 1,

    /// <summary>
    /// Código de Tipo de Equipamiento Gimnasio
    /// </summary>
    [EnumMember(Value = "TIPOS_EQUIPO_GIMNASIO")]
    EquipmentTypeGym = 2,

    /// <summary>
    /// Código de Tipo de Musculo
    /// </summary>
    [EnumMember(Value = "NOMBRES_MUSCULOS")]
    MuscleType = 3,

    /// <summary>
    /// Código de nivel de actividad física
    /// </summary>
    [EnumMember(Value = "NIVEL_ACTIVIDAD_FISICA")]
    PhysicalActivityLevel = 4,

    /// <summary>
    /// Código de ritmo de progreso
    /// </summary>
    [EnumMember(Value = "RITMO_PROGRESO")]
    ProgressRate = 5,
}