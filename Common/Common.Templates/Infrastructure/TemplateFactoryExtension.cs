using Autofac;

namespace Common.Templates.Infrastructure;
public static class TemplateFactoryExtension
{
    public static void UseTemplates(this ContainerBuilder builder) =>
       builder.RegisterModule<TemplateModule>();
}