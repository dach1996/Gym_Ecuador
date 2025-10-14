using System.Text.Json.Serialization;
using Common.WebCommon.Models;
using MediatR;

namespace Common.WebApi.Models;
public interface IApiBaseRequest
{
    /// <inheritdoc />
    [JsonIgnore]
    ContextRequest ContextRequest { get; set; }
}

/// <summary>
/// Implementa interfas con Respuesta
/// </summary>
/// <typeparam name="TResponse"></typeparam>
public interface IApiBaseRequest<out TResponse> : IApiBaseRequest, IRequest<TResponse>
{

}

/// <summary>
/// Implementa paginación con base de request
/// </summary>
public interface IPaginatorApiRequest<out TResponse> : IPaginatorRequest, IApiBaseRequest<TResponse>
{

}


/// <summary>
/// Implementa paginación y ordenamiento con base de request
/// </summary>
public interface IPaginatorSortApiRequest<out TResponse> : IPaginatorRequest, IApiBaseRequest<TResponse>, ISorteableRequest
{

}

