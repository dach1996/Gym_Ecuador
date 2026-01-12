namespace Common.WebApi.Models.Constants;
/// <summary>
///  Functionalities codes
/// </summary>
public static class FunctionalitiesCodes
{
    /// <summary>
    /// Login
    /// </summary>
    public const string LOGIN = "ADM_LOGIN";

    /// <summary>
    /// Get user clients paginated
    /// </summary>
    public const string GET_USER_CLIENTS_PAGINATED = "ADM_CLIENT_GET_PAGINATED";

    /// <summary>
    /// Get public key
    /// </summary>
    public const string GET_PUBLIC_KEY = "ADM_KEY_GET_PUBLIC";

    // ========== OPERACIONES DE GIMNASIO ==========

    /// <summary>
    /// Crear Gimnasio
    /// </summary>
    public const string CREATE_GYM = "ADM_GYM_CREATE";

    /// <summary>
    /// Actualizar Gimnasio
    /// </summary>
    public const string UPDATE_GYM = "ADM_GYM_UPDATE";

    /// <summary>
    /// Obtener Gimnasios Paginados
    /// </summary>
    public const string GET_GYMS_PAGINATED = "ADM_GYM_GET_PAGINATED";

    /// <summary>
    /// Crear Sucursal de Gimnasio
    /// </summary>
    public const string CREATE_GYM_BRANCH = "ADM_GYM_BRANCH_CREATE";

    /// <summary>
    /// Actualizar Sucursal de Gimnasio
    /// </summary>
    public const string UPDATE_GYM_BRANCH = "ADM_GYM_BRANCH_UPDATE";

    /// <summary>
    /// Crear Horario de Sucursal
    /// </summary>
    public const string CREATE_GYM_BRANCH_SCHEDULE = "ADM_GYM_BRANCH_SCH_CREATE";

    /// <summary>
    /// Actualizar Horario de Sucursal
    /// </summary>
    public const string UPDATE_GYM_BRANCH_SCHEDULE = "ADM_GYM_BRANCH_SCH_UPDATE";

    /// <summary>
    /// Eliminar Horario de Sucursal
    /// </summary>
    public const string DELETE_GYM_BRANCH_SCHEDULE = "ADM_GYM_BRANCH_SCH_DELETE";

    /// <summary>
    /// Obtener Sucursales de Gimnasio Paginadas
    /// </summary>
    public const string GET_GYM_BRANCHES_PAGINATED = "ADM_GYM_BRANCH_GET_PAGINATED";

    /// <summary>
    /// Obtener Detalle de Sucursal de Gimnasio por GUID
    /// </summary>
    public const string GET_GYM_BRANCH_BY_GUID = "ADM_GYM_BRANCH_GET_BY_GUID";

    // ========== OPERACIONES DE USUARIOS ADMINISTRADORES ==========

    /// <summary>
    /// Crear Usuario Administrador
    /// </summary>
    public const string CREATE_USER_ADMINISTRATOR = "ADM_USER_ADMIN_CREATE";

    /// <summary>
    /// Eliminar Usuario Administrador
    /// </summary>
    public const string DELETE_USER_ADMINISTRATOR = "ADM_USER_ADMIN_DELETE";

    /// <summary>
    /// Obtener Usuarios Administradores Paginados
    /// </summary>
    public const string GET_USERS_ADMINISTRATOR_PAGINATED = "ADM_USER_ADMIN_GET_PAGINATED";

    /// <summary>
    /// Obtener Detalle de Usuario Administrador por GUID
    /// </summary>
    public const string GET_USER_ADMINISTRATOR_BY_GUID = "ADM_USER_ADMIN_GET_BY_GUID";

    // ========== OPERACIONES DE EQUIPAMIENTOS ==========

    /// <summary>
    /// Crear Equipamiento
    /// </summary>
    public const string CREATE_EQUIPMENT = "ADM_EQP_CREATE";

    /// <summary>
    /// Actualizar Equipamiento
    /// </summary>
    public const string UPDATE_EQUIPMENT = "ADM_EQP_UPDATE";

    /// <summary>
    /// Eliminar Equipamiento
    /// </summary>
    public const string DELETE_EQUIPMENT = "ADM_EQP_DELETE";

    /// <summary>
    /// Obtener Equipamientos Paginados
    /// </summary>
    public const string GET_EQUIPMENTS_PAGINATED = "ADM_EQP_GET_PAGINATED";

    /// <summary>
    /// Obtener Detalle de Equipamiento por GUID
    /// </summary>
    public const string GET_EQUIPMENT_BY_GUID = "ADM_EQP_GET_BY_GUID";

    // ========== OPERACIONES DE NOTIFICACIONES PUSH ==========

    /// <summary>
    /// Enviar Notificación Push por UserGuids
    /// </summary>
    public const string SEND_NOTIFICATION_PUSH_BY_USER_GUIDS = "ADM_NOTIF_PUSH_SEND_BY_USER_GUIDS";

