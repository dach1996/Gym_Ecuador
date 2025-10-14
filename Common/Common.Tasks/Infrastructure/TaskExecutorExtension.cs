using Autofac;

namespace Common.Tasks.Infrastructure;
public static class TaskExecutorExtension
{
    public static void UseTaskExecutor(this ContainerBuilder builder) =>
        builder.RegisterModule<TaskModule>();
}