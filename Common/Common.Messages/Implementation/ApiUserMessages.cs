using Common.Messages.Models;
using Newtonsoft.Json;
using System.Text;
namespace Common.Messages.Implementation;
public class ApiUserMessages : IUserMessages
{
    #region Constructor
    /// <summary>
    /// Ruta de archivo Mensajes Api
    /// </summary>
    private const string PATH_BASE_MESSAGES_API = "Configuration\\Messages";
    private readonly Dictionary<MessageType, MessageModel> _data = new();
    private const int DefaultCode = 0;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="userMessagePath"></param>
    public ApiUserMessages()
    {
        var files = Directory.GetFiles(Path.Combine(AppContext.BaseDirectory, PATH_BASE_MESSAGES_API), "*.json");
        //Obtiene los mensajes de Correcto
        var sucessMessageFile = files.AsEnumerable().FirstOrDefault(t => t.Contains($"{MessageType.Success}"));
        if (sucessMessageFile is not null)
        {
            var dataJson = File.ReadAllText(sucessMessageFile, Encoding.UTF8);
            _data.Add(MessageType.Success, JsonConvert.DeserializeObject<MessageModel>(dataJson));
        }
        //Obtiene los mensaje de error
        var errroMessageFile = files.AsEnumerable().FirstOrDefault(t => t.Contains($"{MessageType.Error}"));
        if (errroMessageFile is not null)
        {
            var dataJson = File.ReadAllText(errroMessageFile, Encoding.UTF8);
            _data.Add(MessageType.Error, JsonConvert.DeserializeObject<MessageModel>(dataJson));
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Obtiene un mensaje de error
    /// </summary>
    /// <param name="code"></param>
    /// <param name="languageCode"></param>
    /// <returns></returns>
    public MessageUser GetErrorMessageByCode(int code, UserLanguage? languageCode = null)
    {
        languageCode ??= (int)UserLanguage.Spanish;
        var messagesType = _data.FirstOrDefault(t => t.Key == MessageType.Error);
        var message = messagesType.Value?.Messages?.Find(t => t.Code == code)
            ?? messagesType.Value?.Messages?.Find(t => t.Code == DefaultCode);
        return new MessageUser
        {
            Code = message?.Code ?? 0,
            Message = message?.ItemMessage?.FirstOrDefault(t => t.Key == languageCode).Value
        };
    }

    /// <summary>
    /// Obtiene el mensaje de error por Default
    /// </summary>
    /// <param name="languageCode"></param>
    /// <returns></returns>
    public MessageUser GetDefaultErrorMessage(UserLanguage? languageCode = null)
    {
        languageCode ??= (int)UserLanguage.Spanish;
        var messagesType = _data.FirstOrDefault(t => t.Key == MessageType.Error);
        var message = messagesType.Value?.Messages?.Find(t => t.Code == DefaultCode);
        return new MessageUser
        {
            Code = message?.Code ?? 0,
            Message = message?.ItemMessage?.FirstOrDefault(t => t.Key == languageCode).Value
        };
    }

    /// <summary>
    /// Obtiene un mensaje de acierto
    /// </summary>
    /// <param name="code"></param>
    /// <param name="languageCode"></param>
    /// <returns></returns>
    public MessageUser GetSucessMessageByCode(int code, UserLanguage? languageCode = null)
    {
        languageCode ??= (int)UserLanguage.Spanish;
        var messagesType = _data.FirstOrDefault(t => t.Key == MessageType.Success);
        var message = messagesType.Value?.Messages?.Find(t => t.Code == code)
            ?? messagesType.Value?.Messages?.Find(t => t.Code == DefaultCode);
        return new MessageUser
        {
            Code = message?.Code ?? 0,
            Message = message?.ItemMessage.FirstOrDefault(t => t.Key == languageCode).Value
        };
    }

    /// <summary>
    /// Obtiene el mensaje de acierto por default
    /// </summary>
    /// <param name="languageCode"></param>
    /// <returns></returns>
    public MessageUser GetDefaultSucessMessage(UserLanguage? languageCode = null)
    {
        languageCode ??= (int)UserLanguage.Spanish;
        var messagesType = _data.FirstOrDefault(t => t.Key == MessageType.Success);
        var message = messagesType.Value?.Messages?.Find(t => t.Code == DefaultCode);
        return new MessageUser
        {
            Code = message?.Code ?? 0,
            Message = message?.ItemMessage.FirstOrDefault(t => t.Key == languageCode).Value
        };
    }
    #endregion
}
