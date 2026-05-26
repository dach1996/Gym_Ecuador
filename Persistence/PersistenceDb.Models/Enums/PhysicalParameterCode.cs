using System.Runtime.Serialization;

namespace PersistenceDb.Models.Enums;

/// <summary>
/// Códigos de parámetros físicos alineados con CORE.PARAMETROS_FISICOS (PAF_ID / PAF_CODIGO)
/// </summary>
public enum PhysicalParameterCode : byte
{
    /// <summary>
    /// Peso
    /// </summary>
    [EnumMember(Value = "PESO")]
    Weight = 1,

    /// <summary>
    /// Altura
    /// </summary>
    [EnumMember(Value = "ALTURA")]
    Height = 2,

    /// <summary>
    /// Grasa porcentual
    /// </summary>
    [EnumMember(Value = "GRASA_PORCENTAJE")]
    BodyFatPercentage = 3,

    /// <summary>
    /// Musculo porcentual
    /// </summary>
    [EnumMember(Value = "MUSCULO_PORCENTAJE")]
    MuscleMassPercentage = 4,

    /// <summary>
    /// Medida de pecho
    /// </summary>
    [EnumMember(Value = "MEDIDA_PECHO")]
    ChestMeasurement = 5,

    /// <summary>
    /// Medida de cintura
    /// </summary>
    [EnumMember(Value = "MEDIDA_CINTURA")]
    WaistMeasurement = 6,

    /// <summary>
    /// Medida de cadera
    /// </summary>
    [EnumMember(Value = "MEDIDA_CADERA")]
    HipMeasurement = 7,

    /// <summary>
    /// Medida de brazo derecho
    /// </summary>
    [EnumMember(Value = "MEDIDA_BRAZO_DER")]
    ArmRightMeasurement = 8,

    /// <summary>
    /// Medida de muslo derecho
    /// </summary>
    [EnumMember(Value = "MEDIDA_MUSLO_DER")]
    ThighRightMeasurement = 9,

    /// <summary>
    /// IMC (calculado; catálogo para etiqueta UI)
    /// </summary>
    [EnumMember(Value = "IMC")]
    Bmi = 10,
}
