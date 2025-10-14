using Common.Messages.Models;
using Common.PluginFactory.Interface;
using Common.Utils;
using Common.Utils.CustomExceptions;
using Common.Utils.Extensions;
using Common.WebApi.Models;
using Common.WebApi.Models.Enum;
using Common.WebCommon.Models;
using Common.WebCommon.Models.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
namespace Common.WebApi.Middleware;
/// <summary>
/// Constructor
/// </summary>
/// <param name="next"></param>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class ConfigureContextMiddleware(
    RequestDelegate next,
    ILogger<ConfigureContextMiddleware> logger,
    IPluginFactory pluginFactory) : MiddlewareBase(
        next,
        logger,
        pluginFactory)
{
    private readonly AppSettingsApi _appSettings = pluginFactory.GetType<AppSettingsApi>();
    private readonly string _aesSecret = pluginFactory.GetType<AppSettingsApi>().AesConfiguration.Keys.FirstValueOrDefault(AesConfiguration.AesImplementationName.ServerGeneral);


    /// <summary>
    /// Invoke
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext httpContext)
    {
        //Lee la URL
        var queryParams = $"{httpContext.Request.QueryString}";
        string path = httpContext.Request.Path;
        var host = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}";
        var urlComplete = $"{host}{path}";
        if (!string.IsNullOrEmpty(queryParams))
            urlComplete = $"{urlComplete}{queryParams}";
        //Toma IP de origen
        var ipOrigin = httpContext.Connection.RemoteIpAddress.ToString();

        //Toma los headers de interés
        var xRequestId = GetHeaderValueByName("X-RequestId", httpContext);
        var xplatform = GetHeaderValueByName("X-Platform", httpContext);
        var xversion = GetHeaderValueByName("X-Version", httpContext);
        var xtime = GetHeaderValueByName("X-Time", httpContext);
        var deviceId = GetHeaderValueByName("X-Device", httpContext);
        var xmodel = GetHeaderValueByName("X-Model", httpContext);
        var xbrand = GetHeaderValueByName("X-Brand", httpContext);
        var xchannel = GetHeaderValueByName("X-Channel", httpContext);
        var xLanguage = GetHeaderValueByName("X-Language", httpContext);
        var xTimezone = GetHeaderValueByName("X-Timezone", httpContext);
        var xContent = GetHeaderValueByName("X-Content", httpContext);
        var xSystemOperation = GetHeaderValueByName("X-SystemOperation", httpContext);
        var xSecret = GetHeaderValueByName("X-Secret", httpContext);
        var xHasGoogleServices = GetHeaderValueByName("X-HasGoogleServices", httpContext);


        if (!Enum.TryParse<Models.Enum.Platform>(xplatform, ignoreCase: true, out var platform))
            throw new AuthException($"La plataforma es invalida (X-Platform: {xplatform}).");

        if (!long.TryParse(xtime, out var timestamp))
            throw new AuthException($"El timestamp tiene un formato invalido (X-Time: {xtime}).");

        if (!Enum.TryParse<Channel>(xchannel, ignoreCase: true, out var channel))
            throw new AuthException($"El Canal es Inválido (X-Platform: {xchannel}).");

        if (!Enum.TryParse<UserLanguage>(xLanguage, ignoreCase: true, out var userLanguage))
            throw new AuthException($"El Idioma es Inválido (X-Language: {xLanguage}).");

        var encryptedFieldClaimDecrypt = GetEncryptedFieldClaimDecrypt(httpContext);

        var connectionDataBaseConfiguration = _appSettings.CustomConnectionStrings
          .Find(c => c.Identifier == $"{ConnectionDataBaseIdentifier.ApiCore}")
          ?? throw new AuthException($"No se encuentra la configuración de base de datos con Identificador : '{ConnectionDataBaseIdentifier.ApiCore}'");

        //Arma el contexto de auditoría
        var contextRequest = new ContextRequest
        {
            RequestId = xRequestId,
            UrlRequest = urlComplete,
            Path = path,
            QueryParameters = queryParams,
            DateRequest = DateTime.UtcNow,
            IpOrigin = ipOrigin,
            Host = host,
            DataBaseConfiguration = connectionDataBaseConfiguration,
            TimeZone = xTimezone.IsNullOrEmpty() ? _appSettings.TimeZone : xTimezone,
            CurrentSubClaim = GetClaimByName(ClaimTypes.NameIdentifier, httpContext)?.Value,
            CountRefresh = int.TryParse(GetClaimByName(nameof(CustomClaims.Refresh), httpContext)?.Value, out var refress) ? refress : 0,
            Scope = GetClaimByName(nameof(CustomClaims.Scope), httpContext)?.Value,
            Headers = new()
            {
                Model = xmodel,
                Brand = xbrand,
                Version = xversion,
                Platform = platform,
                ClientDate = Util.ConvertUnixToDate(timestamp, Util.UnixConvertion.Seconds),
                DeviceId = deviceId,
                Channel = channel,
                UserLanguage = userLanguage,
                TimeZone = xTimezone,
                Content = xContent,
                Secret = xSecret,
                Time = xtime,
                Authorization = GetHeaderValueByName("Authorization", httpContext),
                SystemOperation = xSystemOperation,
                HasGoogleServices = bool.TryParse(xHasGoogleServices, out var result) && result,
            },
            CustomClaims = new CustomClaims
            {
                Jti = GetClaimByName(nameof(CustomClaims.Jti), httpContext)?.Value,
                Refresh = GetClaimByName(nameof(CustomClaims.Refresh), httpContext)?.Value,
                Scope = GetClaimByName(nameof(CustomClaims.Scope), httpContext)?.Value,
                Sub = GetClaimByName(nameof(CustomClaims.Sub), httpContext)?.Value,
                UserId = encryptedFieldClaimDecrypt?.UserId,
                UserName = encryptedFieldClaimDecrypt?.UserName,
                Email = encryptedFieldClaimDecrypt?.Email,
                DeviceId = encryptedFieldClaimDecrypt?.DeviceId,
                MobileId = encryptedFieldClaimDecrypt?.MobileId,
                UserGuid = encryptedFieldClaimDecrypt?.UserGuid,
                PersonId = encryptedFieldClaimDecrypt?.PersonId
            },
        };

        httpContext.Items[nameof(ContextRequest)] = contextRequest;
        await Next(httpContext).ConfigureAwait(false);
    }

    /// <summary>
    /// Obtiene el user Id desencriptado
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    private EncryptedFieldClaim GetEncryptedFieldClaimDecrypt(HttpContext httpContext)
    {
        var encryptedFieldClaimContext = GetClaimByName(nameof(EncryptedFieldClaim), httpContext)?.Value;
        EncryptedFieldClaim encryptedFieldClaim = null;
        if (!encryptedFieldClaimContext.IsNullOrEmpty())
        {
            var encryptedFieldClaimJson = encryptedFieldClaimContext.DecryptAes(_aesSecret);
            encryptedFieldClaim = encryptedFieldClaimJson.ToObject<EncryptedFieldClaim>();
        }
        return encryptedFieldClaim;
    }
}
