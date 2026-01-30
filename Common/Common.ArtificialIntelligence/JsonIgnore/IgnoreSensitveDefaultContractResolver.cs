using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace Common.ArtificialIntelligence.JsonIgnore;
internal sealed class MaskingValueProvider(IValueProvider innerProvider, string mask) : IValueProvider
{

    public object GetValue(object target)
    {
        var original = innerProvider.GetValue(target);
        return original is null ? null : mask;
    }

    public void SetValue(object target, object value) => innerProvider.SetValue(target, value);
}

internal class IgnoreSensitveDefaultContractResolver : DefaultContractResolver
{
    protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
    {
        var property = base.CreateProperty(member, memberSerialization);
        // Si tiene el atributo personalizado, enmascarar el valor al serializar
        if (member.GetCustomAttribute<IgnoreSensitveAttribute>() != null)
            property.ValueProvider = new MaskingValueProvider(property.ValueProvider, "*****");
        return property;
    }
}