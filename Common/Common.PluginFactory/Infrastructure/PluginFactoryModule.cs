using Autofac;
using Common.PluginFactory.Implementation;
using Common.PluginFactory.Interface;

namespace Common.PluginFactory.Infrastructure
{
    public class PluginFactoryModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<PluginFactoryAutofac>().As<IPluginFactory>();
        }
    }
}