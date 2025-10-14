namespace Common.Authentication.AuthenticationServicesException;

/// <summary>
/// Autenticación de Servicios
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="message"></param>
public class CustomAuthenticationServicesException(string message) : Exception(message)
{
}
