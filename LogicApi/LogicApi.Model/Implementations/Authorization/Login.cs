namespace LogicApi.Model.Implementations.Authorization;
/// <summary>
/// Implementaciones de Login
/// </summary>
public enum LoginImplementations
{
    /// <summary>
    /// Biométrico desde app móvil
    /// </summary>
    Biometric = 1,

    /// <summary>
    /// Usuario y Contraseña desde app móvil
    /// </summary>
    UserPassword = 2,

    /// <summary>
    /// Login por Google
    /// </summary>
    Google = 3
}
