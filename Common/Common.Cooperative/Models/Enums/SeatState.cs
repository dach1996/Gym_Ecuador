namespace Common.Cooperative.Models.Enums;
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
    /// Orden generada (Prepagado)
    /// </summary>
    Prepaid = 3,

    /// <summary>
    /// Pagado
    /// </summary>
    Purchased = 4,
}