using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;

namespace Common.WebCommon.Extension;

public static class AppSettingsCommonEstension
{
    /// <summary>
    /// Configura el archivo de Json AppSettings
    /// </summary>
    /// <param name="webApplicationBuilder"></param>
    public static void ConfigureAppSettingsJson(this WebApplicationBuilder webApplicationBuilder)
        => _ = webApplicationBuilder.Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{webApplicationBuilder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables();
}