namespace Common.WebCommon.IaTemplateModel.Templates.Response;
/// <summary>
/// Response for Verify Front Personal Document with IA
/// </summary>
public class VerifyFrontPersonalDocumentIATemplateResponse
{
    /// <summary>
    /// Identification
    /// </summary>
    public string IdentificationNumber { get; set; }

    /// <summary>
    /// Names
    /// </summary>
    public string Names { get; set; }

    /// <summary>
    /// Last Names
    /// </summary>
    public string LastNames { get; set; }

    /// <summary>
    /// Birth Date
    /// </summary>
    public string BirthDate { get; set; }
    
    /// <summary>
    /// Gender
    /// </summary>
    public string Gender { get; set; }
}