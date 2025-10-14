using System.Runtime.Serialization;

namespace Common.Mail.Model.Enum;
/// <summary>
/// Códigos de mail template
/// </summary>
public enum MailTemplateCodes
{
    /// <summary>
    /// Código para olvido de contraseña
    /// </summary>
    [EnumMember(Value = "ForgottenPassword")]
    ForgottenPassword,

    /// <summary>
    /// Nuevo usuario con contraseña temporal 
    /// </summary>
    [EnumMember(Value = "NewUserTemporalPassoword")]
    NewUserTemporalPassoword
}