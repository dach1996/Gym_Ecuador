namespace LogicApi.Model.Response.Profile;

/// <summary>
/// Respuesta del cálculo de macros de perfil
/// </summary>
public class CalculateProfileResponse : IApiBaseResponse
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
    /// Default Constructor
    /// </summary>
    public CalculateProfileResponse()
    {
    }
}
