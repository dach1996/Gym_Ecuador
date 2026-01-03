using System.Runtime.Serialization;

namespace PersistenceDb.Models.Enums;

/// <summary>
/// Ambito de Rol
/// </summary>
public enum RoleType 
{
    /// <summary>
    /// Super Administrador
    /// </summary>
    [EnumMember(Value = "SUPER_ADMIN")]
    SuperAdmin = 1,

    /// <summary>
    /// Administrador
    /// </summary>
    [EnumMember(Value = "ADMIN")]
    Admin = 2,
    
    /// <summary>
    /// Cliente
    /// </summary>
    [EnumMember(Value = "CLIENTE")]
    Client = 3,
}