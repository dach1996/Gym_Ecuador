namespace Common.PushNotification.Implementations.Indigitall.Model.Response;
/// <summary>
/// Respuesta genérica de Indigital
/// </summary>
public class IndigitallGenericResponse
{
    /// <summary>
    /// Código de Estado
    /// </summary>
    /// <value></value>
    public int StatusCode { get; set; }

    /// <summary>
    /// Mensaje
    /// </summary>
    /// <value></value>
    public string Message { get; set; }

}


/// <summary>
/// Respuesta genérica de Indigital
/// </summary>
public class IndigitallGenericResponse<T> : IndigitallGenericResponse
{
    /// <summary>
    /// Lista de Resultados
    /// </summary>
    /// <value></value>
    public T Data { get; set; }
}