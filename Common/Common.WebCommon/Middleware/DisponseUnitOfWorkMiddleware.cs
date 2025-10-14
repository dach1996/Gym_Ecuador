using Microsoft.AspNetCore.Http;
using PersistenceDb.Repository.Interfaces.UnitOfWork;

namespace Common.WebCommon.Middleware;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="appSettings"></param>
/// <param name="rsaSecurity"></param>
/// <param name="_pluginFactory"></param>
public class DisponseUnitOfWorkMiddleware(
    RequestDelegate next)
{

    /// <summary>
    /// Invoke
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    public async Task InvokeAsync(
        HttpContext httpContext,
        IUnitOfWork unitOfWork
        )
    {
        using (unitOfWork)
        {
            await next(httpContext).ConfigureAwait(false);
        }
    }
}