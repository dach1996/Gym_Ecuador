using System.ComponentModel.DataAnnotations;
namespace Common.WebCommon.Attributes.CustomDataAnnotations;

[AttributeUsage(AttributeTargets.Property)]
public class ValidateEnumAttribute : ValidationAttribute
{
    /// <summary>
    /// Validación
    /// </summary>
    /// <param name="value"></param>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var type = value.GetType();
        if (!type.IsEnum)
            return new ValidationResult($"The {value} Is not Enum.");
        if (!Enum.IsDefined(type, value))
            return new ValidationResult($"El valor de {value} no está dentro del Enum: {type.Name}");
        return ValidationResult.Success;
    }
}