using LogicApi.Model.Response.Place;
namespace LogicApi.Model.Request.Place;
/// <summary>
/// Request para obtener los lugares favoritos de un usuario
/// </summary>
public class GetMyFavoritePlaceRequest : IApiBaseRequest<GetMyFavoritePlaceResponse>
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    public GetMyFavoritePlaceRequest(ContextRequest contextRequest)
    {
        ContextRequest = contextRequest;
    }

    /// <summary>
    /// Constructor
    /// </summary>
    public GetMyFavoritePlaceRequest()
    {
        
    }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }
}
