using System.ComponentModel.DataAnnotations;
namespace Common.WebCommon.Attributes.CustomDataAnnotations;

[AttributeUsage(AttributeTargets.Property)]
public class ValidateGuidAttribute : ValidationAttribute
{
    /// <summary>
    /// Validación
    /// </summary>
    /// <param name="value"></param>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        => (value is Guid guid) && Guid.Empty == guid ? new ValidationResult($"The {validationContext.MemberName} field is required.") : ValidationResult.Success;
}