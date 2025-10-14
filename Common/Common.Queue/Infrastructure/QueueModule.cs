using Autofac;
using Common.Queue.Implementation.Azure;
using Common.Queue.Interface;
using Common.Queue.Model.Enum;
namespace Common.Queue.Infrastructure;
public class QueueModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<AzureQueue>().Keyed<IQueue>($"{QueueImplementationName.Azure.ToString().ToUpper()}");
    }
}