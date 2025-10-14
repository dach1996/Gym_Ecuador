using LogicApi.Model.Request.Administration;
using LogicApi.Model.Response.Administration;
using Common.Utils;
using LogicApi.Model.Common;
using Common.Cooperative;

namespace LogicApi.BusinessLogic.AdministrationHandler;
/// <summary>
/// Constructor
/// </summary>
/// <param name="logger"></param>
/// <param name="pluginFactory"></param>
public class GetCooperativeInformationHandler(
    ILogger<GetCooperativeInformationHandler> logger,
    IPluginFactory pluginFactory) : AdministrationBase<GetCooperativeInformationRequest, GetCooperativeInformationResponse>(
        logger,
        pluginFactory)
{

    /// <summary>
    /// Handler
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public override async Task<GetCooperativeInformationResponse> Handle(GetCooperativeInformationRequest request, CancellationToken cancellationToken)
        => await ExecuteHandlerCacheAsync(
            OperationApiName.GetCooperativeInformation,
            CacheCodes.COOPERATIVES_DATA,
            request,
            async () =>
            {
                var cooperatives = await AdministrationUnitOfWork.CooperativeRepository.GetGenericAsync(
                       select => new
                       {
                           select.Id,
                           select.Code,
                           select.Name,
                           BusImageUrl = select.BusImage.Url,
                           LogoImageUrl = select.LogoImage.Url
                       },
                       where => where.State
                   ).ConfigureAwait(false);

                //Información de Buses
                var buses = await AdministrationUnitOfWork.CooperativeBusRepository.GetGenericAsync(
                    select => new
                    {
                        CooperativeBusId = select.Id,
                        select.CooperativeId,
                        CooperativeCode = select.Code,
                        Diagrams = select.FloorDiagramBusCooperatives.Select(selectDiagram => new CooperativeFloor
                        {
                            Id = selectDiagram.Id,
                            Diagram = selectDiagram.Diagram,
                            FloorNumber = selectDiagram.FloorNumber,
                            CooperativeFloorGuid = selectDiagram.Guid,
                            FloorIdentifier = selectDiagram.FloorCooperativeIdentifier
                        })
                    },
                    where => cooperatives.Select(t => t.Id).Contains(where.CooperativeId)
                        && where.State
                        && where.Id > 0
                ).ConfigureAwait(false);
                //Servicios de Bus
                var busServices = (await AdministrationUnitOfWork.CooperativeBusServiceRepository.GetGenericAsync(
                   select => new
                   {
                       select.Code,
                       select.CooperativeBusId
                   },
                   where => where.State && buses.Select(t => t.CooperativeBusId).Contains(where.CooperativeBusId)
               ).ConfigureAwait(false)).GroupBy(gb => gb.CooperativeBusId);

                //Información de Provincia de Cooperativa
                var cooperativeTransportPointInformation = await AdministrationUnitOfWork.CooperativeTransportPointRepository.GetGenericAsync(
                        select => new
                        {
                            CooperativeTransportPointId = select.Id,
                            select.CooperativeId,
                            ProvinceId = select.TransportPoint.Province.Id,
                            ProvinceName = select.TransportPoint.Province.Name,
                            CooperativeTransportPointCode = select.Code,
                            TransportPointName = select.TransportPoint.Name,
                            TransportPointCode = select.TransportPoint.Code
                        }
                    ).ConfigureAwait(false);

                var provinceCooperativeInformation = cooperativeTransportPointInformation.GroupBy(gb => gb.CooperativeId)
                .Select(select => new
                {
                    CooperativeId = select.Key,
                    ProvinceCooperativeInformation = select.GroupBy(gb => gb.ProvinceId)
                    .Select(select => new ProvinceCooperativeInformation
                    {
                        ProvinceId = select.Key,
                        ProvinceName = select.First().ProvinceName,
                        TransportPointInformation = [.. select.Select(t => new TransportPointInformation
                        {
                            CooperativeTransportPointId = t.CooperativeTransportPointId,
                            CooperativeTransportPointCode = t.CooperativeTransportPointCode,
                            TransportPointName = t.TransportPointName,
                            TransportPointCode = t.TransportPointCode
                        })]
                    }).ToArray()
                });


                //Bus con Servicios
                var busInformation = buses.Join(busServices,
                    b => b.CooperativeBusId,
                    bs => bs.Key,
                    (b, bs) => new
                    {
                        b.CooperativeId,
                        BusesInformation = new BusInformation
                        {
                            CooperativeBusId = b.CooperativeBusId,
                            CooperativeBusCode = b.CooperativeCode,
                            Services = bs.Select(t => t.Code).ToArray(),
                            CooperativeFloors = b.Diagrams
                        }
                    }).GroupBy(gb => gb.CooperativeId);

                //Join Final con Cooperativa
                var cooperativeItemDatas = cooperatives.Join(busInformation,
                cp => cp.Id,
                bi => bi.Key,
                (cp, bi) => new CooperativeItemData
                {
                    Id = cp.Id,
                    Code = cp.Code,
                    Name = cp.Name,
                    BusImageUrl = cp.BusImageUrl,
                    LogoImageUrl = cp.LogoImageUrl,
                    Buses = [.. bi.Select(b => b.BusesInformation)],
                    ProvinceCooperativeInformation = provinceCooperativeInformation.FirstOrDefault(where => where.CooperativeId == cp.Id)?.ProvinceCooperativeInformation ?? []
                }).ToArray();
                var cooperativeData = new CooperativeData(cooperativeItemDatas);
                //Obtiene las implementaciones
                var cooperativeImplementation = Util.GetDictionaryEnums<CooperativeImplementationName>();
                foreach (var cooperative in cooperativeData.CooperativeItems)
                    cooperative.ImplementationName = $"{cooperativeImplementation.First(t => t.Value == cooperative.Code).Key}";
                return new GetCooperativeInformationResponse(cooperativeData);
            }, [UnitOfWorkType.Administration]).ConfigureAwait(false);
}
