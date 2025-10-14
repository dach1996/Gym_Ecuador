using Common.Cooperative.Models.Request;
using Common.Cooperative.Models.Response;
using Microsoft.Extensions.Logging;

namespace Common.Cooperative.Implementation.Imbabura;
/// <summary>
/// Implementaciòn para Imbabura
/// /// </summary>
/// <remarks>
/// Constructor
/// </remarks>
/// <param name="logger"></param>
/// <returns></returns>
public class ImbaburaCooperativeServices(
    ILogger<ImbaburaCooperativeServices> logger) : CooperativeServicesBase(logger), ICooperativeServices
{
    /// <summary>
    /// Implementación
    /// </summary>
    /// <returns></returns>
    protected override CooperativeImplementationName ImplementationName => CooperativeImplementationName.Imbabura;

    public async Task<GetAvailableTripsResponse> GetAvailableTripsAsync(GetAvailableTripsRequest request)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(500)).ConfigureAwait(false);

        return await Task.FromResult(new GetAvailableTripsResponse(ImplementationName, new List<Ticket>{
            new(){
            Identifier = "Imbabura-RUTA_1",
            OriginStationCode="Quito-Ofelia",
            DestinationStationCode ="Ibarra",
            BusCode="BUS_IMBABURA_",
            ApproximatePrice=10,
            Schedule = new (){
                DestinationDateTime = DateTime.Now.AddHours(9),
                OriginDateTime = DateTime.Now.AddHours(1)
        },
        SeatAvailable = 10,
        BusType = Models.Enums.BusType.BusSingleFloor,
        },
            new(){
            Identifier = "Imbabura-RUTA_2",
              OriginStationCode="Atacames",
        DestinationStationCode ="Tena",
        BusCode="BUS_IMBABURA_",
        ApproximatePrice=6,
        BusType = Models.Enums.BusType.BusSingleFloor,
        Schedule = new (){

            DestinationDateTime = DateTime.Now.AddHours(9),
            OriginDateTime = DateTime.Now.AddHours(1)
        },
       SeatAvailable = 10
        },
            new(){
            Identifier = "Imbabura-RUTA_3",
              OriginStationCode="Portoviejo",
        DestinationStationCode ="Cuenca",
        BusCode="BUS_IMBABURA_",
        ApproximatePrice=13,
        BusType = Models.Enums.BusType.BusSingleFloor,
        Schedule = new (){

            DestinationDateTime = DateTime.Now.AddHours(9),
            OriginDateTime = DateTime.Now.AddHours(1)
        },
        SeatAvailable = 10
        }, new(){
            Identifier = "Imbabura-RUTA_4",
              OriginStationCode="Riobamba",
        DestinationStationCode ="Quevedo",
        BusCode="BUS_IMBABURA_",
        ApproximatePrice=13,
        BusType = Models.Enums.BusType.BusSingleFloor,
        Schedule = new (){
            DestinationDateTime = DateTime.Now.AddHours(9),
            OriginDateTime = DateTime.Now.AddHours(1)
        },
        SeatAvailable = 10
        }}
      )).ConfigureAwait(false);
    }

    public async Task<GetBusSeatResponse> GetBusSeatAsync(GetBusSeatRequest request)
    {
        await Task.Delay(TimeSpan.FromMilliseconds(500)).ConfigureAwait(false);
        var tuple = new List<Tuple<string, string>>(){
            Tuple.Create("Imbabura-1","BUS_IMBABURA_4"),
            Tuple.Create("Imbabura-2","BUS_IMBABURA_5"),
            Tuple.Create("Imbabura-3","BUS_IMBABURA_6"),
        };
        return new GetBusSeatResponse(
            tuple.Find(t => t.Item1 == request.TicketIdentifier)?.Item2 ?? tuple[0].Item2,
            new Seat[]{
                new("A1", "PISO_1",10),
                new("A4", "PISO_1",10),
                new("A7", "PISO_1",10),
                new("A22","PISO_1",10),
                new("A44","PISO_1",10),
                new("A43","PISO_1",10),
                new("A2", "PISO_2",10),
                new("A3", "PISO_2",10),
                new("A5", "PISO_2",10),
                new("A29","PISO_2",10),
                new("A32","PISO_2",10),
                new("A33","PISO_2",10),
            }
        );
    }

    public Task<ReserveSeatResponse> ReserveSeatAsync(ReserveSeatRequest request)
    {
        throw new NotImplementedException();
    }
}