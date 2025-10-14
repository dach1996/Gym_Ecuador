using System.Net;
using System.Runtime.Serialization;

namespace Common.Utils.CustomExceptions;

public abstract class BaseException : Exception
{
    /// <summary>
    /// Código HTTP que se debe responder
    /// </summary>
    public int CodeHttp { get; set; }

    /// <summary>
    /// Descripción de porque esta arrojando la excepción  
    /// </summary>
    public string AdditionalInfoError { get; set; }

    /// <summary>
    /// Excepción base 
    /// </summary>
    /// <param name="additionalInfo"></param>
    /// <param name="codeHttp"></param>
    protected BaseException(string additionalInfo, HttpStatusCode codeHttp = HttpStatusCode.InternalServerError) : base(additionalInfo)
    {
        AdditionalInfoError = additionalInfo;
        CodeHttp = (int)codeHttp;
    }
}


