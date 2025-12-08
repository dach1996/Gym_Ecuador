using Common.Security.Model.Enum;
using Microsoft.Extensions.Configuration;

namespace Common.Security.Implementation.Jwt;
public class WebJwtManager(IConfiguration configuration) : JwtManagerBase(configuration)
{
    protected override JwtIdentifier JwtIdentifier => JwtIdentifier.Web;
}
