using System.Runtime.Serialization;

namespace Common.WebCommon.Models.Enum;
/// <summary>
/// Rutas de almacenamiento
/// </summary>
public enum PathCode
{
    /// <summary>
    /// Ruta de imágenes de usuarios
    /// </summary>
    [EnumMember(Value = "USER_IMAGE")]
    UserImage = 1,

    /// <summary>
    /// Ruta de imágenes de sucursales de gimnasio
    /// </summary>
    [EnumMember(Value = "GYM_BRANCH_IMAGE")]
    GymBranchImage = 2,

    /// <summary>
    /// Ruta de imágenes de procesos de seguimiento
    /// </summary>
    [EnumMember(Value = "PROCESS_TRACKING_IMAGE")]
    ProcessTrackingImage = 3,

    /// <summary>
    /// Ruta de imágenes de equipamientos
    /// </summary>
    [EnumMember(Value = "EQUIPMENT_IMAGE")]
    EquipmentImage = 4,
}