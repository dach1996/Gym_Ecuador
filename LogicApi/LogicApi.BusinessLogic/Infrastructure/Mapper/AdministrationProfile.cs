using ModelResponse = LogicApi.Model.Response.Administration;
namespace LogicApi.BusinessLogic.Infrastructure.Mapper;
public class AdministrationProfile : Profile
{
    public AdministrationProfile() => ConfigurationMapper();

    /// <summary>
    /// Cooperativa
    /// </summary>
    private void ConfigurationMapper()
    {
        _ = CreateMap<ControlValidationItem, ModelResponse.ControlValidationItem>();
    }
}