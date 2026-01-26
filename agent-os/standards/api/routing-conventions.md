# Routing Conventions

## Formato de Ruta Base

```csharp
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
```

## Naming de Acciones

Nombres explícitos para mejor documentación en Swagger:

| Operación | Verbo | Ruta | Ejemplo |
|-----------|-------|------|------|
| Crear | POST | `/Create` | `POST /Gym/Create` |
| Actualizar | PUT | `/Update` | `PUT /Gym/Update` |
| Listar | GET | `/Get{Entities}` | `GET /Gym/GetGyms` |
| Obtener uno | GET | `/GetByGuid` | `GET /Gym/GetByGuid` |
| Eliminar | DELETE | `/Delete` | `DELETE /Gym/Delete` |
| Paginado | GET | `/GetPaginated` | `GET /UserClient/GetPaginated` |

## Atributos Requeridos

```csharp
[HttpGet("GetGyms")]
[ProducesResponseType(200, Type = typeof(GenericResponse<GetGymsResponse>))]
public async Task<IActionResult> GetGyms(...)
```

- Siempre documentar `[ProducesResponseType]` con el tipo de respuesta
- Request de query: `[FromQuery]`
- Request de body: `[FromBody]`
