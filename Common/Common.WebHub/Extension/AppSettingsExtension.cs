using Common.WebCommon.Models;
using Common.WebHub.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
namespace Common.WebHub.Extension;
public static class AppSettingsExtension
{
    /// <summary>
    /// Agrega AppSettings
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddAppSettingsSocketsModel(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.AddSingleton(configuration.Get<AppSettingsWebSockets>());
        _ = services.AddSingleton(configuration.Get<AppSettingsCommon>());
    }
  
}