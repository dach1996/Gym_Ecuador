using Common.WebCommon.Attributes;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Common.WebCommon.Json;

public class SensitiveProductionProperty : DefaultContractResolver
{
    protected override List<MemberInfo> GetSerializableMembers(Type objectType) => [.. objectType.GetProperties()
            .Where(pi => !Attribute.IsDefined(pi, typeof(IgnoreSensibleAttribute)))
            .Where(pi => !Attribute.IsDefined(pi, typeof(JsonIgnoreAttribute)))];
}

public class SensitiveDevelopmentProperty : DefaultContractResolver
{
    protected override List<MemberInfo> GetSerializableMembers(Type objectType) => [.. objectType.GetProperties().Where(pi => !Attribute.IsDefined(pi, typeof(JsonIgnoreAttribute)))];
}