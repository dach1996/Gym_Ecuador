namespace LogicApi.Model.Response.Authorization;
/// <summary>
/// Respuesta de Refresh Token
/// </summary>
public class RefreshTokenResponse
{
    /// <summary>
    /// Token de acceso
    /// </summary>
    public string AccessToken { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="accessToken"></param>
    public RefreshTokenResponse(string accessToken) => AccessToken = accessToken;
}