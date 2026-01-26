# Request Models

## Estructura Básica

Todos los requests implementan `IApiBaseRequest<TResponse>`:

```csharp
public class CreateGymRequest : IApiBaseRequest<CreateGymResponse>
{
    [Required]
    public string Name { get; set; }

    [Required]
    [ValidateGuid]
    public Guid GymBranchGuid { get; set; }

    [JsonIgnore]
    public CommonContextRequest ContextRequest { get; set; }
}
```

## Reglas

- Implementar `IApiBaseRequest<TResponse>` donde TResponse es el tipo de respuesta
- Siempre incluir `CommonContextRequest ContextRequest` con `[JsonIgnore]`
- Usar DataAnnotations para validaciones: `[Required]`, `[MaxLength]`, etc.
- Usar validaciones custom: `[ValidateGuid]` para GUIDs

## Ubicación

```
Logic{Api}.Model/Request/{Entity}/
    Create{Entity}Request.cs
    Update{Entity}Request.cs
    Get{Entity}sRequest.cs       // Listar/Paginar
    Get{Entity}ByGuidRequest.cs  // Obtener uno
    Delete{Entity}Request.cs
```

## Paginación

```csharp
[Required]
public int PageNumber { get; set; } = 1;

[Required]
public int PageSize { get; set; } = 10;
```
