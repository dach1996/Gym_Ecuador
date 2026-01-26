# Response Models

## Respuesta Simple

Implementar `IApiBaseResponse`:

```csharp
public class CreateGymResponse : IApiBaseResponse
{
    public string UserMessage { get; set; }
    public bool ShowMessage { get; set; }

    // Datos específicos de la respuesta
    public Guid GymGuid { get; set; }
    public string Name { get; set; }

    public CreateGymResponse(Guid gymGuid, string name)
    {
        GymGuid = gymGuid;
        Name = name;
    }

    public CreateGymResponse() { } // Constructor vacío requerido
}
```

## Respuesta Paginada

Implementar `IPaginatorApiResponse<TItem>`:

```csharp
public class GetEquipmentsResponse(int totalRegister, IEnumerable<EquipmentItem> registers)
    : IPaginatorApiResponse<EquipmentItem>
{
    public string UserMessage { get; set; }
    public bool ShowMessage { get; set; }
    public int TotalRegister { get; set; } = totalRegister;
    public IEnumerable<EquipmentItem> Registers { get; set; } = registers;
}
```

## Reglas

- Siempre incluir `UserMessage` y `ShowMessage`
- Usar constructores con parámetros para datos obligatorios
- Incluir constructor vacío para deserialización
- Para paginación: `TotalRegister` + `Registers`

## Ubicación

```
Logic{Api}.Model/Response/{Entity}/
    Create{Entity}Response.cs
    Get{Entity}sResponse.cs
    {Entity}Item.cs  // Para items en listas
```
