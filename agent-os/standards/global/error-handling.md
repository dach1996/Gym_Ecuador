## Mejores prácticas de manejo de errores

- **Mensajes Amigables para el Usuario**: Proporcionar mensajes de error claros y accionables a los usuarios sin exponer detalles técnicos o información de seguridad
- **Fallar Rápido y Explícitamente**: Validar la entrada y verificar precondiciones temprano; fallar con mensajes de error claros en lugar de permitir estado inválido
- **Tipos de Excepción Específicos**: Usar tipos de excepción/error específicos en lugar de genéricos para permitir manejo dirigido
- **Manejo Centralizado de Errores**: Manejar errores en límites apropiados (controladores, capas de API) en lugar de dispersar bloques try-catch por todas partes
- **Degradación Gradual**: Diseñar sistemas para degradarse gradualmente cuando fallan servicios no críticos en lugar de romperse completamente
- **Estrategias de Reintento**: Implementar retroceso exponencial para fallos transitorios en llamadas a servicios externos
- **Limpiar Recursos**: Siempre limpiar recursos (manejadores de archivos, conexiones) en bloques finally o mecanismos equivalentes

## Manejo de Errores en el Proyecto Gym

### CustomException
- Usar `CustomException` para todos los errores de negocio
- Pasar código de error desde el enum `MessagesCodesError`
- Incluir mensaje descriptivo en español
- Ejemplo:
```csharp
throw new CustomException((int)MessagesCodesError.SystemError, "Usuario no encontrado");
```

### ExceptionHandlingMiddleware
- El middleware `ExceptionHandlingMiddleware` captura todas las excepciones automáticamente
- Convierte `CustomException` a respuestas HTTP apropiadas
- Registra errores en logs con Serilog usando structured logging
- No es necesario usar try-catch en handlers a menos que se requiera manejo específico

### Logging de Errores
- Usar `ILogger<T>` inyectado en constructores
- Usar niveles apropiados: `Information`, `Warning`, `Error`
- Incluir contexto relevante usando structured logging
- Ejemplo:
```csharp
Logger.LogError("Usuario no encontrado: {UserGuid}", userGuid);
```

### Mensajes de Error
- Los mensajes se obtienen a través de `UserMessages.GetErrorMessageByCode()`
- Los mensajes se internacionalizan según el idioma del usuario
- Los códigos de error están definidos en `MessagesCodesError` enum