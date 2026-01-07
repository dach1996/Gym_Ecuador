using LogicCommon.Model.Response.Person;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Common.WebCommon.Models;

namespace LogicCommon.Model.Request.Person;
/// <summary>
/// Request común para obtener una persona por número de cédula
/// </summary>
public class GetPersonByDocumentNumberCommonRequest : ICommonBaseRequest<GetPersonByDocumentNumberResponse>
{
    /// <summary>
    /// Número de documento de la persona
    /// </summary>
    [Required]
    [StringLength(50)]
    public string DocumentNumber { get; set; }

    /// <summary>
    /// Context
    /// </summary>
    [JsonIgnore]
    public CommonContextRequest CommonContextRequest { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="commonContextRequest"></param>
    /// <param name="documentNumber"></param>
    public GetPersonByDocumentNumberCommonRequest(CommonContextRequest commonContextRequest, string documentNumber)
    {
        CommonContextRequest = commonContextRequest;
        DocumentNumber = documentNumber;
    }

    /// <summary>
    /// Default Constructor
    /// </summary>
    public GetPersonByDocumentNumberCommonRequest()
    {
    }
}

