# Repository Pattern

## Generic Repository

Todos los repositorios heredan de `GenericRepository<TEntity>` que implementa `IGenericRepository<TEntity>`.

## Métodos de Consulta

### GetGenericAsync - Para lecturas

Usar cuando solo necesitas datos específicos (proyección a DTO):

```csharp
var gyms = await UnitOfWork.GymRepository.GetGenericAsync(
    select => new GymItem(select.Id, select.Name),
    where => where.IsActive
).ConfigureAwait(false);
```

### GetByAsync / GetByFirstOrDefaultAsync - Para updates

Usar cuando necesitas la entidad completa para actualizarla:

```csharp
var gym = await UnitOfWork.GymRepository.GetByFirstOrDefaultAsync(
    where => where.Guid == request.GymGuid
).ConfigureAwait(false);

gym.Name = request.Name;
await UnitOfWork.GymRepository.UpdateAsync(gym);
```

## Métodos Comunes

| Método | Uso |
|--------|-----|
| `GetGenericAsync` | Listar con proyección a DTO |
| `GetPaginatorGenericAsync` | Listar paginado con proyección |
| `GetByFirstOrDefaultAsync` | Obtener entidad para update |
| `ExistAnyAsync` | Verificar existencia |
| `AddAsync` | Crear nueva entidad |
| `UpdateAsync` | Actualizar entidad |
| `DeleteAsync` | Eliminar entidad |

## Reglas

- Siempre usar `.ConfigureAwait(false)`
- Para lecturas: `GetGenericAsync` con proyección
- Para updates: `GetByFirstOrDefaultAsync` + `UpdateAsync`
- Acceder repositorios via `UnitOfWork.{Entity}Repository`
