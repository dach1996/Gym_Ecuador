using PersistenceDb.Models.Core;

namespace PersistenceDb.Repository.Interfaces.Core;
/// <summary>
/// Interfaz de Mensajes Encolados
/// </summary>
public interface IQueueMessageRepository : IGenericRepository< QueueMessage>
{
}