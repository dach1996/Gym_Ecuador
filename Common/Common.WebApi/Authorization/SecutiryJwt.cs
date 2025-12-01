
using Microsoft.Extensions.DependencyInjection;
namespace Common.WebApi.Authorization;
public static  class SecurityJwt
{
    public static void AddSecurityJwt(this IServiceCollection services)
     => services.AddAuthentication(options => options.DefaultScheme = SchemaName.CustomSchema)
                .AddScheme<CustomAuthOptions, TokenAuthenticationHandler>(SchemaName.CustomSchema, options => { });
}
