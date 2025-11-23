namespace Common.WebApi.Attributes.EnumFunctionality;
[AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
/// <summary>
/// Atributo personalizado que se utiliza para asociar un nombre de funcionalidad a un miembro de una enumeración.
/// Este atributo se aplica a campos de enumeración y permite especificar un nombre de funcionalidad que puede ser
/// utilizado en diferentes contextos, como la generación de documentación o la validación de permisos.
/// </summary>
public sealed class FunctionalityMemberAttribute(
    string functionalityName,
    bool registerAuditLog = false) : Attribute
{
    private string _functionalityName = functionalityName;
    private bool _isValueSetExplicitly;

    public string FunctionalityName
    {
        get => _functionalityName;
        set
        {
            _functionalityName = value;
            _isValueSetExplicitly = true;
        }
    }
    public bool RegisterAuditLog => registerAuditLog;
    public bool IsValueSetExplicitly => _isValueSetExplicitly;
}

