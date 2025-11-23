using Autofac;
using Autofac.Extensions.DependencyInjection;
using Common.Blob.Infrastructure;
using Common.Clock.Infrastructure;
using Common.Messages.Infrastructure;
using Common.PluginFactory.Infrastructure;
using Common.Security.Infrastructure.Extension;
using Common.Security.Infrastructure.Extensions;
using Common.WebApi.Attributes.Infrastructure;
using Common.WebApi.Authorization;
using Common.Cache.Infrastructure;
using Common.WebApi.Extension;
using Common.WebApi.Middleware;
using PersistenceDb.Infrastructure.Extension;
using Common.Mail.Infrastructure;
using Common.Tasks.Infrastructure;
using Common.WebCommon.Extension;
using Common.WebCommon.Middleware;
using Common.Authentication.Infrastructure;
using LogicCommon.BusinessLogic;
using Common.PushNotification.Infrastructure;
using LogicAdministratorApi.BusinessLogic;
using Common.WebApi.Models;
try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.ConfigureAppSettingsJson();
    var logger = builder.AddSerilogCustom();
    logger.Information("Ambiente Seleccionado: {@Enviroment}...", builder.Environment.EnvironmentName);
    logger.Information("Cargando Recursos de Aplicación...");
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
    builder.Services.AddHttpClient();
    builder.Services.AddMemoryCache();
    builder.Services.AddSwaggerServices(builder.Configuration);
    builder.Services.AddCorsSetting(builder.Configuration);
    builder.Services.AddDependencyInjectionSetting();
    //builder.Services.AddSecurityJwt();
    //builder.Services.AddAppSettingsModel<AppSettingsAdministrator>(builder.Configuration);
    builder.Services.AddCustomControllers();
    builder.Services.AddUserMessagesApi();
    builder.Services.AddApiVersioning();
    builder.Services.AddCustomAttributes();
    builder.Services.AddApplicationInsightsTelemetry();
    builder.Services.ScanAutoMapperProfiles(builder.Configuration);
    builder.Services.AddClock();
    builder.Services.AddMediatrTypes(typeof(BusinessLogicAdministratorBase), typeof(BusinessLogicCommonBase));
    builder.Host.ConfigureContainer<ContainerBuilder>(builderAutofac =>
    {
        builderAutofac.UsePluginFactory();
        builderAutofac.UseJwtManager();
        builderAutofac.UseUnitOfWorkRepository();
        builderAutofac.UseTaskExecutor();
        builderAutofac.UseRsaSecurity();
        builderAutofac.UseCache(builder.Environment.EnvironmentName);
        builderAutofac.UseBlob();
        builderAutofac.UseMail();
        // builderAutofac.UseAttributeAbstractionsAssemblies();
        //   builderAutofac.UseMiddlewareAbstractionsAssemblies();
        builderAutofac.UseAuthenticationServices();
        builderAutofac.UseQueue();
        builderAutofac.UsePushNotification();
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
    app.UseMiddleware<DisponseUnitOfWorkMiddleware>();
    app.UseMiddleware<ConfigureContextMiddleware>();
    app.MapControllers();
    logger.Information("Recursos Cargados Correctamente...");
    logger.Information("Iniciando Aplicación...");
    await app.RunAsync().ConfigureAwait(false);
}
catch (Exception ex)
{
    SerilogExtension.GetLoggerCritical().Error(ex, "Error al Iniciar la Aplicación: {@Message}", ex.Message);
    throw;
}

