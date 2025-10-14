using Common.Utils.Extensions;
namespace LogicApi.Model.Common;

/// <summary>
/// Clase base para abstraer información de cooperativas
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="cooperativeItems"></param>
public class CooperativeData(
    CooperativeItemData[] cooperativeItems
    )
{
    /// <summary>
    /// Items de información de Cooperativas
    /// </summary>
    /// <value></value>
    public CooperativeItemData[] CooperativeItems { get; set; } = cooperativeItems;

    /// <summary>
    /// Obtiene el código de implementación mediante el Id de Cooperativa 
    /// </summary>
    /// <param name="cooperativeId"></param>
    /// <returns></returns>
    public string GetImplementationCodeByCooperativeId(int cooperativeId)
        => CooperativeItems.AsEnumerable().FirstOrDefault(first => first.Id == cooperativeId)?.Code
        ?? throw new ArgumentException($"No se encuentra el código de implementación del Id de Cooperativa: {cooperativeId}", nameof(cooperativeId));

    /// <summary>
    /// Obtiene el código de implementación mediante el Id de Cooperativa 
    /// </summary>
    /// <param name="cooperativeIds"></param>
    /// <returns></returns>
    public string[] GetImplementationCodeByCooperativeIds(IEnumerable<int> cooperativeIds)
    {
        var cooperativeIdsList = CooperativeItems.AsEnumerable().Where(where => cooperativeIds.Contains(where.Id)).Select(select => select.Code);
        if (cooperativeIdsList.IsNullOrEmpty())
            throw new ArgumentException($"No se encuentran los códigos de implementación de los Id de Cooperativa: {cooperativeIds.Join(",")}", nameof(cooperativeIds));
        if (cooperativeIdsList.Count() != cooperativeIds.Count())
            throw new ArgumentException($"No se encuentran todos los códigos de implementación de los Id de Cooperativa: {cooperativeIds.Join(",")}", nameof(cooperativeIds));
        return [.. cooperativeIdsList];
    }


    /// <summary>
    /// Obtiene la información de las cooperativas con su implementación y su nombre a mostrar
    /// </summary>
    /// <returns></returns>
    public IDictionary<string, string> GetDictionaryCooperativeImplementations()
        => CooperativeItems.ToDictionary(key => key.Code, value => value.Name);

    /// <summary>
    /// Obtiene todos los códigos de implementacion registrados
    /// </summary>
    /// <returns></returns>
    public string[] GetAllImplementationCodes()
        => [.. CooperativeItems.Select(select => select.Code)];

    /// <summary>
    /// Obtiene la informaciòn de cooperativa por Id
    /// </summary>
    /// <param name="cooperativeId"></param>
    /// <returns></returns>
    public CooperativeItemData GetCooperativeDataById(int cooperativeId)
        => CooperativeItems.AsEnumerable().FirstOrDefault(first => first.Id == cooperativeId)
        ?? throw new ArgumentException($"No se encuentra el Id de Cooperativa: {cooperativeId}", nameof(cooperativeId));

    /// <summary>
    /// Obtiene la información de la provincia de la cooperativa por Id
    /// </summary> 
    /// <param name="originProvinceId"></param>
    /// <param name="destinationProvinceId"></param>
    /// <returns></returns>
    public CooperativeItemData[] GetCooperativeDataByAllowedProvinceId(int originProvinceId, int destinationProvinceId)
        => [.. CooperativeItems.AsEnumerable()
            .Where(where =>
                where.ProvinceCooperativeInformation.Any(any => any.ProvinceId == originProvinceId) &&
                where.ProvinceCooperativeInformation.Any(any => any.ProvinceId == destinationProvinceId)
            )];

}


/// <summary>
/// Información de Cooperativa
/// </summary>
public class CooperativeItemData : CooperativeDataBase
{
    /// <summary>
    /// Nombre del Key para implementación
    /// </summary>
    /// <value></value>
    public string ImplementationName { get; set; }

    /// <summary>
    /// Nombre Coperativa
    /// </summary>
    /// <value></value>
    public string Name { get; set; }

    /// <summary>
    /// Url de Imagen de Bus
    /// </summary>
    /// <value></value>
    public string BusImageUrl { get; set; }

    /// <summary>
    /// Url de Imagen de Logo
    /// </summary>
    /// <value></value>
    public string LogoImageUrl { get; set; }

    /// <summary>
    /// Información de Buses de Cooperativa
    /// </summary>
    /// <value></value>
    public BusInformation[] Buses { get; set; }

