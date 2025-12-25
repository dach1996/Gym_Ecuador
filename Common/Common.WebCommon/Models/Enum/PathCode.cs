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
}