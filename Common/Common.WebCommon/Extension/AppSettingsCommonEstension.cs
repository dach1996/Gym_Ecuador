using System.Reflection;
using Common.Utils.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Common.WebCommon.Extension;

public static class AppSettingsCommonEstension
{
    /// <summary>
    /// Configura el archivo de Json AppSettings
    /// </summary>
    /// <param name="webApplicationBuilder"></param>
    public static void ConfigureAppSettingsJson(this WebApplicationBuilder webApplicationBuilder)
    {
        _ = webApplicationBuilder.Configuration
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{webApplicationBuilder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
        if (webApplicationBuilder.Environment.IsDevelopmentOrDebug())
        {
            // Usar el assembly de entrada (el proyecto que ejecuta la aplicación) en lugar del assembly común
            // para que pueda encontrar el UserSecretsId definido en el .csproj del proyecto de ejecución
            var entryAssembly = Assembly.GetEntryAssembly();
            if (entryAssembly != null)
            {
                _ = webApplicationBuilder.Configuration.AddUserSecrets(entryAssembly, true);
            }
        }
    }
}