
using Common.WebCommon.Models;
using Common.WebJob.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.WebJob.Extension;

public static class AppSettingsWebJobExtension
{

    /// <summary>
    /// Agrega AppSettings
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddAppSettingsModel(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.AddSingleton(configuration.Get<AppSettingsWebJob>());
        _ = services.AddSingleton(configuration.Get<AppSettingsCommon>());
    }
}