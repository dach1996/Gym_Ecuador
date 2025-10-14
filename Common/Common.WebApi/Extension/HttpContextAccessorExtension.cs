using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Common.WebApi.Extension;
public static class HttpContextAccessorExtension
{
    /// <summary>
    /// Añadir Dependencia
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddDependencyInjectionSetting(this IServiceCollection services)
    => services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
}
