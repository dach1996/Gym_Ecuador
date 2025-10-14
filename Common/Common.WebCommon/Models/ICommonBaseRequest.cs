using System.Text.Json.Serialization;
using MediatR;

namespace Common.WebCommon.Models;
/// <summary>
/// Cómo Base Request
/// </summary>
public interface ICommonBaseRequest
{
    /// <inheritdoc />
    [JsonIgnore]
    CommonContextRequest CommonContextRequest { get; set; }
}

/// <summary>
/// Implementa interfas con Respuesta
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface ICommonBaseRequest<out TResponse> : ICommonBaseRequest, IRequest<TResponse>
{

}
