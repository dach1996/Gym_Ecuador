namespace LogicApi.Model.Response.Profile;

/// <summary>
/// Respuesta de crear perfil
/// </summary>
public class CreateProfileResponse : IApiBaseResponse
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
    /// Guid del perfil creado
    /// </summary>
    public Guid ProfileGuid { get; set; }

    /// <summary>
    /// Nombre del perfil
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Proteína diaria en gramos
    /// </summary>
    public short Protein { get; set; }

    /// <summary>
    /// Carbohidratos diarios en gramos
    /// </summary>
    public short Carbohydrates { get; set; }

    /// <summary>
    /// Grasas diarias en gramos
    /// </summary>
    public short Fats { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    public CreateProfileResponse(Guid profileGuid, string name, short protein, short carbohydrates, short fats)
    {
        ProfileGuid = profileGuid;
        Name = name;
        Protein = protein;
        Carbohydrates = carbohydrates;
        Fats = fats;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public CreateProfileResponse()
    {
    }
}
