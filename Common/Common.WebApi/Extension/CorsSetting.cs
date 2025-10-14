using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.WebApi.Extension;

public static class CorsExtension
{
    /// <summary>
    /// Cors
    /// </summary>
    /// <param name="app"></param>
    public static void UseCorsSetting(this IApplicationBuilder app)
        => app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

    public static void AddCorsSetting(this IServiceCollection services, IConfiguration configuration)
    {
        var namePolicy = configuration["Cors:NamePolicy"];
        var originForCORS = configuration.GetSection("Cors:Origins").Get<string[]>();

        services
            .AddCors(options =>
            {
                options
                    .AddPolicy(
                        namePolicy
                        , policy =>
                        {
                            policy
                            .WithOrigins(originForCORS)
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                        }
                    );
            });
    }
}
