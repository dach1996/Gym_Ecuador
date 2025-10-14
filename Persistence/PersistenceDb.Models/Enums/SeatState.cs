namespace PersistenceDb.Models.Enums;
/// <summary>
/// Estado de Asiento
/// </summary>
public enum SeatState : byte
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

    /// <summary>
    /// Expirado
    /// </summary>
    Expired = 5,
}