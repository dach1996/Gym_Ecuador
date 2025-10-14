namespace LogicApi.Model.Enum;
/// <summary>
/// Estados de la orden a presentar en el móvil
/// </summary>
public enum OrderViewState
{
    /// <summary>
    /// Pendiente de Pago
    /// </summary>
    PaymentPending,

    /// <summary>
    /// Expirada
    /// </summary>
    Expired,

    /// <summary>
    /// Cancelada
    /// </summary>
    Cancel,

    /// <summary>
    /// Pendiente de Viaje
    /// </summary>
    TravelPending,

    /// <summary>
    /// Completada
    /// </summary>
    Complete,
}
