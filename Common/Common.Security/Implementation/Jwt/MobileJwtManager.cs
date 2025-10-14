using Common.Security.Model.Enum;
using Microsoft.Extensions.Configuration;

namespace Common.Security.Implementation;
public class MobileJwtManager(IConfiguration configuration) : JwtManagerBase(configuration)
{
    protected override JwtIdentifier JwtIdentifier => JwtIdentifier.Mobile;
}
