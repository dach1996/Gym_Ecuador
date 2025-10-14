using CommonCooperative =  Common.Cooperative.Models.Response;
using ModelResponse =  LogicApi.Model.Response.Ticket;

namespace LogicApi.BusinessLogic.Infrastructure.Mapper;
public class CooperativeProfile : Profile
{
    public CooperativeProfile() => ConfigurationMapper();

    /// <summary>
    /// Cooperativa
    /// </summary>
    private void ConfigurationMapper()
    {
        _ = CreateMap<CommonCooperative.Schedule,  ModelResponse.Schedule>();
    }
}