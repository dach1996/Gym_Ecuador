
namespace Common.PushNotification.Implementations.Indigitall.Model.Request.Common;
/// <summary>
/// Información Adicional de Request
/// </summary>
public class AdditionalDataRequest
{
    /// <summary>
    /// Ignorar el registro
    /// </summary>
    /// <value></value>
    public bool IgnoreRegister { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="ignoreRegister"></param>
    public AdditionalDataRequest(bool ignoreRegister = true) => IgnoreRegister = ignoreRegister;
}