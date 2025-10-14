using Autofac;
using Common.Security.Infrastructure.Modules;

namespace Common.Security.Infrastructure.Extensions
{
    public static class RsaSecurityExtension
    {
        public static void UseRsaSecurity(this ContainerBuilder builder) =>
            builder.RegisterModule<RsaSecurityModule>();
    }
}
