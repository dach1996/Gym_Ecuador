using Autofac;
using Common.Blob.Implementations.Azure;
using Common.Blob.Models;
namespace Common.Blob.Infrastructure;
public class BlobModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<AzureStorageBlobBus>().Keyed<IBlobBus>($"{BlobImplementationNames.AzureStorage.ToString().ToUpper()}");
    }
}