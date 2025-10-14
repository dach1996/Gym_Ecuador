namespace PersistenceDb.Models.Enums;
/// <summary>
/// Tipo de Registro de Usuario
/// </summary>
public enum UserTypeRegister : byte
{
    /// <summary>
    /// Registro Manual
    /// </summary>
    Manual = 1,

    /// <summary>
    /// Registro por Google
    /// </summary>
    Google = 2,

    /// <summary>
    /// Registro por Facebook
    /// </summary>
    Facebook = 3

}