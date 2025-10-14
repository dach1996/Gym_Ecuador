using Microsoft.AspNetCore.Http;

namespace Common.Utils.Extensions;
public static class HttpContextExtensions
{
    /// <summary>
    /// Obtiene un Header
    /// </summary>
    /// <param name="headerName"></param>
    /// <param name="context"></param>
    /// <returns></returns>
    public static string GetHeaderByName(this HttpContext context, string headerName)
        => context.Request.Headers.FirstOrDefault(p => p.Key.Equals(headerName, StringComparison.InvariantCultureIgnoreCase)).Value;
}
