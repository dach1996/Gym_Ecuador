namespace Common.Utils.GenericAttributes;
/// <summary>
/// Atributo para registrar claves y valores y asignarlos a propiedades
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class EnumKeyValueAttribute : Attribute
{
    /// <summary>
    /// Clave
    /// </summary>
    /// <value></value>
    public string Key { get; private set; }

    /// <summary>
    /// Valor
    /// </summary>
    /// <value></value>
    public string Value { get; private set; }

    /// <summary>
    /// Constructor 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    public EnumKeyValueAttribute(string key, string value)
    {
        Key = key;
        Value = value;
    }
}
