namespace Common.WebHub.Models;

/// <summary>
/// Respuesta Genérica
/// </summary>
public class GenericHubResponse<T>
{

    /// <summary>
    /// Código de respuesta
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// Response type
    /// </summary>
    public ResponseType ResponseType { get; set; }

    /// <summary>
    /// Mensaje al usuario
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// Información de vuelta al usuario
    /// </summary>
    public T Content { get; set; }

    /// <summary>
    /// Respuesta exitosa
    /// </summary>
    public static GenericHubResponse<T> Success(T content, string message = "Operación exitosa")
        => new() { Code = 0, ResponseType = ResponseType.Success, Content = content, Message = message };

    /// <summary>
    /// Respuesta de error
    /// </summary>
    public static GenericHubResponse<T> Error(string message = "Operación fallida")
        => new() { Code = 1, ResponseType = ResponseType.Error, Message = message };

}

/// <summary>
/// Tipo de respuesta
/// </summary>
public enum ResponseType
{
    /// <summary>
    /// Respuesta exitosa
    /// </summary>
    Success,
    /// <summary>
    /// Respuesta de error
    /// </summary>
    Error,
}
