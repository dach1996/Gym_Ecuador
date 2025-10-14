using System.Runtime.Serialization;
namespace Common.Utils.Extensions;
public static class EnumExtension
{

    /// <summary>
    /// Obtiene el Enumerable
    /// </summary>
    public static string GetEnumMember(this Enum @enum)
    {
        var attr = @enum.GetType().GetMember(@enum.ToString()).FirstOrDefault()?
                                  .GetCustomAttributes(false).OfType<EnumMemberAttribute>()
                                  .FirstOrDefault();
        return attr == null ? @enum.ToString() : attr.Value;
    }
}