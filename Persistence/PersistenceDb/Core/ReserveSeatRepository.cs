using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Core;
using PersistenceDb.Repository.Interfaces.Core;

namespace PersistenceDb.Core;

public class ReserveSeatRepository(PersistenceContext dbContext, ILogger<ReserveSeatRepository> logger) : GenericRepositoryRowControl<ReserveSeat>(dbContext, logger), IReserveSeatRepository
{
}