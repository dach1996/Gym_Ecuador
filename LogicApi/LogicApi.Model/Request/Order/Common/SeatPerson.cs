using Common.WebCommon.Attributes.CustomDataAnnotations;

namespace LogicApi.Model.Request.Order.Common;

/// <summary>
/// Asiento Persona
/// </summary>
public class SeatPersonRequest
{
    /// <summary>
    /// Id de Asienta
    /// </summary>
    /// <value></value>
    [ValidateGuid]
    [Required]
    public Guid SeatGuid { get; set; }

    /// <summary>
    /// Id de Persona
    /// </summary>
    /// <value></value>
    [ValidateGuid]
    [Required]
    public Guid PersonGuid { get; set; }

    /// <summary>
    /// Precio de Asiento
    /// </summary>
    /// <value></value>
    [Range(1, double.MaxValue)]
    [Required]
    public decimal Price { get; set; }
}