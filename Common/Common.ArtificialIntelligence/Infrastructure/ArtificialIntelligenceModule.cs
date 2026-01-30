using Autofac;
using Common.ArtificialIntelligence.Implementations.DeepSeek;
using Common.ArtificialIntelligence.Implementations.Gemini;
using Common.ArtificialIntelligence.Implementations.OpenAI;
using Microsoft.Extensions.DependencyInjection;
namespace Common.ArtificialIntelligence.Infrastructure;
public class ArtificialIntelligenceModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<GeminiArtificialIntelligence>().Keyed<IArtificialIntelligence>($"{ArtificialIntelligenceImplementationType.Gemini.ToString().ToUpper()}");
        builder.RegisterType<OpenAIArtificialIntelligence>().Keyed<IArtificialIntelligence>($"{ArtificialIntelligenceImplementationType.OpenAI.ToString().ToUpper()}");
        builder.RegisterType<DeepSeekArtificialIntelligence>().Keyed<IArtificialIntelligence>($"{ArtificialIntelligenceImplementationType.DeepSeek.ToString().ToUpper()}");
    }
}

public static class ArtificialIntelligenceExtensions
{
    public static void AddArtificialIntelligenceHttpClients(this IServiceCollection services)
    {
        services.AddHttpClient($"{ArtificialIntelligenceImplementationType.Gemini}");
        services.AddHttpClient($"{ArtificialIntelligenceImplementationType.OpenAI}");
        services.AddHttpClient($"{ArtificialIntelligenceImplementationType.DeepSeek}");
    }
}