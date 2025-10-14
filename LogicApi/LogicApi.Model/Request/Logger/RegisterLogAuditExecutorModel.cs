using Common.Tasks;
using Common.WebApi.Models.Enum;

namespace LogicApi.Model.Request.Logger;
/// <summary>
///Modelo ejecutor de 
/// </summary>
public class RegisterLogAuditExecutorModel : IExecutorModel, IApiBaseRequest
{
    /// <summary>
    /// data 
    /// </summary>
    /// <value></value>
    public object RequestData { get; set; }

    /// <summary>
    /// Información de respuesta
    /// </summary>
    /// <value></value>
    public object ResponseData { get; set; }

    /// <summary>
    /// Resultado de Operación
    /// </summary>
    /// <value></value>
    public bool OperationResult { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public ContextRequest ContextRequest { get; set; }

    /// <summary>
    /// Nombre de operación
    /// </summary>
    /// <value></value>
    public OperationApiName OperationName { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="operationName"></param>
    /// <param name="operationResult"></param>
    /// <param name="dataRequest"></param>
    /// <param name="dataResponse"></param>
    public RegisterLogAuditExecutorModel(OperationApiName operationName, bool operationResult, IApiBaseRequest dataRequest, object dataResponse)
    {
        OperationName = operationName;
        OperationResult = operationResult;
        ContextRequest = dataRequest.ContextRequest;
        RequestData = dataRequest;
        ResponseData = dataResponse;
    }

    /// <summary>
    /// Crea un registro exitoso
    /// </summary>
    /// <param name="operationName"></param>
    /// <param name="request"></param>
    /// <param name="dataResponse"></param>
    /// <returns></returns>
    public static RegisterLogAuditExecutorModel SuccessRegister(OperationApiName operationName, IApiBaseRequest request, object dataResponse)
       => new(operationName, true, request, dataResponse);

    /// <summary>
    /// Error de registro
    /// </summary>
    /// <param name="operationName"></param>
    /// <param name="request"></param>
    /// <param name="dataResponse"></param>
    /// <returns></returns>
    public static RegisterLogAuditExecutorModel ErrorRegister(OperationApiName operationName, IApiBaseRequest request, object dataResponse)
       => new(operationName, false, request, dataResponse);

}