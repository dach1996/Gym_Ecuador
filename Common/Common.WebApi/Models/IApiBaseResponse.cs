
using System;
using Common.WebCommon.Models;

namespace Common.WebApi.Models;
public interface IApiBaseResponse
{
    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    string UserMessage { get; set; }

    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    bool ShowMessage { get; set; }
}

/// <summary>t
/// Implementación de Paginación con respuesta base de Api
/// </summary>
public interface IPaginatorApiResponse<TResponse> : IPaginatorResponse<TResponse>, IApiBaseResponse
{

}

