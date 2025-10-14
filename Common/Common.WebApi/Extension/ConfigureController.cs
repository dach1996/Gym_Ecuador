using Common.Messages;
using Common.WebApi.Models;
using Common.WebApi.Models.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Mime;
using System.Text.Json.Serialization;
namespace Common.WebApi.Extension;

public static class ConfigureController
{
    public static void AddCustomControllers(this IServiceCollection services)
    {
        services.AddControllers()
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

                    var response = new GenericResponse<Dictionary<string, string[]>>
                    {
                        Code = (int)MessagesCodesError.FormatError,
                        ResponseType = nameof(ResponseType.Error),
                        Content = errorList
                    };
                    var result = new BadRequestObjectResult(response);
                    result.ContentTypes.Add(MediaTypeNames.Application.Json);
                    return result;
                };
            });
    }
}
