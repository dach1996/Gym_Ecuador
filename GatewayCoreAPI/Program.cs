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
using LogicApi.Abstractions.Infrastructure;
using LogicApi.BusinessLogic;
using PersistenceDb.Infrastructure.Extension;
using Common.Models.CatalogsTypeItems.Infrastructure;
using Common.Mail.Infrastructure;
using Common.Tasks.Infrastructure;
using Common.WebCommon.Extension;
using Common.WebCommon.Middleware;
using Common.Card.Infrastructure;
using Common.UserDocumentation.Infrastructure;
using Common.Authentication.Infrastructure;
using Common.Cooperative.Infrastructure;
using LogicCommon.BusinessLogic;
using Common.PushNotification.Infrastructure;
using Common.EventHub.Infrastructure;
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
    builder.Services.AddSecurityApiJwt();
    builder.Services.AddAppSettingsModel(builder.Configuration);
    builder.Services.AddCustomControllers();
    builder.Services.AddUserMessagesApi();
    builder.Services.AddApiVersioning();
    builder.Services.AddCustomAttributes();
    builder.Services.AddApplicationInsightsTelemetry();
    builder.Services.ScanAutoMapperProfiles();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddCatalogsTypeItems();
    builder.Services.AddClock();
    builder.Services.AddCardServices();
    builder.Services.AddDocumentationServices();
    builder.Services.AddCooperativeServices();
    builder.Services.AddEventHub();
    builder.Services.AddMediatrTypes(typeof(BusinessLogicBase), typeof(BusinessLogicCommonBase));
    builder.Services.AddCustomDatabaseConfiguration(builder.Configuration);
    builder.Host.ConfigureContainer<ContainerBuilder>(builderAutofac =>
    {
        builderAutofac.UsePluginFactory();
        builderAutofac.UseEventHub();
        builderAutofac.UseJwtManager();
        builderAutofac.UseUnitOfWorkRepository();
        builderAutofac.UseTaskExecutor();
        builderAutofac.UseRsaSecurity();
        builderAutofac.UseCache(builder.Environment.EnvironmentName);
        builderAutofac.UseBlob();
        builderAutofac.UseMail();
        builderAutofac.UseCardServices();
        builderAutofac.UseDocumentationServices();
        builderAutofac.UseAbstractionsAssemblies();
        builderAutofac.UseAuthenticationServices();
        builderAutofac.UserCooperativeServices();
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
    app.UseMiddleware<ConfigureContextMiddleware>();
    app.UseMiddleware<ValidateIntegrityMiddleware>();
    app.UseMiddleware<DisponseUnitOfWorkMiddleware>();
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

