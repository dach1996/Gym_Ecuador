using LogicApi.Model.Response.Membership;

namespace LogicApi.Model.Request.Membership;

/// <summary>
/// Solicitud para obtener una membresía por GUID
/// </summary>
public class GetMembershipByGuidRequest : IRequest<GetMembershipByGuidResponse>, IApiBaseRequest
{
    /// <summary>
    /// Guid de la membresía
    /// </summary>
    public Guid MembershipGuid { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="contextRequest"></param>
    /// <param name="membershipGuid"></param>
    public GetMembershipByGuidRequest(ContextRequest contextRequest, Guid membershipGuid)
    {
        ContextRequest = contextRequest;
        MembershipGuid = membershipGuid;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetMembershipByGuidRequest()
    {
    }
}
