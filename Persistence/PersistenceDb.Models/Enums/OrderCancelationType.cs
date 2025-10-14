
namespace PersistenceDb.Models.Enums;
/// <summary>
/// Tipos de cancelación de Orden
/// </summary>
public enum OrderCancelationType : byte
{
    /// <summary>
    ///  Creada
    /// </summary>
    Manual = 1,

    /// <summary>
    ///  Cancelada 
    /// </summary>
    External = 2,

    /// <summary>
    ///  Expirada
    /// </summary>
    Expiration = 3,
}

