using Common.Utils.CustomExceptions;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;
namespace Common.WebApi.Extension;

public static class ConfigureController
{
    public static void AddCustomControllers(this IServiceCollection services)
    {
        _ = services.AddControllers()
            .AddJsonOptions(opt =>
            {
                opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                opt.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            }
           )
            .ConfigureApiBehaviorOptions(opt =>
            {
                opt.InvalidModelStateResponseFactory = context =>
                {
                    var errorList = context.ModelState.ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                    );
                    throw new ModelException("Modelo inválido recibido", errorList);
                };
            });
    }
}
