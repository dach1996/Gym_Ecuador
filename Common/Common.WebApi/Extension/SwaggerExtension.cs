using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System.Reflection;
using Swashbuckle.AspNetCore.SwaggerGen;
using Common.WebApi.Models;
using Common.WebApi.Attributes.DecryptAttribute;
using Microsoft.OpenApi.Any;
using Common.Utils.Extensions;
using Common.WebCommon.Models.Configuration;
using Microsoft.Extensions.Configuration;

namespace Common.WebApi.Extension;

/// <summary>
/// SwaggerSetting
/// </summary>
public static class SwaggerExtension
{
    /// <summary>
    /// Use swagger
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public static IApplicationBuilder UseSwaggerSetting(this WebApplication app)
    {
        var swaggerConfiguration = app.Configuration.GetSection(nameof(SwaggerConfiguration)).Get<SwaggerConfiguration>();
        var env = app.Environment;
        //Verifica el ambiente
        if (env.IsDevelopmentOrDebug())
        {
            //Configura el Swagger normalmente
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = string.Empty;
                string basePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? string.Empty : "..";
                c.SwaggerEndpoint($"{basePath}/swagger/{swaggerConfiguration.Path}/swagger.json", swaggerConfiguration.ApplicationName);
            });
        }
        else
        {
            //Inyecta una sobrecarga de la pagina de Inicio
            Assembly assembly = Assembly.GetExecutingAssembly();
            string assemblyName = assembly.ManifestModule.Name.Replace(".dll", "");
            var guid = Guid.NewGuid();
            app.UseSwagger(o =>
            {
                o.RouteTemplate = $"/swagger/{guid}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{guid}/swagger.json", "Gateway Mobile");
                c.RoutePrefix = string.Empty;
                c.IndexStream = () => assembly.GetManifestResourceStream($"{assemblyName}.SwaggerNotAvailable.html");
            });
        }
        return app;
    }

    /// <summary>
    /// Register Swagger
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddSwaggerServices(this IServiceCollection services, IConfiguration configuration)
    {
        var swaggerConfiguration = configuration.GetSection(nameof(SwaggerConfiguration)).Get<SwaggerConfiguration>();
        services.AddSwaggerGen(c =>
        {
            //Configuración de la documentación
            c.SwaggerDoc(swaggerConfiguration.Path, new OpenApiInfo
            {
                Title = swaggerConfiguration.Title,
                Version = swaggerConfiguration.Version,
                Description = swaggerConfiguration.Description
            });

            //Headers Definitions
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "Token de autorización. \r\n\r\n Ingresa 'Bearer' [space] y luego el token de acceso.\r\n\r\nEjemplo: \"Bearer xxxxx123456789abcdef\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey
            });


            //Headers Requirements
            c.DocumentFilter<SwaggerIgnoreFilter>();
            c.OperationFilter<AddRequiredHeaderParameter>();
            //Agrega las fuentes de comentarios de diferentes proyectos
            foreach (var documentXml in swaggerConfiguration.DocumentsXml)
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, documentXml));
            c.SchemaFilter<AddEncrypFieldDescriptionFilter>();
        });
    }


    public class AddRequiredHeaderParameter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {

            operation.Parameters.Add(new OpenApiParameter
            {
                Description = "Id generado en el dispositivo para rastreo de Logs compartidos",
                Name = "X-RequestId",
                Required = true,
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema { Type = "string" }
            });
            operation.Parameters.Add(new OpenApiParameter
            {
                Description = $"Plataforma.\r\n\r\nEj: ANDROID, iOS",
                Name = "X-Platform",
                Required = true,
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema { Type = "string" }
            });
            operation.Parameters.Add(new OpenApiParameter
            {
                Description = $"Sistema Operativo del Dispositivo",
                Name = "X-SystemOperation",
                Required = true,
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema { Type = "string" }
            });
            operation.Parameters.Add(new OpenApiParameter
            {
                Description = $"Versión de la aplicación.\r\n\r\nEj: 1.1.1, 1.0.1",
                Name = "X-Version",
                Required = true,
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema { Type = "string" }
            });
            operation.Parameters.Add(new OpenApiParameter
            {
                Description = "Time-stamp",
                Name = "X-Time",
                Required = true,
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema { Type = "string" }
            });
            operation.Parameters.Add(new OpenApiParameter
            {
                Description = "Identificador del dispositivo",
                Name = "X-Device",
                Required = true,
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema { Type = "string" }
            });
            operation.Parameters.Add(new OpenApiParameter
            {
                Description = "Modelo de dispositivo Usado",
                Name = "X-Model",
                Required = true,
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema { Type = "string" }
            });
            operation.Parameters.Add(new OpenApiParameter
            {
                Description = "Marca de Dipositivo usado",
                Name = "X-Brand",
                Required = true,
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema { Type = "string" }
            });
            operation.Parameters.Add(new OpenApiParameter
            {
                Description = "Canal usado: 'Mobile'",
                Name = "X-Channel",
                Required = true,
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema { Type = "string" }
            });
            operation.Parameters.Add(new OpenApiParameter
            {
                Description = $"Lenguaje Soportado.\r\n\r\nEjem: Spanish, English",
                Name = "X-Language",
                Required = true,
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema { Type = "string" }
            });
            operation.Parameters.Add(new OpenApiParameter
            {
                Description = "Zona Horaria.\r\n\r\nEjem: America/Guayaquil",
                Name = "X-Timezone",
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema { Type = "string" }
            });
            operation.Parameters.Add(new OpenApiParameter
            {
                Description = "Hash para la integridad del request",
                Name = "X-Content",
                Required = true,
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema { Type = "string" }
            });
            operation.Parameters.Add(new OpenApiParameter
            {
                Description = "Secreto encriptado utilizado para Integridad",
                Name = "X-Secret",
                Required = true,
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema { Type = "string" }
            });
        }
    }

    public class SwaggerIgnoreFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            foreach (var (operation, parameter) in from path in swaggerDoc.Paths
                                                   from operation in path.Value.Operations
                                                   let parameters = operation.Value.Parameters.ToList()
                                                   from parameter in parameters
                                                   where parameter.Name.StartsWith(nameof(ContextRequest))
                                                   select (operation, parameter))
                operation.Value.Parameters.Remove(parameter);
        }
    }

    /// <summary>
    /// Agrega descripción de encriptado
    /// </summary>
    public class AddEncrypFieldDescriptionFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            var attr = context.MemberInfo?.CustomAttributes.FirstOrDefault(x =>
                x.AttributeType.Name == nameof(EncryptedFieldAttribute));
            if (attr is not null)
                schema.Extensions.Add("Encrypt Field", new OpenApiBoolean(true));
        }
    }
}
