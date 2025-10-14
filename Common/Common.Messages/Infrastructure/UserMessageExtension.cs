using Common.Messages.Implementation;
using Microsoft.Extensions.DependencyInjection;
namespace Common.Messages.Infrastructure;

public static class UserMessageExtension
{
    /// <summary>
    /// Extensión para Api
    /// </summary>
    /// /// <param name="services"></param>
    /// <typeparam name="IUserMessages"></typeparam>
    /// <typeparam name="UserMessages"></typeparam>
    public static void AddUserMessagesApi(this IServiceCollection services) =>
        services.AddSingleton<IUserMessages, ApiUserMessages>();
}
