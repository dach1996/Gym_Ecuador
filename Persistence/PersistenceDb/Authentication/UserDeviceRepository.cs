using System.Linq.Expressions;
using EFCore.BulkExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersistenceDb.Models.Authentication;
using PersistenceDb.Repository.Interfaces.Authentication;

namespace PersistenceDb.Authentication;

public class UserDeviceRepository : GenericRepository<UserDevice>, IUserDeviceRepository
{
    public UserDeviceRepository(PersistenceContext dbContext, ILogger<UserDeviceRepository> logger) : base(dbContext, logger)
    {
    }

    public async Task<int> UpdateByTestAsync<TProperty, TProperty2>(
        Expression<Func<UserDevice, TProperty>> propertyExpression,
        TProperty value,
        Expression<Func<UserDevice, TProperty2>> propertyExpression2,
        TProperty2 value2
    )
    {
        return await Context.Set<UserDevice>()
        .Where(where => where.UserId == 1)
        .ExecuteUpdateAsync(set => set.SetProperty(propertyExpression, value).SetProperty(propertyExpression2, value2)).ConfigureAwait(false);
    }
}