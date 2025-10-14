using Autofac;
namespace Common.Tasks.Infrastructure;
public class TaskModule : Module
{
    /// <summary>
    /// Módulo
    /// </summary>
    /// <param name="builder"></param>
    protected override void Load(ContainerBuilder builder) 
        => _ = builder.RegisterType(typeof(TaskExecutorBuilder)).AsSelf();
}