    /// <summary>
    /// Obtener Notificaciones Push enviadas de manera paginada
    /// </summary>
    public const string GET_NOTIFICATION_PUSHES_PAGINATED = "ADM_NOTIF_PUSH_GET_PAGINATED";

    /// <summary>
    /// Crear Usuario Cliente
    /// </summary>
    public const string CREATE_USER_CLIENT = "ADM_CLIENT_CREATE";

    // ========== OPERACIONES DE PLANES DE SUCURSAL ==========

    /// <summary>
    /// Crear Plan de Sucursal
    /// </summary>
    public const string CREATE_BRANCH_PLAN = "ADM_BRANCH_PLAN_CREATE";

    /// <summary>
    /// Actualizar Plan de Sucursal
    /// </summary>
    public const string UPDATE_BRANCH_PLAN = "ADM_BRANCH_PLAN_UPDATE";

    /// <summary>
    /// Eliminar Plan de Sucursal
    /// </summary>
    public const string DELETE_BRANCH_PLAN = "ADM_BRANCH_PLAN_DELETE";

    /// <summary>
    /// Obtener Planes de Sucursal Paginados
    /// </summary>
    public const string GET_BRANCH_PLANS_PAGINATED = "ADM_BRANCH_PLAN_GET_PAGINATED";

    /// <summary>
    /// Obtener Detalle de Plan de Sucursal por GUID
    /// </summary>
    public const string GET_BRANCH_PLAN_BY_GUID = "ADM_BRANCH_PLAN_GET_BY_GUID";

    // ========== OPERACIONES DE EJERCICIOS ==========

    /// <summary>
    /// Crear ejercicio
    /// </summary>
    public const string CREATE_EXERCISE = "ADM_EXERCISE_CREATE";

    /// <summary>
    /// Actualizar ejercicio
    /// </summary>
    public const string UPDATE_EXERCISE = "ADM_EXERCISE_UPDATE";

    /// <summary>
    /// Obtener ejercicios
    /// </summary>
    public const string GET_EXERCISES = "ADM_EXERCISE_GET";

    /// <summary>
    /// Obtener detalle de ejercicio por GUID
    /// </summary>
    public const string GET_EXERCISE_BY_GUID = "ADM_EXERCISE_GET_BY_GUID";

    // ========== OPERACIONES DE RUTINAS ==========

    /// <summary>
    /// Obtener rutinas creadas por el administrador
    /// </summary>
    public const string GET_ROUTINES_CREATED_BY_ADMIN = "ADM_ROUTINE_GET_CREATED_BY_ADMIN";

    /// <summary>
    /// Obtener ejercicios de rutina por GUID
    /// </summary>
    public const string GET_ROUTINE_EXERCISES_BY_GUID = "ADM_ROUTINE_EXERCISE_GET_BY_GUID";

    /// <summary>
    /// Crear rutina con ejercicios para un usuario
    /// </summary>
    public const string CREATE_ROUTINE_WITH_EXERCISES_FOR_USER = "ADM_ROUTINE_CREATE_WITH_EXERCISES";

    // ========== OPERACIONES DE ROLES Y FUNCIONALIDADES ==========

    /// <summary>
    /// Crear rol
    /// </summary>
    public const string CREATE_ROLE = "ROL_REGISTER";

    /// <summary>
    /// Actualizar rol
    /// </summary>
    public const string UPDATE_ROLE = "ROL_UPDATE";

    /// <summary>
    /// Obtener roles
    /// </summary>
    public const string GET_ROLES = "ADM_ROLE_GET";

    /// <summary>
    /// Obtener todas las funcionalidades
    /// </summary>
    public const string GET_ALL_FUNCTIONALITIES = "ADM_FUNC_GET_ALL";

    /// <summary>
    /// Actualizar usuario administrador
    /// </summary>
    public const string UPDATE_USER_ADMINISTRATOR = "ADM_USER_UPDATE";

    /// <summary>
    /// Obtener detalle de rol por GUID
    /// </summary>
    public const string GET_ROLE_DETAIL = "ADM_ROLE_GET_BY_GUID";

    /// <summary>
    /// Crear funcionalidad
    /// </summary>
    public const string CREATE_FUNCTIONALITY = "ADM_FUNC_CREATE";

    /// <summary>
    /// Actualizar funcionalidad
    /// </summary>
    public const string UPDATE_FUNCTIONALITY = "ADM_FUNC_UPDATE";

    /// <summary>
    /// Obtener todas las funciones
    /// </summary>
    public const string GET_FUNCTIONS = "ADM_FUNCTION_GET_ALL";

    /// <summary>
    /// Crear función
    /// </summary>
    public const string CREATE_FUNCTION = "ADM_FUNCTION_CREATE";

    /// <summary>
    /// Actualizar función
    /// </summary>
    public const string UPDATE_FUNCTION = "ADM_FUNCTION_UPDATE";
}