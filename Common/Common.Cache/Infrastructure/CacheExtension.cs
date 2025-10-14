using Autofac;
using Common.Cache.Implementation.DotNet;
using Common.Cache.Interface;
using Common.Cache.Model;
using Microsoft.Extensions.Configuration;
namespace Common.Cache.Infrastructure;
public static class CacheExtension
{
    private const string PathFile = "Configuration\\Files";
    private const string FileName = "CacheSettings";

    public static void UseCache(this ContainerBuilder builder, string enviromentName)
    {
        _ = builder.RegisterType<DotNetAdministratorCache>().As<IAdministratorCache>();
        _ = builder.RegisterInstance(new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile($"{PathFile}\\{FileName}.json", false, true)
            .AddJsonFile($"{FileName}.{enviromentName}.json", true, true)
            .Build()
            .Get<CacheConfigurationModel>());
    }
}
