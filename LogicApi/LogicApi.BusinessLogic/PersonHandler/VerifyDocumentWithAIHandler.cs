using LogicApi.Model.Request.Person;
using LogicApi.Model.Response.Person;
using Common.Utils.ImageTools;
using Common.WebCommon.IaTemplateModel.Templates.Response;
using Common.WebCommon.IaTemplateModel.Templates.Request;

namespace LogicApi.BusinessLogic.PersonHandler;

/// <summary>
/// Handler para verificar documento con IA
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class VerifyDocumentWithAIHandler(
    ILogger<VerifyDocumentWithAIHandler> logger,
    IPluginFactory pluginFactory)
    : PersonBase<VerifyDocumentWithAIRequest, VerifyDocumentWithAIResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la verificación de documento con IA
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<VerifyDocumentWithAIResponse> Handle(VerifyDocumentWithAIRequest request, CancellationToken cancellationToken)
    => await ExecuteHandlerAsync(OperationApiName.VerifyDocumentWithAI, request, async () =>
        {
            // Convertir imágenes de Base64 a bytes
            var frontDocumentImage = await ImageManagement.OptimizeImageAsync(request.FrontImageBase64).ConfigureAwait(false);
            var frontDocumentResponse = await ProcessDocumentTemplateIaAsJsonAsync<VerifyFrontPersonalDocumentIATemplateResponse>(frontDocumentImage, new VerifyFrontPersonalDocumentIATemplateModel()).ConfigureAwait(false);
            var currentUserPerson = await UnitOfWork.UserRepository.GetFirstOrDefaultGenericAsync(
                select => new { select.Person.DocumentNumber },
                where => where.Id == UserId
            );
            var frontDocumentNumber = frontDocumentResponse.IdentificationNumber.Trim().Replace("-", "").Replace(".", "").Replace(" ", "");
            if (frontDocumentNumber != currentUserPerson.DocumentNumber)
                throw new CustomException((int)MessagesCodesError.DocumentNumberMismatch, "El número de documento no coincide con el número de documento de la persona");
            await UnitOfWork.UserRepository.UpdateByAsync((user => user.HasVerifiedData, true), where => where.Id == UserId).ConfigureAwait(false);
            return VerifyDocumentWithAIResponse.Success();
        }, registerLogAudit: true).ConfigureAwait(false);
}
