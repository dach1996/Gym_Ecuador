using LogicCommon.Model.Enum;

namespace LogicApi.Model.Common;
/// <summary>
/// Espacios
/// </summary>
public class SeatRegistered
{
    /// <summary>
    /// Identificador de Espacio en el Bus
    /// </summary>
    /// <value></value>
    public string Identifier { get; set; }

    /// <summary>
    /// Estado de Asiento
    /// </summary>
    /// <value></value>
    public SeatState? SeatState { get; set; }

}