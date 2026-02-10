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

### GetFirstOrDefaultGenericAsync - Para lecturas con datos relacionados

Cuando necesitas un solo registro con propiedades de navegación (ej. rutina con ejercicios), usar proyección en lugar de `Include`:

```csharp
var routine = await UnitOfWork.RoutineRepository.GetFirstOrDefaultGenericAsync(
    select => new RoutineDetail
    {
        Guid = select.Guid,
        Name = select.Name,
        Exercises = select.RoutineExercises.Select(re => new RoutineExerciseDetail
        {
            ExerciseName = re.Exercise.Name,
            Series = re.Series
        }).ToList()
    },
    where => where.Guid == request.RoutineGuid
).ConfigureAwait(false);
```

EF traduce la proyección a JOINs; no hace falta `.Include()`.

### GetByAsync / GetByFirstOrDefaultAsync - Para updates

Usar cuando necesitas la entidad completa para actualizarla (sin includes):

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
| `GetFirstOrDefaultGenericAsync` | Un registro con proyección (acceso directo a navegación, sin Include) |
| `GetByFirstOrDefaultAsync` | Obtener entidad para update (solo where, sin includes) |
| `ExistAnyAsync` | Verificar existencia |
| `AddAsync` | Crear nueva entidad |
| `UpdateAsync` | Actualizar entidad |
| `DeleteAsync` | Eliminar entidad |

## Reglas

- Siempre usar `.ConfigureAwait(false)`
- **No usar `.Include()` en consultas.** Para datos relacionados en lecturas: `GetGenericAsync` o `GetFirstOrDefaultGenericAsync` con proyección que acceda a las propiedades de navegación (EF genera los JOINs).
- Para lecturas: `GetGenericAsync` / `GetFirstOrDefaultGenericAsync` con proyección.
- Para updates: `GetByFirstOrDefaultAsync(where)` + `UpdateAsync` (sin includes).
- Acceder repositorios via `UnitOfWork.{Entity}Repository`
