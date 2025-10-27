namespace LogicApi.Model.Response.MembershipType;

/// <summary>
/// Respuesta de crear tipo de membresía
/// </summary>
public class CreateMembershipTypeResponse : IApiBaseResponse
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
    /// Guid del tipo de membresía creado
    /// </summary>
    public Guid MembershipTypeGuid { get; set; }

    /// <summary>
    /// Nombre del plan
    /// </summary>
    public string PlanName { get; set; }

    /// <summary>
    /// Precio del plan
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="membershipTypeGuid"></param>
    /// <param name="planName"></param>
    /// <param name="price"></param>
    public CreateMembershipTypeResponse(Guid membershipTypeGuid, string planName, decimal price)
    {
        MembershipTypeGuid = membershipTypeGuid;
        PlanName = planName;
        Price = price;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CreateMembershipTypeResponse()
    {
    }
}
