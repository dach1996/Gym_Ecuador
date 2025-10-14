using Common.Utils.GenericAttributes;

namespace Common.Utils.Extensions;
/// <summary>
/// Extensión de atributo para enumerables
/// </summary>
public static class EnumKeyValueAttributeExtensions
{
    public static string GetEnumKeyValue(this Enum @enum, string key)
        => @enum.GetType().GetMember(@enum.ToString()).FirstOrDefault()?
                                  .GetCustomAttributes(false).OfType<EnumKeyValueAttribute>()
                                  .FirstOrDefault(kv => kv.Key == key)?.Value;
}