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
}