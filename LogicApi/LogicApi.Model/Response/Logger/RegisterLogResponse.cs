
namespace LogicApi.Model.Response.Logger;
/// <summary>
/// Registro de Log
/// </summary>
public class RegisterLogResponse 
{
    /// <summary>
    /// If log was registered
    /// </summary>
    public bool IsRegistered { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="isRegistered"></param>
    public RegisterLogResponse(bool isRegistered)
        => IsRegistered = isRegistered;
}
