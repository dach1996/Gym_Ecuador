using LogicApi.Model.Response.CommonConfiguration.Common;
using PersistenceDb.Models.Administration;

namespace LogicApi.BusinessLogic.Infrastructure.Mapper;
public class CommonConfigurationProfile : Profile
{
    public CommonConfigurationProfile() => ConfigurationMapper();

    /// <summary>
    /// Mapeo de Get Contract Balance
    /// </summary>
    private void ConfigurationMapper()
    {
        _ = CreateMap<Catalog, ItemCatalogResponse>();
    }
}