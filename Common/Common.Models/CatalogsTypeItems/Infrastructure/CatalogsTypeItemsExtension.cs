using Common.Models.CatalogsTypeItems.Implementation;
using Common.Models.CatalogsTypeItems.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Models.CatalogsTypeItems.Infrastructure;

public static class CatalogsTypeItemsExtension
{
    public static void AddCatalogsTypeItems(this IServiceCollection services) 
        => services.AddSingleton<ICataloguesType, CataloguesType>(s
             => new CataloguesType("CatalogsTypeItems\\JsonFiles\\ItemsCatalogueTypes.Json"));
}