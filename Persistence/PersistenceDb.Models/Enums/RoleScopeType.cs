using System.Runtime.Serialization;

namespace PersistenceDb.Models.Enums;

/// <summary>
/// Ambito de Rol
/// </summary>
public enum RoleScopeType : byte
{
    /// <summary>
    /// Ambito de Rol Global
    /// </summary>
    [EnumMember(Value = "GLOBAL")]
    Global = 1,

    /// <summary>
    /// Negocio
    /// </summary>
    [EnumMember(Value = "GYM")]
    Gym = 2,

    /// <summary>
    /// Sucursal
    /// </summary>
    [EnumMember(Value = "SUCURSAL")]
    Branch = 3,
}