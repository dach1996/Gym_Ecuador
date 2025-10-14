using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
namespace Common.Utils.Extensions;
public static class WebHostEnvironmentExtension
{
    /// <summary>
    /// Verifica si est� en un ambiente de Debbug
    /// </summary>
    public static bool IsDebug(this IWebHostEnvironment webHostEnvironment)
        => webHostEnvironment.IsEnvironment("Debug");

    /// <summary>
    /// Verifica si es un ambiente de stagin o producciòn
    /// </summary>
    public static bool IsProductionOrStaging(this IWebHostEnvironment webHostEnvironment)
        => webHostEnvironment.IsProduction() || webHostEnvironment.IsStaging();

    /// <summary>
    /// Verifica si es un ambiente de develop o debbug
    /// </summary>
    public static bool IsDevelopmentOrDebug(this IWebHostEnvironment webHostEnvironment)
        => webHostEnvironment.IsDevelopment() || webHostEnvironment.IsDebug();
}