    /// <summary>
    /// Obtiene la informaciòn del bus por el código asignado por la cooperativa
    /// </summary>
    /// <param name="busIdentifierCode"></param>
    /// <returns></returns>
    public BusInformation GetBusInformation(string busIdentifierCode)
        => Buses.AsEnumerable().FirstOrDefault(first => first.CooperativeBusCode == busIdentifierCode)
            ?? throw new ArgumentException($"No se encuentra el Bus con Identificación: {busIdentifierCode} en la cooperativa: {Code}", nameof(busIdentifierCode));

    /// <summary>
    /// Información de Provincia de Cooperativa
    /// </summary>
    /// <value></value>
    public ProvinceCooperativeInformation[] ProvinceCooperativeInformation { get; set; }

    /// <summary>
    /// Obtiene el Id del piso por el Guid
    /// </summary>
    /// <param name="floorGuid"></param>
    /// <returns></returns>
    public int GetFloorIdByGuid(Guid floorGuid)
        => Buses
            .SelectMany(select => select.CooperativeFloors)
            .FirstOrDefault(first => first.CooperativeFloorGuid == floorGuid)
            ?.Id
            ?? throw new ArgumentException($"No se encuentra el Id del piso con el Guid: {floorGuid}", nameof(floorGuid));



    /// <summary>
    /// Obtiene el Guid del piso por el código
    /// </summary>
    /// <param name="floorId"></param>
    /// <returns></returns>
    public Guid GetFloorGuidById(int floorId)
        => Buses
            .SelectMany(select => select.CooperativeFloors)
            .FirstOrDefault(first => first.Id == floorId)
            ?.CooperativeFloorGuid
            ?? throw new ArgumentException($"No se encuentra el Guid del piso con el Id: {floorId}", nameof(floorId));


    /// <summary>   
    /// Obtiene el nombre del punto de transporte por el código
    /// </summary>
    /// <param name="cooperativeTransportPointCode"></param>
    /// <returns></returns>
    public string GetTransportPointNameByCodeCooperativeStation(string cooperativeTransportPointCode)
        => ProvinceCooperativeInformation
            .SelectMany(select => select.TransportPointInformation)
            .FirstOrDefault(first => first.CooperativeTransportPointCode == cooperativeTransportPointCode)
            ?.TransportPointName
            ?? string.Empty;

    /// <summary>   
    /// Obtiene el Id del punto de transporte por el código
    /// </summary>
    /// <param name="placeCode"></param>
    /// <returns></returns>
    public int? GetTransportPointIdByPlaceCode(string placeCode)
        => ProvinceCooperativeInformation
            .SelectMany(select => select.TransportPointInformation)
            .FirstOrDefault(first => first.TransportPointCode == placeCode)
            ?.CooperativeTransportPointId;

}

/// <summary>
/// Clase base de cooperativa
/// </summary>
public abstract class CooperativeDataBase
{
    /// <summary>
    /// Id de Comperativa
    /// </summary>
    /// <value></value>
    public int Id { get; set; }

    /// <summary>
    /// Código Coperativa
    /// </summary>
    /// <value></value>
    public string Code { get; set; }
}


/// <summary>
/// Información de Provincia de Cooperativa
/// </summary>
public class ProvinceCooperativeInformation
{
    /// <summary>
    /// Id de Provincia
    /// </summary>
    /// <value></value>
    public int ProvinceId { get; set; }

    /// <summary>
    /// Nombre de Provincia
    /// </summary>
    public string ProvinceName { get; set; }

    /// <summary>
    /// Información de Punto de Transporte
    /// </summary>
    /// <value></value>
    public TransportPointInformation[] TransportPointInformation { get; set; }
}

/// <summary>
/// Información de Punto de Transporte
/// </summary>
public class TransportPointInformation
{
    /// <summary>
    /// Id de Punto de Transporte
    /// </summary>
    /// <value></value>
    public int CooperativeTransportPointId { get; set; }

    /// <summary>
    /// Código de Punto de Transporte
    /// </summary>
    /// <value></value>
    public string CooperativeTransportPointCode { get; set; }

    /// <summary>
    /// Nombre de Punto de Transporte
    /// </summary>
    /// <value></value>
    public string TransportPointName { get; set; }

    /// <summary>
    /// Código de Provincia
    /// </summary>
    /// <value></value>
    public string TransportPointCode { get; set; }
}

