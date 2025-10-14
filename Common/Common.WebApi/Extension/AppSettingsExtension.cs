
using Common.WebApi.Models;
using Common.WebCommon.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Common.WebApi.Extension;
public static class AppSettingsExtension
{
    /// <summary>
    /// Agrega AppSettings
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddAppSettingsModel(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.AddSingleton(configuration.Get<AppSettingsApi>());
        _ = services.AddSingleton(configuration.Get<AppSettingsCommon>());
    }


  
}