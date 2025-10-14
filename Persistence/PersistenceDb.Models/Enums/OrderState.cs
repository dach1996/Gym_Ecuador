
namespace PersistenceDb.Models.Enums;
/// <summary>
/// Estado de Órdenes
/// </summary>
public enum OrderState : byte
{
    /// <summary>
    ///  Creada
    /// </summary>
    Created = 1,

    /// <summary>
    ///  Cancelada 
    /// </summary>
    Cancelated = 2,

    /// <summary>
    ///  Expirada
    /// </summary>
    Expired = 3,

    /// <summary>
    /// Pagada
    /// </summary>
    Paid = 4
}

