using System.Diagnostics;
using Microsoft.Extensions.Logging;

namespace Common.ArtificialIntelligence.DelegatingHandlers;

internal class LoggerHandler(ILogger<LoggerHandler> logger) : DelegatingHandler
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        logger.LogInformation("Request Headers: {@Headers}", request.Headers);
        var timeStart = Stopwatch.StartNew();
        logger.LogInformation("Request Path: {@Path}", request.RequestUri);
        var response = await base.SendAsync(request, cancellationToken);
        timeStart.Stop();
        logger.LogInformation("Response {@Response} en {@Time}ms", await response.Content.ReadAsStringAsync(cancellationToken), timeStart.ElapsedMilliseconds);
        return response;
    }
}