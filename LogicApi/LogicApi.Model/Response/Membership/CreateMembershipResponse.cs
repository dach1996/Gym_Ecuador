namespace LogicApi.Model.Response.Membership;

/// <summary>
/// Respuesta de crear membresía
/// </summary>
public class CreateMembershipResponse : IApiBaseResponse
{
    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    public string UserMessage { get; set; }

    /// <summary>
    /// Mostrar Mensaje?
    /// </summary>
    public bool ShowMessage { get; set; }

    /// <summary>
    /// Guid de la membresía creada
    /// </summary>
    public Guid MembershipGuid { get; set; }

    /// <summary>
    /// Estado de la membresía
    /// </summary>
    public string Status { get; set; }

    /// <summary>
    /// Fecha de fin
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="membershipGuid"></param>
    /// <param name="status"></param>
    /// <param name="endDate"></param>
    public CreateMembershipResponse(Guid membershipGuid, string status, DateTime endDate)
    {
        MembershipGuid = membershipGuid;
        Status = status;
        EndDate = endDate;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CreateMembershipResponse()
    {
    }
}
