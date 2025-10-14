namespace Common.Authentication.Models.Response;
/// <summary>
/// Respuesta autenticación 
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="email"></param>
/// <param name="name"></param>
/// <param name="urlImage"></param>
public class AuthenticationResponse(string email, string name, string urlImage)
{

    /// <summary>
    /// Correo
    /// </summary>
    /// <value></value>
    public string Email { get; set; } = email;

    /// <summary>
    /// Nombre de Usuario
    /// </summary>
    /// <value></value>
    public string Name { get; set; } = name;

    /// <summary>
    /// Imagen
    /// </summary>
    /// <value></value>
    public string UrlImage { get; set; } = urlImage;
}

