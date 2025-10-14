using Autofac;
namespace Common.Blob.Infrastructure;
public  static class BlobExtension
{
    public static void UseBlob(this ContainerBuilder builder) =>
       builder.RegisterModule<BlobModule>();
}
