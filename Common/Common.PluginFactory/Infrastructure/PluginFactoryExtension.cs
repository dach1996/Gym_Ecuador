using Autofac;
namespace Common.PluginFactory.Infrastructure
{
    public static class PluginFactoryExtension
    {
        public static void UsePluginFactory(this ContainerBuilder builder) =>
         builder.RegisterModule<PluginFactoryModule>();
    }
}