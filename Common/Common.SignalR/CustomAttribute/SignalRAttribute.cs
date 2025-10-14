namespace Common.SignalR.CustomAttribute;

/// <summary>
/// Atributo para Class Signalr
/// </summary>
[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
public class HubClassNameAttribute : Attribute
{
    /// <summary>
    /// Nombre de Ruta
    /// </summary>
    /// <value></value>
    public string PathName { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="pathName"></param>
    public HubClassNameAttribute(string pathName) => PathName = pathName;
}