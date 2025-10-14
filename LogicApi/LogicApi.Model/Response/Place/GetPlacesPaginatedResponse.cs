namespace LogicApi.Model.Response.Place;
/// <summary>
/// Respuesta del servicio Obtener Lugares Paginada
/// </summary>
public class GetPlacesPaginatedResponse : IPaginatorApiResponse<PlaceItem>
{
    /// <summary>
    /// Mensaje al Usuario
    /// </summary>
    public string UserMessage { get; set; }

    /// <summary>
    /// Mostrar Mensaje?
    /// </summary>
    /// <value></value>
    public bool ShowMessage { get; set; }


    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="totalRegister"></param>
    /// <param name="registers"></param>
    public GetPlacesPaginatedResponse(int totalRegister, IEnumerable<PlaceItem> registers)
    {
        TotalRegister = totalRegister;
        Registers = registers;
    }

    /// <summary>
    /// Total de Registros
    /// </summary>
    /// <value></value>
    public int TotalRegister { get; set; }


    /// <summary>
    /// Registros
    /// </summary>
    /// <value></value>
    public IEnumerable<PlaceItem> Registers { get; set; }
}

/// <summary>
/// Lugares
/// </summary>
public class PlaceItem
{
    /// <summary>
    /// Código
    /// </summary>
    /// <value></value>
    public string Code { get; set; }

    /// <summary>
    /// Nombre
    /// </summary>
    /// <value></value>
    public string Name { get; set; }

    /// <summary>
    /// Nombre Corto
    /// </summary>
    /// <value></value>
    public string ShortName { get; set; }
}

