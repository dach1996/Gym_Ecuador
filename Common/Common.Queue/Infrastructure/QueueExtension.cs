using Autofac;
using Common.Queue.Infrastructure;
public static class QueueExtension
{
    public static void UseQueue(this ContainerBuilder builder) =>
       builder.RegisterModule<QueueModule>();
}
