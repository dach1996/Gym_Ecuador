using Common.Messages;
using Common.Utils.CustomExceptions;
using Common.Utils.Extensions;
using LogicApi.Model.Enum;
namespace LogicApi.Model.Common;
/// <summary>
/// Información de Cooperativa
/// </summary>
public class BusInformation
{

    /// <summary>
    /// Identificador de Bus
    /// </summary>
    /// <value></value>
    public int CooperativeBusId { get; set; }

    /// <summary>
    /// Identificador de Cooperativa
    /// </summary>
    /// <value></value>
    public string CooperativeBusCode { get; set; }

    /// <summary>
    /// Servicios
    /// </summary>
    /// <value></value>
    public string[] Services { get; set; }

    /// <summary>
    /// Diagramas
    /// </summary>
    /// <value></value>
    public IEnumerable<CooperativeFloor> CooperativeFloors { get; set; }

    /// <summary>
    /// Obtiene el Id me diante el identificador de piso
    /// </summary>
    /// <param name="floorIdentifier"></param>
    /// <returns></returns>
    public int GetFloorIdByIdentifier(string floorIdentifier)
        => CooperativeFloors.FirstOrDefault(first => first.FloorIdentifier == floorIdentifier)
            ?.Id ?? throw new CustomException((int)MessagesCodesError.DiagramFloorNotFound, $"No se encontró el Id del Identificador: '{floorIdentifier}'");

    /// <summary>
    /// Obtiene el Guid me diante el Id de piso
    /// </summary>
    /// <param name="floorId"></param>
    /// <returns></returns>
    public Guid GetFloorGuidById(int floorId)
        => CooperativeFloors.FirstOrDefault(first => first.Id == floorId)
            ?.CooperativeFloorGuid ?? throw new CustomException((int)MessagesCodesError.DiagramFloorNotFound, $"No se encontró el Guid del Id: '{floorId}'");


    private string[] _seatKeys = [];

    /// <summary>
    /// Obtiene las llaves únicas de los pisos con los asientos
    /// </summary>
    /// <returns></returns>
    public string[] GetSeatsKey()
    {
        if (_seatKeys.IsNullOrEmpty())
            _seatKeys = [.. GetCooperativeFloors()
                .SelectMany(
                    floor => floor.GetFloorSpaces()
                        .SelectMany(row => row
                            .Select(space => $"{floor.CooperativeFloorGuid}{space.SeatIdentifier}".ToLower())))];
        return _seatKeys;

    }

    /// <summary>
    /// Obtiene los pisos de la cooperativa
    /// </summary>
    /// <returns></returns>
    public IEnumerable<CooperativeFloor> GetCooperativeFloors() => CooperativeFloors.OrderBy(order => order.FloorNumber);

}

/// <summary>
/// Diagrama de bus cooperative
/// </summary>
public class CooperativeFloor
{
    /// <summary>
    /// Id
    /// </summary>
    /// <value></value>
    public int Id { get; set; }

    /// <summary>
    /// Identificador de Piso
    /// </summary>
    /// <value></value>
    public string FloorIdentifier { get; set; }

    /// <summary>
    /// Identificador de Bus
    /// </summary>
    /// <value></value>
    public Guid CooperativeFloorGuid { get; set; }

    /// <summary>
    /// Orden de Pisos
    /// </summary>
    /// <value></value>
    public int FloorNumber { get; set; }

    /// <summary>
    /// Diagramas
    /// </summary>
    /// <value></value>
    public string Diagram { get; set; }

    /// <summary>
    /// Espacios de Bus
    /// </summary>
    private IEnumerable<IEnumerable<BusOriginalSpace>> _busSpaces;

    /// <summary>
    /// Espacios de Bus
    /// </summary>
    /// <value></value>
    public IEnumerable<IEnumerable<BusOriginalSpace>> GetFloorSpaces()
    {
        if (_busSpaces.IsNullOrEmpty())
        {
            _ = Diagram ??
                       throw new CustomException((int)MessagesCodesError.DiagramFloorNotFound, $"El piso: '{FloorIdentifier}' con Id: '{Id}' posee diagramas vacíos");
            if (!Diagram.TryToObject<BusOriginalSpace[][]>(out var result))
                throw new CustomException((int)MessagesCodesError.DiagramFloorNotFound, $"El diagrama del piso: '{FloorIdentifier}' con Id: '{Id}' no es válido");
            foreach (var item in result)
                foreach (var seat in item)
                    seat.SeatIdentifier ??= Guid.NewGuid().ToString();
            _busSpaces = result;
        }
        return _busSpaces;
    }

}

/// <summary>
/// Espacios de diagrama de Bus
/// </summary>
public class BusOriginalSpace
{
    /// <summary>
    /// Identificador de Espacio en el Bus
    /// </summary>
    /// <value></value>
    public string SeatIdentifier { get; set; }

    /// <summary>
    /// Tipo de espacio en el Bus
    /// </summary>
    /// <value></value>
    public BusSpaceType BusSpaceType { get; set; }

}

