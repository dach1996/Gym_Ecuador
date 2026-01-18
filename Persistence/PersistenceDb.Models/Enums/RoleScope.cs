using System.Runtime.Serialization;

namespace PersistenceDb.Models.Enums;
/// <summary>
/// Alcance del rol
/// </summary>
public enum RoleScope : byte
{
    /// <summary>
    /// Global
    /// </summary>
    [EnumMember(Value = "SUPER_ADMIN")]
    Global = 1,

    /// <summary>
    /// Gym
    /// </summary>
    /// <value></value>
    [EnumMember(Value = "ADMINISTRADOR_GYM")]
    Gym = 2,

    /// <summary>
    /// Gym Branch
    /// </summary>
    [EnumMember(Value = "ADMINISTRADOR_SUCURSAL")]
    GymBranch = 3,

    /// <summary>
    /// Client
    /// </summary>
    [EnumMember(Value = "CLIENTE")]
    Client = 4,
}