namespace LogicApi.Model.Common;

/// <summary>
/// Condiciones
/// </summary>
public class SecurityValidations
{
    /// <summary>
    /// Identificador de Validación
    /// </summary>
    public string Identifier { get; set; }

    /// <summary>
    /// Otp Generado
    /// </summary>
    public object Values { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="identifier"></param>
    /// <param name="values"></param>
    public SecurityValidations(string identifier, object values = null)
    {
        Identifier = identifier;
        Values = values;
    }
}
