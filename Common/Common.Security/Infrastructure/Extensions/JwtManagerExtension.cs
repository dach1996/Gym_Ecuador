using Autofac;
using Common.Security.Infrastructure.Modules;

namespace Common.Security.Infrastructure.Extension
{
    public static class JwtManagerExtension
    {
        public static void UseJwtManager(this ContainerBuilder builder) =>
            builder.RegisterModule<JwtManagerModule>();
    }
}