using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Cache.Infrastructure;
using Common.Clock.Infrastructure;
using Common.Mail.Infrastructure;
using Common.PluginFactory.Infrastructure;
using Common.PushNotification.Infrastructure;
using Common.WebCommon.Extension;
using Common.WebJob.Extension;
using Common.EventHub.Infrastructure;
using Common.Templates.Infrastructure;
using LogicCommon.BusinessLogic;
using LogicWebJob.BusinessLogic;
using PersistenceDb.Infrastructure.Extension;
using Serilog;
var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT");
var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(GetConfigurationBuilder().Build())
        .CreateLogger();
try
{
    logger.Information("Ambiente Seleccionado: {@Enviroment}...", environment);
    logger.Information("Cargando Recursos de Aplicación...");
    IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureHostConfiguration(hostConfig => hostConfig.SetBasePath(Directory.GetCurrentDirectory()))
    .ConfigureAppConfiguration((configurationBuilder) => GetConfigurationBuilder())
    .ConfigureWebJobs(wb =>
    {
        wb.AddAzureStorageCoreServices();
        wb.AddAzureStorageBlobs();
        wb.AddAzureStorageQueues();
    })
    .ConfigureServices((context, services) =>
    {
        services.AddMediatrTypes(typeof(BusinessLogicWebJobBase), typeof(BusinessLogicCommonBase));
        services.AddClock();
        services.AddAppSettingsModel(context.Configuration);
        services.AddEventHub();
        services.AddCustomApplicationInsightsTelemetry(context.Configuration);
    })
    .ConfigureLogging((context, loggerConfigurationBuilder) =>
    {
        loggerConfigurationBuilder.ClearProviders();
        loggerConfigurationBuilder.AddSerilog(logger);
    })
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builderAutofac =>
    {
        builderAutofac.UsePluginFactory();
        builderAutofac.UseUnitOfWorkRepository();
        builderAutofac.UseEventHub();
        builderAutofac.UseCache(environment);
        builderAutofac.UseMail();
        builderAutofac.UseQueue();
        builderAutofac.UsePushNotification();
        builderAutofac.UseTemplates();
    })
    .Build();
    using (host)
    {
        logger.Information("Recursos Cargados Correctamente...");
        logger.Information("Iniciando Aplicación...");
        await host.RunAsync();
    }
}
catch (Exception ex)
{
    logger.Error(ex, "Error al inicializar WebJobs: {@Error}", ex.Message);
}


IConfigurationBuilder GetConfigurationBuilder() => new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", false, true)
               .AddJsonFile($"appsettings.{environment}.json", optional: true, true)
               .AddEnvironmentVariables();
