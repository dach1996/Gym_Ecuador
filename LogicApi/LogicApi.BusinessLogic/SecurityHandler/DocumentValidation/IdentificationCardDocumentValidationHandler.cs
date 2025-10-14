using LogicApi.Abstractions.Interfaces.Security;
using LogicApi.Model.Request.Security;

namespace LogicApi.BusinessLogic.SecurityHandler.DocumentValidation;
/// <summary>
/// Validación de Documento
/// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class IdentificationCardDocumentValidationHandler(
    ILogger<IdentificationCardDocumentValidationHandler> logger,
    IPluginFactory pluginFactory) : DocumentValidationHandler(
        logger,
        pluginFactory), IDocumentValidationHandler
{

    /// <summary>
    /// Manejador
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public async Task<HandlerResponse> Handle(DocumentValidationRequest request)
    => await ExecuteHandlerAsync(OperationApiName.IdentificationCardValidation, request, async () =>
        {
            var documentNumber = request.DocumentNumber;
            // Verificar que todos los caracteres sean dígitos
            if (!documentNumber.ContainsOnlyNumbers())
                throw new CustomException((int)MessagesCodesError.ValidateIdentificationCard, $"El número de Cédula: '{documentNumber}' continee letras.");
            // Verificar si la cédula tiene 10 dígitos
            if (documentNumber.Length != 10)
                throw new CustomException((int)MessagesCodesError.ValidateIdentificationCard, $"El número de Cédula: '{documentNumber}' ingresado no posee 10 dígitos");
            // Obtener el dígito de control (último dígito) de la cédula
            var digitControl = $"{documentNumber[9]}".ToInt();
            // Obtener los primeros 9 dígitos de la cédula
            var digits = new int[9];
            for (int i = 0; i < 9; i++)
                digits[i] = $"{documentNumber[i]}".ToInt();
            // Calcular la suma de verificación
            var sum = 0;
            for (int i = 0; i < 9; i++)
                if (i % 2 == 0)
                {
                    var temp = digits[i] * 2;
                    if (temp > 9)
                        temp -= 9;
                    sum += temp;
                }
                else
                    sum += digits[i];
            // Calcular el dígito de control esperado
            var digitControlWaiting = 0;
            if (sum % 10 != 0)
                digitControlWaiting = 10 - (sum % 10);
            // Verificar si el dígito de control es correcto
            if (digitControl != digitControlWaiting)
                throw new CustomException((int)MessagesCodesError.ValidateIdentificationCard, $"El número de cédula: '{documentNumber}' no cumple validaciones.");
            return await Task.FromResult(HandlerResponse.Complete()).ConfigureAwait(false);
        });

}