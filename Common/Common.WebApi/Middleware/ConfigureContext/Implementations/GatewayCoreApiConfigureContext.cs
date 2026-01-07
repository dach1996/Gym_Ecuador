using System.Security.Claims;
using Common.PluginFactory.Interface;
using Common.Utils;
using Common.Utils.CustomExceptions;
using Common.Utils.Extensions;
using Common.WebApi.Models.ContextRequestModel;
using Common.WebApi.Models.EncryptedClaims;
using Common.WebCommon.Models;
using Common.WebCommon.Models.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Common.WebApi.Middleware.ConfigureContext.Implementations;

/// <summary>
/// Constructor
/// </summary>
/// <param name="appSettings"></param>
public class GatewayCoreApiConfigureContext(ILogger<GatewayCoreApiConfigureContext> logger, IPluginFactory pluginFactory, AppSettingsCommon appSettings)
    : ConfigureContextBase(logger, pluginFactory, appSettings)
{
    private readonly string _aesSecret = appSettings.AesConfiguration.Keys.FirstValueOrDefault(AesConfiguration.AesImplementationName.ServerGeneral);

    public override CommonContextRequest ValidateContext(HttpContext httpContext)
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
        var xRequestId = GetHeaderValueFromName("X-RequestId", httpContext);
        var xplatform = GetHeaderValueFromName("X-Platform", httpContext);
        var xversion = GetHeaderValueFromName("X-Version", httpContext);
        var xtime = GetHeaderValueFromName("X-Time", httpContext);
        var deviceId = GetHeaderValueFromName("X-Device", httpContext);
        var xmodel = GetHeaderValueFromName("X-Model", httpContext);
        var xbrand = GetHeaderValueFromName("X-Brand", httpContext);
        var xauthorization = GetHeaderValueFromName("Authorization", httpContext);
        var xchannel = GetHeaderValueFromName("X-Channel", httpContext);
        var xLanguage = GetHeaderValueFromName("X-Language", httpContext);
        var xTimezone = GetHeaderValueFromName("X-Timezone", httpContext);
        var xContent = GetHeaderValueFromName("X-Content", httpContext);
        var xSystemOperation = GetHeaderValueFromName("X-SystemOperation", httpContext);
        var xSecret = GetHeaderValueFromName("X-Secret", httpContext);
        var xHasGoogleServices = GetHeaderValueFromName("X-HasGoogleServices", httpContext);


        if (!Enum.TryParse<Platform>(xplatform, ignoreCase: true, out var platform))
            throw new AuthException($"La plataforma es invalida (X-Platform: {xplatform}).");

        if (!long.TryParse(xtime, out var timestamp))
            throw new AuthException($"El timestamp tiene un formato invalido (X-Time: {xtime}).");

        if (!Enum.TryParse<Channel>(xchannel, ignoreCase: true, out var channel))
            throw new AuthException($"El Canal es Inválido (X-Platform: {xchannel}).");

        if (!Enum.TryParse<UserLanguage>(xLanguage, ignoreCase: true, out var userLanguage))
            throw new AuthException($"El Idioma es Inválido (X-Language: {xLanguage}).");

        var connectionDataBaseConfiguration = AppSettings.CustomConnectionStrings
            .Find(c => c.Identifier == $"{ConnectionDataBaseIdentifier.ApiCore}")
            ?? throw new AuthException($"No se encuentra la configuración de base de datos con Identificador : '{ConnectionDataBaseIdentifier.ApiCore}'");
        var encryptedFieldClaimDecrypt = GetEncryptedFieldClaimDecrypt(httpContext);
        //Arma el contexto de auditoría
        return new ContextRequest
        {
            RequestId = xRequestId,
            UrlRequest = urlComplete,
            Path = path,
            QueryParameters = queryParams,
            DateRequest = DateTime.UtcNow,
            IpOrigin = ipOrigin,
            Host = host,
            DataBaseConfiguration = connectionDataBaseConfiguration,
            TimeZone = xTimezone.IsNullOrEmpty() ? AppSettings.TimeZone : xTimezone,
            CurrentSubClaim = GetClaimValueOrNullFromName(ClaimTypes.NameIdentifier, httpContext),
            CountRefresh = int.TryParse(GetClaimValueOrNullFromName(nameof(CommonCustomClaims.Refresh), httpContext), out var refress) ? refress : 0,
            Scope = GetClaimValueOrNullFromName(nameof(CommonCustomClaims.Scope), httpContext),
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
                Authorization = xauthorization,
                Time = xtime,
                SystemOperation = xSystemOperation,
                HasGoogleServices = bool.TryParse(xHasGoogleServices, out var result) && result,
            },
            CustomClaims = new CustomClaimsApi
            {
                Jti = GetClaimValueOrNullFromName(nameof(CommonCustomClaims.Jti), httpContext),
                Refresh = GetClaimValueOrNullFromName(nameof(CommonCustomClaims.Refresh), httpContext),
                Scope = GetClaimValueOrNullFromName(nameof(CommonCustomClaims.Scope), httpContext),
                Sub = GetClaimValueOrNullFromName(nameof(CommonCustomClaims.Sub), httpContext),
                UserId = encryptedFieldClaimDecrypt?.UserId,
                UserName = encryptedFieldClaimDecrypt?.UserName,
                Email = encryptedFieldClaimDecrypt?.Email,
                DeviceId = encryptedFieldClaimDecrypt?.DeviceId,
                MobileId = encryptedFieldClaimDecrypt?.MobileId,
                PersonId = encryptedFieldClaimDecrypt?.PersonId,
                UserDateTimeRegister = encryptedFieldClaimDecrypt?.UserDateTimeRegister ?? DateTime.Now
            },
        };
    }


    /// <summary>
    /// Obtiene el user Id desencriptado
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    private EncryptedFieldsApi GetEncryptedFieldClaimDecrypt(HttpContext httpContext)
    {
        var encryptedFieldClaimContext = GetClaimValueOrNullFromName(nameof(EncryptedFieldClaimCommon), httpContext);
        EncryptedFieldsApi encryptedFieldClaim = null;
        if (!encryptedFieldClaimContext.IsNullOrEmpty())
        {
            var encryptedFieldClaimJson = encryptedFieldClaimContext.DecryptAes(_aesSecret);
            encryptedFieldClaim = encryptedFieldClaimJson.ToObject<EncryptedFieldsApi>();
        }
        return encryptedFieldClaim;
    }
}