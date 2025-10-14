using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Authentication;
using PersistenceDb.Repository.Interfaces.Authentication;
namespace PersistenceDb.Authentication;
public class CardRepository : GenericRepository<Card>, ICardRepository
{
    public CardRepository(PersistenceContext dbContext, ILogger<CardRepository> logger) : base(dbContext, logger)
    {
    }
}