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
    [EnumMember(Value = "Global")]
    Global = 1,

    /// <summary>
    /// Gym
    /// </summary>
    /// <value></value>
    [EnumMember(Value = "Gimnasio")]
    Gym = 2,

    /// <summary>
    /// Gym Branch
    /// </summary>
    [EnumMember(Value = "Sucursal de Gimnasio")]
    GymBranch = 3,

    /// <summary>
    /// Client
    /// </summary>
    [EnumMember(Value = "Cliente")]
    Client = 4,
}