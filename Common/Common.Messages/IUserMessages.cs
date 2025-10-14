using Common.Messages.Models;
namespace Common.Messages;
public interface IUserMessages
{
    /// <summary>
    /// Obtiene el mensaje
    /// </summary>
    /// <param name="code"></param>
    /// <param name="languageCode"></param>
    /// <returns></returns>
    MessageUser GetErrorMessageByCode(int code, UserLanguage? languageCode = null);

    /// <summary>
    /// Obtiene el mensaje de acierto
    /// </summary>
    /// <param name="code"></param>
    /// <param name="languageCode"></param>
    /// <returns></returns>
    MessageUser GetSucessMessageByCode(int code, UserLanguage? languageCode = null);

    /// <summary>
    /// Mensaje por default
    /// </summary>
    /// <param name="languageCode"></param>
    /// <returns></returns>
    MessageUser GetDefaultErrorMessage(UserLanguage? languageCode = null);


    /// <summary>
    /// Mensaje por default
    /// </summary>
    /// <param name="languageCode"></param>
    /// <returns></returns>
    MessageUser GetDefaultSucessMessage(UserLanguage? languageCode = null);
}
