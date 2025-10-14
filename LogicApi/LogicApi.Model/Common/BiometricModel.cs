namespace LogicApi.Model.Common;
/// <summary>
/// Modelo de datos para la biometría
/// </summary>
public class BiometricModel
{
    /// <summary>
    /// Id de la biometría
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Fecha y hora de generación de la huella
    /// </summary>
    public DateTime DateTimeGenerated { get; set; }

    /// <summary>
    /// Id de la persona
    /// </summary>
    public int PersonId { get; set; }

    /// <summary>
    /// Id del usuario
    /// </summary>
    public int UserId { get; set; }

    /// <summary>
    /// Fecha y hora de expiración de la huella
    /// </summary>
    public DateTime DateTimeExpired { get; set; }
}