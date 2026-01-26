# Response Envelope

Todas las respuestas API usan `GenericResponse<T>`:

```json
{
  "code": 200,
  "responseType": "Success",
  "message": "Operación exitosa",
  "content": { ... }
}
```

## Reglas

- Nunca retornar datos sin el envelope
- Usar `Success()` del controller base para respuestas exitosas
- `code`: código numérico de estado
- `responseType`: "Success" o tipo de error
- `message`: mensaje legible para el usuario
- `content`: datos de la respuesta (genérico T)

## Ubicación

`Common.WebApi.Models.GenericResponse<T>`
