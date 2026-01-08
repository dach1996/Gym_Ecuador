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
    GymAdministrator = 2,

    /// <summary>
    /// Administrador de sucursal
    /// </summary>
    [EnumMember(Value = "ADMIN_SUCURSAL")]
    GymBranchAdministrator = 3,
    
    /// <summary>
    /// Cliente
    /// </summary>
    [EnumMember(Value = "CLIENTE")]
    Client = 4,
}