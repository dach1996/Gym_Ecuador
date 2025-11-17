using LogicApi.Model.Request.Service;
using LogicApi.Model.Response.Service;
using PersistenceDb.Models.Core;

namespace LogicApi.BusinessLogic.ServiceHandler;

/// <summary>
/// Handler para crear servicio
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class CreateServiceHandler(
    ILogger<CreateServiceHandler> logger,
    IPluginFactory pluginFactory) : ServiceBase<CreateServiceRequest, CreateServiceResponse>(logger, pluginFactory)
{
    /// <summary>
    /// Maneja la creación de un servicio
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<CreateServiceResponse> Handle(CreateServiceRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerAsync(OperationApiName.CreateService, request, async () =>
            {
                // Validar que no exista un servicio con el mismo nombre
                if (await UnitOfWork.ServiceRepository
                    .ExistAnyAsync(where => where.Name.ToLower() == request.Name.ToLower())
                    .ConfigureAwait(false))
                    throw new CustomException((int)MessagesCodesError.SystemError, "Ya existe un servicio con este nombre");

                // Crear el nuevo servicio
                var newService = new Service
                {
                    Name = request.Name,
                    Description = request.Description,
                    RequiresReservation = request.RequiresReservation,
                    IsActive = true
                };

                // Guardar en la base de datos
                await UnitOfWork.ServiceRepository.AddAsync(newService).ConfigureAwait(false);

                return new CreateServiceResponse(newService.Id, newService.Name)
                {
                    UserMessage = GetSuccessMessage(MessagesCodesSucess.Ok),
                    ShowMessage = true
                };
            }
        ).ConfigureAwait(false);
}

