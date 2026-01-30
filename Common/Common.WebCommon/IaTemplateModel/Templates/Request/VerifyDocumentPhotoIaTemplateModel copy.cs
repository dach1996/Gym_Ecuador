using Newtonsoft.Json;

namespace Common.WebCommon.IaTemplateModel.Templates.Request;

/// <summary>
/// Verify Front Personal Document 
/// </summary>
public class VerifyFrontPersonalDocumentIATemplateModel : IIaTemplateModel
{
    /// <summary>
    /// Template Name
    /// </summary>
    [JsonProperty(nameof(TemplateName))]
    public IATemplateName TemplateName => IATemplateName.VerifyFrontPersonalDocument;
}