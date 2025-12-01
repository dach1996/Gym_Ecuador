using System.Security.Claims;
using Common.PluginFactory.Interface;
using Common.Utils;
using Common.Utils.CustomExceptions;
using Common.Utils.Extensions;
using Common.WebApi.Models.AppSettingsModel;
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
public class AdministratorApiConfigureContext(ILogger<AdministratorApiConfigureContext> logger, IPluginFactory pluginFactory, AppSettingsAdministrator appSettings)
    : ConfigureContextBase(logger, pluginFactory, appSettings)
{

    private readonly string _aesSecret = appSettings.AesConfiguration.Keys.FirstValueOrDefault(Common.WebCommon.Models.AesConfiguration.AesImplementationName.ServerGeneral);

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

        //Toma los headers de interés
        var xRequestId = GetHeaderValueFromName("X-RequestId", httpContext);
        var xplatform = GetHeaderValueFromName("X-Platform", httpContext);
        var xversion = GetHeaderValueFromName("X-Version", httpContext);
        var xtime = GetHeaderValueFromName("X-Time", httpContext);
        var xchannel = GetHeaderValueFromName("X-Channel", httpContext);
        var xLanguage = GetHeaderValueFromName("X-Language", httpContext);
        var xTimezone = GetHeaderValueFromName("X-Timezone", httpContext);
        var xContent = GetHeaderValueFromName("X-Content", httpContext);
        var xSecret = GetHeaderValueFromName("X-Secret", httpContext);


        if (!Enum.TryParse<Platform>(xplatform, ignoreCase: true, out var platform))
            throw new AuthException($"La plataforma es invalida (X-Platform: {xplatform}).");

        if (!long.TryParse(xtime, out var timestamp))
            throw new AuthException($"El timestamp tiene un formato invalido (X-Time: {xtime}).");

        if (!Enum.TryParse<Channel>(xchannel, ignoreCase: true, out var channel))
            throw new AuthException($"El Canal es Inválido (X-Chanel: {xchannel}).");

        if (!Enum.TryParse<UserLanguage>(xLanguage, ignoreCase: true, out var userLanguage))
            throw new AuthException($"El Idioma es Inválido (X-Language: {xLanguage}).");

        var connectionDataBaseConfiguration = AppSettings.CustomConnectionStrings
            .Find(c => c.Identifier == $"{ConnectionDataBaseIdentifier.ApiCore}")
            ?? throw new AuthException($"No se encuentra la configuración de base de datos con Identificador : '{ConnectionDataBaseIdentifier.ApiCore}'");
        var encryptedFieldClaimDecrypt = GetEncryptedFieldClaimDecrypt(httpContext);
        //Arma el contexto de auditoría
        return new AdminContextRequest
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
                Version = xversion,
                Platform = platform,
                ClientDate = Util.ConvertUnixToDate(timestamp, Util.UnixConvertion.Seconds),
                Channel = channel,
                UserLanguage = userLanguage,
                TimeZone = xTimezone,
                Content = xContent,
                Secret = xSecret,
                Time = xtime,
            },
            CustomClaims = new CustomClaimsAdministrator
            {
                Jti = GetClaimValueOrNullFromName(nameof(CommonCustomClaims.Jti), httpContext),
                Refresh = GetClaimValueOrNullFromName(nameof(CommonCustomClaims.Refresh), httpContext),
                Scope = GetClaimValueOrNullFromName(nameof(CommonCustomClaims.Scope), httpContext),
                Sub = GetClaimValueOrNullFromName(nameof(CommonCustomClaims.Sub), httpContext),
                UserName = encryptedFieldClaimDecrypt?.UserName,
                Email = encryptedFieldClaimDecrypt?.Email,
                UserGuid = encryptedFieldClaimDecrypt?.UserGuid,
                UserInformationCacheDateTimeCreation = encryptedFieldClaimDecrypt?.UserInformationCacheDateTimeCreation,
                EstablishmentId = encryptedFieldClaimDecrypt?.EstablishmentId,
                EstablishmentBranchId = encryptedFieldClaimDecrypt?.EstablishmentBranchIds,
                VeterinarianId = encryptedFieldClaimDecrypt?.VeterinarianId,
                EstablishmentBranchGuid = encryptedFieldClaimDecrypt?.EstablishmentBranchGuid,
                FirstName = encryptedFieldClaimDecrypt?.FirstName,
                Surname = encryptedFieldClaimDecrypt?.Surname,
            },
        };
    }


    /// <summary>
    /// Obtiene el user Id desencriptado
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    private EncryptedFieldsClaimsAdministrator GetEncryptedFieldClaimDecrypt(HttpContext httpContext)
    {
        var encryptedFieldClaimContext = GetClaimValueOrNullFromName(nameof(EncryptedFieldsClaimsAdministrator), httpContext);
        EncryptedFieldsClaimsAdministrator encryptedFieldClaim = null;
        if (!encryptedFieldClaimContext.IsNullOrEmpty())
        {
            var encryptedFieldClaimJson = encryptedFieldClaimContext.DecryptAes(_aesSecret);
            encryptedFieldClaim = encryptedFieldClaimJson.ToObject<EncryptedFieldsClaimsAdministrator>();
        }
        return encryptedFieldClaim;
    }
}