using Common.Utils.Cryptography.XxHash;

namespace Common.Utils.ConstansCodes;
/// <summary>
/// Códigos para Cache
/// </summary>
public static class CacheCodes
{

    /// <summary>
    /// Items de catálogo
    /// </summary>
    /// <returns></returns>
    public const string ITEM_CATALOGUE = nameof(ITEM_CATALOGUE);

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
    /// Validación de expresiones regulares
    /// </summary>
    /// <returns></returns>
    public const string PASSWORD_VALIDATE_REGULAR_EXPRESSION = nameof(PASSWORD_VALIDATE_REGULAR_EXPRESSION);

    /// <summary>
    /// Cooperativas
    /// </summary>
    /// <returns></returns>
    public const string COOPERATIVES_IMPLEMENTATION = nameof(COOPERATIVES_IMPLEMENTATION);

    /// <summary>
    /// Cooperativas
    /// </summary>
    /// <returns></returns>
    public const string COOPERATIVES_DATA = nameof(COOPERATIVES_DATA);

    /// <summary>
    /// Información de Bus
    /// </summary>
    /// <returns></returns>
    public const string BUS_INFORMATION = nameof(BUS_INFORMATION);

    /// <summary>
    /// Información de Lugares
    /// </summary>
    /// <returns></returns>
    public const string PLACE_INFORMATION = nameof(PLACE_INFORMATION);

    /// <summary>
    /// Catálogos iniciales
    /// </summary>
    /// <returns></returns>
    public const string GET_INITIAL_CATALOGUES = nameof(GET_INITIAL_CATALOGUES);

    /// <summary>
    /// Lugares
    /// </summary>
    /// <returns></returns>
    public const string PLACES = nameof(PLACES);

    /// <summary>
    /// Lugares
    /// </summary>
    /// <returns></returns>
    public const string PLACES_INFORMATION = nameof(PLACES_INFORMATION);

    /// <summary>
    /// Asientos de Ruta
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="routeGuid"></param>
    /// <returns></returns>
    public static string LastRouteViewByUser(int userId) => $"{nameof(LastRouteViewByUser)}_{userId}";

    /// <summary>
    /// Información de Usuario por Id de Usuario
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public static string UserInformationByUserId(int userId) => $"{nameof(UserInformationByUserId)}_{userId}";

    /// <summary>
    /// Información de Ruta por Guid de Ruta
    /// </summary>
    /// <param name="routeGuid"></param>
    /// <returns></returns>
    public static string RouteDataByRouteGuid(Guid routeGuid) => $"{nameof(RouteDataByRouteGuid)}_{routeGuid}";

    /// <summary>
    /// Asiento por Guid de Ruta, Asiento y Guid de Piso de Bus
    /// </summary>
    /// <param name="routeGuid"></param>
    /// <param name="floorBusCooperativeGuid"></param>
    /// <param name="seatIdentifier"></param>
    /// <returns></returns>
    public static string SeatIdByData(Guid routeGuid, Guid floorBusCooperativeGuid, string seatIdentifier)
    => $"{nameof(SeatIdByData)}_{routeGuid}_{floorBusCooperativeGuid}_{seatIdentifier}";

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
}