# Tablas del Esquema GIMNASIO - Resumen de Implementación

## Tablas Creadas

### 1. GIMNASIO
**Archivo**: `GIMNASIO.sql`
**Modelo C#**: `Gym.cs`
- Tabla principal que almacena información de los gimnasios
- Incluye datos básicos, ubicación (latitud/longitud), horarios y contacto
- **Nuevas columnas agregadas**: descripción corta, sitio web, horarios, coordenadas GPS

### 2. TIPOS_MEMBRESIA
**Archivo**: `TIPOS_MEMBRESIA.sql`
**Modelo C#**: `MembershipType.cs`
- Define los diferentes planes de membresía disponibles por gimnasio
- Incluye precio, duración y descripción de beneficios

### 3. MEMBRESIAS
**Archivo**: `MEMBRESIAS.sql`
**Modelo C#**: `Membership.cs`
- Conecta personas con gimnasios y define su rol
- Estados: Activa, Vencida, Congelada
- Roles: Miembro, Entrenador, Administrador

### 4. FOTOS_GIMNASIO
**Archivo**: `FOTOS_GIMNASIO.sql`
**Modelo C#**: `GymPhoto.cs`
- Almacena imágenes del gimnasio
- Permite marcar una imagen como principal
- Incluye descripción para cada foto

### 5. VIDEOS_GIMNASIO
**Archivo**: `VIDEOS_GIMNASIO.sql`
**Modelo C#**: `GymVideo.cs`
- Almacena videos promocionales o de instalaciones
- Links a YouTube, Vimeo, etc.

### 6. MAQUINAS_GIMNASIO
**Archivo**: `MAQUINAS_GIMNASIO.sql`
**Modelo C#**: `GymMachine.cs`
- Inventario de máquinas por gimnasio
- Estados: Disponible, En Mantenimiento
- Incluye fecha de última revisión

### 7. ENTRENADORES
**Archivo**: `ENTRENADORES.sql`
**Modelo C#**: `Trainer.cs`
- Información detallada de entrenadores
- Vinculado a una persona y a un gimnasio específico
- Incluye especialidad y biografía

### 8. CLASES_GRUPALES
**Archivo**: `CLASES_GRUPALES.sql`
**Modelo C#**: `GroupClass.cs`
- Definición de clases grupales (Spinning, Yoga, Zumba, etc.)
- Incluye duración, capacidad máxima y entrenador asignado

### 9. HORARIOS_CLASE
**Archivo**: `HORARIOS_CLASE.sql`
**Modelo C#**: `ClassSchedule.cs`
- Programación semanal de las clases grupales
- Incluye día, hora de inicio/fin y ubicación de sala

### 10. RESERVAS_CLASE
**Archivo**: `RESERVAS_CLASE.sql`
**Modelo C#**: `ClassReservation.cs`
- Sistema de reservas para clases grupales
- Estados: Confirmada, Cancelada, Asistida

### 11. RESENAS_GIMNASIO
**Archivo**: `RESENAS_GIMNASIO.sql`
**Modelo C#**: `GymReview.cs`
- Reseñas y calificaciones de usuarios sobre gimnasios
- Calificación de 1-5 estrellas con comentarios

### 12. CALIFICACIONES_ENTRENADOR
**Archivo**: `CALIFICACIONES_ENTRENADOR.sql`
**Modelo C#**: `TrainerRating.cs`
- Calificaciones específicas para entrenadores
- Calificación de 1-5 estrellas con comentarios

### 13. OBJETIVOS_PERSONALES
**Archivo**: `OBJETIVOS_PERSONALES.sql`
**Modelo C#**: `PersonalGoal.cs`
- Seguimiento granular de objetivos de usuarios
- Tipos: Perder peso, Ganar músculo, Aumentar fuerza
- Estados: Activo, Completado, Abandonado

## Modificaciones a Tablas Existentes

### PERSONA (Esquema AUTENTICACION)
**Archivo**: `ALTER_PERSONA_ADD_URL_FOTO_PERFIL.sql`
**Modelo C#**: Actualizado en `Person.cs`
- **Nueva columna agregada**: `PNA_URL_FOTO_PERFIL` - URL de foto de perfil del usuario

## Actualizaciones en el Código

### PersistenceContext.cs
- Agregado using para `PersistenceDb.Models.Gym`
- Agregada nueva sección `#region Gym DbSet` con todos los DbSet de las nuevas tablas

## Nomenclatura Seguida

- **Tablas SQL**: Nombres en español, mayúsculas
- **Columnas**: Prefijo de 3 letras + nombre descriptivo
- **Modelos C#**: Nombres en inglés con atributos de mapeo
- **Comentarios**: En español tanto en SQL como en C#
- **Constraints**: Incluyen checks para calificaciones (1-5 estrellas)
- **Foreign Keys**: Correctamente definidas entre todas las tablas relacionadas

## Relaciones Principales

1. **Gimnasio** → Tipos de Membresía, Fotos, Videos, Máquinas, Entrenadores
2. **Persona** → Membresías, Reservas, Reseñas, Calificaciones, Objetivos
3. **Entrenador** → Clases Grupales, Calificaciones
4. **Clases Grupales** → Horarios de Clase
5. **Horarios de Clase** → Reservas de Clase

Todas las tablas incluyen campos de auditoría estándar: GUID, fecha de registro y usuario registrador.
