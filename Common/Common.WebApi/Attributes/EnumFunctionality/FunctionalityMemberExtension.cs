namespace Common.WebApi.Attributes.EnumFunctionality;
public static class FunctionalityMemberExtension
{
    /// <summary>
    /// Obtiene el nombre de la función asociada a un valor de enumeración utilizando el atributo FunctionalityMemberAttribute.
    /// </summary>
    public static string GetFunctionalityMember(this Enum @enum)
    {
        var attr = (@enum.GetType().GetMember(@enum.ToString()).FirstOrDefault()?
                                  .GetCustomAttributes(false).OfType<FunctionalityMemberAttribute>()
                                  .FirstOrDefault()) ?? throw new InvalidOperationException("No se ha encontrado el atributo FunctionalityMemberAttribute para el enumerable");
        return attr.FunctionalityName;
    }

    /// <summary>
    /// Obtiene el valor del atributo RegisterAuditLog asociado a un valor de enumeración utilizando el atributo FunctionalityMemberAttribute.
    /// </summary>
    public static bool GetRegisterAuditLog(this Enum @enum)
    {
        var attr = (@enum.GetType().GetMember(@enum.ToString()).FirstOrDefault()?
                                  .GetCustomAttributes(false).OfType<FunctionalityMemberAttribute>()
                                  .FirstOrDefault()) ?? throw new InvalidOperationException("No se ha encontrado el atributo FunctionalityMemberAttribute para el enumerable");
        return attr.RegisterAuditLog;
    }
}