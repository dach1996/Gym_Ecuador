# Product Roadmap

1. [ ] **Gestión Multi-Gimnasio y Sucursales** — Sistema completo para crear y gestionar múltiples gimnasios y sus sucursales, incluyendo configuración de horarios, información de contacto, ubicación y estado activo/inactivo. Permite a super administradores gestionar toda la cadena desde una plataforma centralizada. `L`

2. [ ] **Sistema de Autenticación y Roles Multi-Nivel** — Implementación completa de autenticación con roles jerárquicos (Super Administrador, Administrador de Gimnasio, Administrador de Sucursal, Cliente, Entrenador) y control de acceso basado en permisos. Incluye gestión de usuarios, asignación de roles y autenticación JWT. `M`

3. [ ] **Planes de Membresía y Suscripciones** — Sistema completo de planes de membresía por sucursal con configuración de precios, duración, beneficios (acceso multi-sucursal, clases incluidas, sesiones con entrenador), períodos de prueba, opciones de congelamiento y renovación automática. Incluye gestión de membresías activas de clientes con fechas de inicio y fin. `L`

4. [ ] **Sistema de Entrenadores** — Gestión completa de entrenadores incluyendo creación de perfiles con biografías, asignación a gimnasios y sucursales, gestión de clases asignadas y clientes asignados. Permite a administradores gestionar el personal de entrenadores y a entrenadores ver su información y asignaciones. `M`

5. [ ] **Gestión de Equipos y Máquinas** — Catálogo completo de equipos y máquinas del gimnasio con información detallada (nombre, descripción, ubicación en sucursal, estado operativo). Permite a administradores gestionar inventario de equipos por sucursal y realizar seguimiento de mantenimiento. `S`

6. [ ] **Catálogo de Ejercicios** — Sistema completo de catálogo de ejercicios con descripciones detalladas, instrucciones paso a paso, imágenes de referencia, videos opcionales y sistema de tags para categorización (por grupo muscular, tipo de ejercicio, equipamiento requerido). Permite búsqueda y filtrado avanzado. `M`

7. [ ] **Sistema de Rutinas Personalizadas** — Creación y gestión de rutinas personalizadas que pueden ser asignadas a clientes específicos. Incluye composición de rutinas con ejercicios del catálogo, configuración de series, repeticiones, peso, descanso y orden de ejercicios. Permite a entrenadores crear rutinas y a clientes visualizar sus rutinas asignadas. `L`

8. [ ] **Gestión de Clases Grupales** — Sistema completo de clases grupales con creación de clases, configuración de horarios recurrentes, capacidad máxima de participantes, asignación de entrenadores y gestión de estado (activa, cancelada, completada). Permite a administradores gestionar el calendario de clases por sucursal. `M`

9. [ ] **Sistema de Reservas de Clases** — Plataforma completa para que clientes reserven clases grupales con verificación automática de capacidad disponible, validación de membresía activa y beneficios incluidos. Incluye cancelación de reservas, lista de espera automática y confirmaciones. Permite a clientes ver sus reservas activas y historial. `M`

10. [ ] **Registro de Series y Seguimiento de Progreso** — Sistema para que clientes registren las series realizadas durante sus entrenamientos, incluyendo peso, repeticiones, tiempo de descanso y fecha. Permite visualización de historial de progreso con gráficos y estadísticas. Los entrenadores pueden ver el progreso de sus clientes asignados. `M`

11. [ ] **Sistema de Metas Personales** — Funcionalidad para que clientes establezcan metas personales (peso, medidas, fuerza, resistencia) y realicen seguimiento de su progreso hacia esas metas. Incluye recordatorios y visualización de avance con indicadores de progreso. `S`

12. [ ] **Sistema de Reseñas y Calificaciones** — Plataforma para que clientes califiquen y reseñen gimnasios y entrenadores con puntuaciones numéricas y comentarios escritos. Incluye visualización de calificaciones promedio, filtrado de reseñas y moderación básica. Permite a entrenadores y administradores ver sus calificaciones. `M`

13. [ ] **Comunicación en Tiempo Real con WebSockets** — Implementación de SignalR para comunicación en tiempo real que permite actualizaciones instantáneas de disponibilidad de clases, confirmaciones de reservas, notificaciones de cancelaciones y mensajería entre usuarios. Mejora la experiencia de usuario con actualizaciones en vivo. `L`

14. [ ] **Sistema de Notificaciones Push** — Implementación completa de notificaciones push a dispositivos móviles para recordatorios de clases próximas, confirmaciones de reservas, actualizaciones de membresía, mensajes importantes de administradores y notificaciones de progreso. Incluye gestión de preferencias de notificaciones por usuario. `M`

15. [ ] **Reportes y Estadísticas para Administradores** — Herramientas de análisis y reportes que permiten a administradores visualizar métricas clave como asistencia a clases, uso de instalaciones, satisfacción de clientes (basada en calificaciones), rendimiento de entrenadores, ingresos por planes de membresía y tendencias de uso. Incluye exportación de reportes. `L`

> Notes
> - Order items by technical dependencies and product architecture
> - Each item should represent an end-to-end (frontend + backend) functional and testable feature
> - Features are ordered to build incrementally from foundational infrastructure (gyms, users, memberships) to advanced collaboration features (reviews, real-time communication)
> - MVP focus: Items 1-9 provide core functionality for basic gym management operations
> - Advanced features: Items 10-15 enhance user experience and provide advanced analytics
