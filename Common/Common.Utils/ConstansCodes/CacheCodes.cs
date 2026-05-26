using Common.Utils.Cryptography.XxHash;

namespace Common.Utils.ConstansCodes;
/// <summary>
/// Códigos para Cache
/// </summary>
public static class CacheCodes
{
    /// <summary>
    /// Catálogo de archivos
    /// </summary>
    /// <returns></returns>
    public const string CATALOGUE_FILE = nameof(CATALOGUE_FILE);

    /// <summary>
    /// Catálogo
    /// </summary>
    /// <returns></returns>
    public const string CATALOGUE = nameof(CATALOGUE);

    /// <summary>
    /// Parámetros
    /// </summary>
    /// <returns></returns>
    public const string PARAMETER = nameof(PARAMETER);

    /// <summary>
    /// Catálogos iniciales
    /// </summary>
    /// <returns></returns>
    public const string GET_INITIAL_CATALOGUES = nameof(GET_INITIAL_CATALOGUES);
    /// <summary>
    /// Plataformas
    /// </summary>
    /// <returns></returns>
    public const string PLATFORM_ROLES = nameof(PLATFORM_ROLES);

    /// <summary>
    /// Rutas de almacenamiento
    /// </summary>
    /// <returns></returns>
    public const string FILE_BASE_PATHS = nameof(FILE_BASE_PATHS);

    /// <summary>
    /// Información del gimnasio
    /// </summary>
    /// <returns></returns>
    public const string GYM_INFORMATION = nameof(GYM_INFORMATION);

    /// <summary>
    /// Roles
    /// </summary>
    /// <returns></returns>
    public const string ROLES = nameof(ROLES);

    /// <summary>
    /// Parámetros físicos
    /// </summary>
    /// <returns></returns>
    public const string PHYSICAL_PARAMETERS = nameof(PHYSICAL_PARAMETERS);

    /// <summary>
    /// Persona por Número de Cédula
    /// </summary>
    /// <param name="documentNumber"></param>
    /// <returns></returns>
    public static string PersonDetailsByDocumentNumber(string documentNumber) => $"{nameof(PersonDetailsByDocumentNumber)}_{documentNumber}";

    /// <summary>
    /// Información de Usuario por Id de Usuario
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public static string UserInformationByUserId(int userId) => $"{nameof(UserInformationByUserId)}_{userId}";

    /// <summary>
    /// Información de Usuario por Guid de Usuario
    /// </summary>
    /// <param name="token"></param>
    /// <returns></returns>
    public static string LogOutToken(string token) => $"{nameof(LogOutToken)}_{XxHash.ToHash32(token)}";

    /// <summary>
    /// Id de Usuario por Nombre de Usuario
    /// </summary>
    /// <param name="userName"></param>
    /// <returns></returns>
    public static string UserIdByUserName(string userName) => $"{nameof(UserIdByUserName)}_{userName}";

    /// <summary>
    /// Id de Dispositivo por MobileId
    /// </summary>
    /// <param name="mobileId"></param>
    /// <returns></returns>
    public static string DeviceIdByMobileId(string mobileId) => $"{nameof(DeviceIdByMobileId)}_{mobileId}";

    /// <summary>
    /// Url de Imagen por Id de Archivo
    /// </summary>
    /// <param name="guid"></param>
    /// <returns></returns>
    public static string UrlImageByFileId(int fileId) => $"{nameof(UrlImageByFileId)}_{fileId}";

}