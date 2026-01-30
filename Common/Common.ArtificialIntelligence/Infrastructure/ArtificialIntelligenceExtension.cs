using Autofac;
namespace Common.ArtificialIntelligence.Infrastructure;
public  static class ArtificialIntelligenceExtension
{
    public static void UseArtificialIntelligence(this ContainerBuilder builder) =>
       builder.RegisterModule<ArtificialIntelligenceModule>();
}
