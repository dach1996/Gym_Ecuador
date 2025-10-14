using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Messages.Infrastructure;
using Common.PluginFactory.Infrastructure;
using Common.Security.Infrastructure.Extension;
using Common.WebApi.Extension;
using Common.WebApi.Middleware;
using PersistenceDb.Infrastructure.Extension;
using Common.WebCommon.Extension;
using Common.Clock.Infrastructure;
using Common.Cache.Infrastructure;
using LogicWebSocket.BusinessLogic;
using Common.Security.Infrastructure.Extensions;
using WebSocketsApp.Hubs;
using Common.WebHub.Filters.Intrastructure;
using Common.WebHub.Extension;
using Common.WebHub.Models;
try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.ConfigureAppSettingsJson();
    var logger = builder.AddSerilogCustom();
    logger.Information("Ambiente Seleccionado: {@Enviroment}...", builder.Environment.EnvironmentName);
    logger.Information("Cargando Recursos de Aplicación...");
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Services.AddMemoryCache();
    builder.Services.AddSwaggerServices(builder.Configuration);
    builder.Services.AddCorsSetting(builder.Configuration);
    builder.Services.AddDependencyInjectionSetting();
    builder.Services.AddCustomAuthentications();
    builder.Services.AddCustomAuthorizations();
    builder.Services.AddAppSettingsSocketsModel(builder.Configuration);
    builder.Services.AddCustomControllers();
    builder.Services.AddUserMessagesApi();
    builder.Services.AddApplicationInsightsTelemetry();
    builder.Services.ScanAutoMapperProfiles();
    builder.Services.AddClock();
    builder.Services.AddMediatrTypes(typeof(BusinessLogicWebSocketBase));
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddApiVersioning();
    builder.Services.AddSwaggerGen();
    builder.Services.AddCustomAzureSignalR(builder.Configuration);
    builder.Host.ConfigureContainer<ContainerBuilder>(builderAutofac =>
    {
        builderAutofac.UsePluginFactory();
        builderAutofac.UseJwtManager();
        builderAutofac.UseUnitOfWorkRepository();
        builderAutofac.UseCache(builder.Environment.EnvironmentName);
        builderAutofac.UseRsaSecurity();
        builderAutofac.UseJwtManager();
    });

    var app = builder.Build();
    app.UseSwaggerSetting();
    app.UseCorsSetting();
    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseMiddleware<TimeDurationRequestMiddleware>();
    app.UseMiddleware<ExceptionHandlingMiddleware>();
    app.MapHub<HubManager>("/ws").RequireAuthorization(AuthorizationPolicyName.JwtPolicy);
    app.MapControllers().RequireAuthorization(AuthorizationPolicyName.ApiKeyPolicy);
    logger.Information("Recursos Cargados Correctamente...");
    logger.Information("Iniciando Aplicación...");
    await app.RunAsync().ConfigureAwait(false);
}
catch (Exception ex)
{
    SerilogExtension.GetLoggerCritical().Error(ex, "Error al Iniciar la Aplicación: {@Message}", ex.Message);
    throw;
}
