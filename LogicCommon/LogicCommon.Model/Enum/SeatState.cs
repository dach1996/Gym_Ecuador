namespace LogicCommon.Model.Enum;
/// <summary>
/// Estado de Asiento
/// </summary>
public enum SeatState
{
    /// <summary>
    /// Disponible
    /// </summary>
    Available = 1,

    /// <summary>
    /// Reservado
    /// </summary>
    Reserved = 2,

    /// <summary>
    /// No disponible
    /// </summary>
    NotAvailable = 3,

    /// <summary>
    /// Pagado
    /// </summary>
    Purchased = 4,

    /// <summary>
    /// Prepagado
    /// </summary>
    Prepaid = 5,
}