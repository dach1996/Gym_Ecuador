using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
namespace Common.WebCommon.Extension;
public static class AutoMapperExtension
{
    /// <summary>
    /// Configure mappers for solution
    /// </summary>
    /// <param name="services"></param>
    /// <param name="profiles"></param>
    public static void ScanAutoMapperProfiles(this IServiceCollection services)
         => _ = services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()
           ?.SelectMany(s => s.GetTypes())
           .Where(p => typeof(Profile).IsAssignableFrom(p))
           .ToArray());
}