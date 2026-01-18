
using System.Runtime.Serialization;
using Common.WebApi.Attributes.EnumFunctionality;
using Common.WebApi.Models.Constants;

namespace Common.WebApi.Models.Enum;
/// <summary>
/// Operaciones para el Api de Administrador
/// </summary>
public enum OperationAdministratorName : short
{
    /// <summary>
    /// Iniciar sesión
    /// </summary>
    [EnumMember(Value = "Iniciar sesión")]
    [FunctionalityMember(FunctionalitiesCodes.LOGIN, registerAuditLog: true)]
    Login = 1,

    /// <summary>
    /// Obtener Clientes
    /// </summary>
    [EnumMember(Value = "Obtener Clientes")]
    [FunctionalityMember(FunctionalitiesCodes.GET_USER_CLIENTS_PAGINATED)]
    GetUserClientsPaginated = 9,

    /// <summary>
    /// Obtiene llave pública
    /// </summary>
    [EnumMember(Value = "Obtiene llave pública")]
    [FunctionalityMember(FunctionalitiesCodes.GET_PUBLIC_KEY)]
    GetPublicKey = 42,

    // ========== OPERACIONES DE GIMNASIO ==========

    /// <summary>
    /// Crear Gimnasio
    /// </summary>
    [EnumMember(Value = "Crear Gimnasio")]
    [FunctionalityMember(FunctionalitiesCodes.CREATE_GYM, registerAuditLog: true)]
    CreateGym = 71,

    /// <summary>
    /// Actualizar Gimnasio
    /// </summary>
    [EnumMember(Value = "Actualizar Gimnasio")]
    [FunctionalityMember(FunctionalitiesCodes.UPDATE_GYM, registerAuditLog: true)]
    UpdateGym = 72,

    /// <summary>
    /// Obtener Gimnasios Paginados
    /// </summary>
    [EnumMember(Value = "Obtener Gimnasios Paginados")]
    [FunctionalityMember(FunctionalitiesCodes.GET_GYMS_PAGINATED)]
    GetGymsPaginated = 73,

    /// <summary>
    /// Crear Sucursal de Gimnasio
    /// </summary>
    [EnumMember(Value = "Crear Sucursal de Gimnasio")]
    [FunctionalityMember(FunctionalitiesCodes.CREATE_GYM_BRANCH, registerAuditLog: true)]
    CreateGymBranch = 74,

    /// <summary>
    /// Actualizar Sucursal de Gimnasio
    /// </summary>
    [EnumMember(Value = "Actualizar Sucursal de Gimnasio")]
    [FunctionalityMember(FunctionalitiesCodes.UPDATE_GYM_BRANCH, registerAuditLog: true)]
    UpdateGymBranch = 75,

    /// <summary>
    /// Crear Horario de Sucursal
    /// </summary>
    [EnumMember(Value = "Crear Horario de Sucursal")]
    [FunctionalityMember(FunctionalitiesCodes.CREATE_GYM_BRANCH_SCHEDULE, registerAuditLog: true)]
    CreateGymBranchSchedule = 76,

    /// <summary>
    /// Actualizar Horario de Sucursal
    /// </summary>
    [EnumMember(Value = "Actualizar Horario de Sucursal")]
    [FunctionalityMember(FunctionalitiesCodes.UPDATE_GYM_BRANCH_SCHEDULE, registerAuditLog: true)]
    UpdateGymBranchSchedule = 77,

    /// <summary>
    /// Eliminar Horario de Sucursal
    /// </summary>
    [EnumMember(Value = "Eliminar Horario de Sucursal")]
    [FunctionalityMember(FunctionalitiesCodes.DELETE_GYM_BRANCH_SCHEDULE, registerAuditLog: true)]
    DeleteGymBranchSchedule = 78,

    /// <summary>
    /// Obtener Sucursales de Gimnasio Paginadas
    /// </summary>
    [EnumMember(Value = "Obtener Sucursales de Gimnasio Paginadas")]
    [FunctionalityMember(FunctionalitiesCodes.GET_GYM_BRANCHES_PAGINATED)]
    GetGymBranchesPaginated = 79,

    /// <summary>
    /// Obtener Detalle de Sucursal de Gimnasio por GUID
    /// </summary>
    [EnumMember(Value = "Obtener Detalle de Sucursal de Gimnasio por GUID")]
    [FunctionalityMember(FunctionalitiesCodes.GET_GYM_BRANCH_BY_GUID)]
    GetGymBranchByGuid = 80,

    /// <summary>
    /// Obtener Sucursales de Gimnasio por Funcionalidad
    /// </summary>
    [EnumMember(Value = "Obtener Sucursales de Gimnasio por Funcionalidad")]
    [FunctionalityMember(FunctionalitiesCodes.GET_GYM_BRANCHES_BY_FUNCTIONALITY)]
    GetGymBranchesByFunctionality = 116,

    // ========== OPERACIONES DE USUARIOS ADMINISTRADORES ==========

    /// <summary>
    /// Crear Usuario Administrador
    /// </summary>
    [EnumMember(Value = "Crear Usuario Administrador")]
    [FunctionalityMember(FunctionalitiesCodes.CREATE_USER_ADMINISTRATOR, registerAuditLog: true)]
    CreateUserAdministrator = 81,

    /// <summary>
    /// Eliminar Usuario Administrador
    /// </summary>
    [EnumMember(Value = "Eliminar Usuario Administrador")]
    [FunctionalityMember(FunctionalitiesCodes.DELETE_USER_ADMINISTRATOR, registerAuditLog: true)]
    DeleteUserAdministrator = 82,

    /// <summary>
    /// Obtener Usuarios Administradores Paginados
    /// </summary>
    [EnumMember(Value = "Obtener Usuarios Administradores Paginados")]
    [FunctionalityMember(FunctionalitiesCodes.GET_USERS_ADMINISTRATOR_PAGINATED)]
    GetUsersAdministratorPaginated = 83,

    /// <summary>
    /// Obtener Detalle de Usuario Administrador por GUID
    /// </summary>
    [EnumMember(Value = "Obtener Detalle de Usuario Administrador por GUID")]
    [FunctionalityMember(FunctionalitiesCodes.GET_USER_ADMINISTRATOR_BY_GUID)]
    GetUserAdministratorByGuid = 84,

    /// <summary>
    /// Crear Equipamiento
    /// </summary>
    [EnumMember(Value = "Crear Equipamiento")]
    [FunctionalityMember(FunctionalitiesCodes.CREATE_EQUIPMENT, registerAuditLog: true)]
    CreateEquipment = 85,

    /// <summary>
    /// Actualizar Equipamiento
    /// </summary>
    [EnumMember(Value = "Actualizar Equipamiento")]
    [FunctionalityMember(FunctionalitiesCodes.UPDATE_EQUIPMENT, registerAuditLog: true)]
    UpdateEquipment = 86,

    /// <summary>
    /// Eliminar Equipamiento
    /// </summary>
    [EnumMember(Value = "Eliminar Equipamiento")]
    [FunctionalityMember(FunctionalitiesCodes.DELETE_EQUIPMENT, registerAuditLog: true)]
    DeleteEquipment = 87,

    /// <summary>
    /// Obtener Equipamientos Paginados
    /// </summary>
    [EnumMember(Value = "Obtener Equipamientos Paginados")]
    [FunctionalityMember(FunctionalitiesCodes.GET_EQUIPMENTS_PAGINATED)]
    GetEquipmentsPaginated = 88,

    /// <summary>
    /// Obtener Detalle de Equipamiento por GUID
    /// </summary>
    [EnumMember(Value = "Obtener Detalle de Equipamiento por GUID")]
    [FunctionalityMember(FunctionalitiesCodes.GET_EQUIPMENT_BY_GUID)]
    GetEquipmentByGuid = 89,

    /// <summary>
    /// Enviar Notificación Push por UserGuids
    /// </summary>
    [EnumMember(Value = "Enviar Notificación Push por UserGuids")]
    [FunctionalityMember(FunctionalitiesCodes.SEND_NOTIFICATION_PUSH_BY_USER_GUIDS, registerAuditLog: true)]
    SendNotificationPushByUserGuids = 90,

    /// <summary>
    /// Obtener Notificaciones Push enviadas de manera paginada
    /// </summary>
    [EnumMember(Value = "Obtener Notificaciones Push enviadas de manera paginada")]
    [FunctionalityMember(FunctionalitiesCodes.GET_NOTIFICATION_PUSHES_PAGINATED)]
    GetNotificationPushesPaginated = 91,

    /// <summary>
    /// Crear Usuario Cliente
    /// </summary>
    [EnumMember(Value = "Crear Usuario Cliente")]
    [FunctionalityMember(FunctionalitiesCodes.CREATE_USER_CLIENT, registerAuditLog: true)]
    CreateUserClient = 92,

    /// <summary>
    /// Obtener Detalle de Usuario Cliente por GUID
    /// </summary>
    [EnumMember(Value = "Obtener Detalle de Usuario Cliente por GUID")]
    [FunctionalityMember(FunctionalitiesCodes.GET_USER_CLIENT_BY_GUID)]
    GetUserClientByGuid = 117,

    // ========== OPERACIONES DE PLANES DE SUCURSAL ==========

    /// <summary>
    /// Crear Plan de Sucursal
    /// </summary>
    [EnumMember(Value = "Crear Plan de Sucursal")]
    [FunctionalityMember(FunctionalitiesCodes.CREATE_BRANCH_PLAN, registerAuditLog: true)]
    CreateBranchPlan = 93,

    /// <summary>
    /// Actualizar Plan de Sucursal
    /// </summary>
    [EnumMember(Value = "Actualizar Plan de Sucursal")]
    [FunctionalityMember(FunctionalitiesCodes.UPDATE_BRANCH_PLAN, registerAuditLog: true)]
    UpdateBranchPlan = 94,

    /// <summary>
    /// Eliminar Plan de Sucursal
    /// </summary>
    [EnumMember(Value = "Eliminar Plan de Sucursal")]
    [FunctionalityMember(FunctionalitiesCodes.DELETE_BRANCH_PLAN, registerAuditLog: true)]
    DeleteBranchPlan = 95,

    /// <summary>
    /// Obtener Planes de Sucursal Paginados
    /// </summary>
    [EnumMember(Value = "Obtener Planes de Sucursal Paginados")]
    [FunctionalityMember(FunctionalitiesCodes.GET_BRANCH_PLANS_PAGINATED)]
    GetBranchPlansPaginated = 96,

    /// <summary>
    /// Obtener Detalle de Plan de Sucursal por GUID
    /// </summary>
    [EnumMember(Value = "Obtener Detalle de Plan de Sucursal por GUID")]
    [FunctionalityMember(FunctionalitiesCodes.GET_BRANCH_PLAN_BY_GUID)]
    GetBranchPlanByGuid = 97,

    // ========== OPERACIONES DE RUTINAS ==========

    /// <summary>
    /// Obtener rutinas creadas por el administrador
    /// </summary>
    [EnumMember(Value = "Obtener rutinas creadas por el administrador")]
    [FunctionalityMember(FunctionalitiesCodes.GET_ROUTINES_CREATED_BY_ADMIN)]
    GetRoutinesCreatedByAdmin = 98,

    /// <summary>
    /// Obtener ejercicios de rutina por GUID
    /// </summary>
    [EnumMember(Value = "Obtener ejercicios de rutina por GUID")]
    [FunctionalityMember(FunctionalitiesCodes.GET_ROUTINE_EXERCISES_BY_GUID)]
    GetRoutineExercisesByGuid = 99,

    /// <summary>
    /// Crear rutina con ejercicios para un usuario
    /// </summary>
    [EnumMember(Value = "Crear rutina con ejercicios para un usuario")]
    [FunctionalityMember(FunctionalitiesCodes.CREATE_ROUTINE_WITH_EXERCISES_FOR_USER, registerAuditLog: true)]
    CreateRoutineWithExercisesForUser = 100,

    // ========== OPERACIONES DE EJERCICIOS ==========

    /// <summary>
    /// Obtener ejercicios
    /// </summary>
    [EnumMember(Value = "Obtener ejercicios")]
    [FunctionalityMember(FunctionalitiesCodes.GET_EXERCISES)]
    GetExercises = 101,

    /// <summary>
    /// Obtener detalle de ejercicio por GUID
    /// </summary>
    [EnumMember(Value = "Obtener detalle de ejercicio por GUID")]
    [FunctionalityMember(FunctionalitiesCodes.GET_EXERCISE_BY_GUID)]
    GetExerciseByGuid = 102,

    /// <summary>
    /// Crear ejercicio
    /// </summary>
    [EnumMember(Value = "Crear ejercicio")]
    [FunctionalityMember(FunctionalitiesCodes.CREATE_EXERCISE, registerAuditLog: true)]
    CreateExercise = 103,

    /// <summary>
    /// Actualizar ejercicio
    /// </summary>
    [EnumMember(Value = "Actualizar ejercicio")]
    [FunctionalityMember(FunctionalitiesCodes.UPDATE_EXERCISE, registerAuditLog: true)]
    UpdateExercise = 104,

    /// <summary>
    /// Actualizar usuario administrador
    /// </summary>
    [EnumMember(Value = "Actualizar usuario administrador")]
    [FunctionalityMember(FunctionalitiesCodes.UPDATE_USER_ADMINISTRATOR, registerAuditLog: true)]
    UpdateUserAdministrator = 105,

    // ========== OPERACIONES DE ROLES Y FUNCIONALIDADES ==========

    /// <summary>
    /// Obtener todos los roles
    /// </summary>
    [EnumMember(Value = "Obtener todos los roles")]
    [FunctionalityMember(FunctionalitiesCodes.GET_ROLES)]
    GetRoles = 106,

    /// <summary>
    /// Obtener todas las funcionalidades
    /// </summary>
    [EnumMember(Value = "Obtener todas las funcionalidades")]
    [FunctionalityMember(FunctionalitiesCodes.GET_ALL_FUNCTIONALITIES)]
    GetFunctionalities = 107,

    /// <summary>
    /// Actualizar un rol
    /// </summary>
    [EnumMember(Value = "Actualizar un rol")]
    [FunctionalityMember(FunctionalitiesCodes.UPDATE_ROLE, registerAuditLog: true)]
    UpdateRole = 108,

    /// <summary>
    /// Crear un nuevo rol
    /// </summary>
    [EnumMember(Value = "Crear un nuevo rol")]
    [FunctionalityMember(FunctionalitiesCodes.CREATE_ROLE, registerAuditLog: true)]
    CreateRole = 109,

    /// <summary>
    /// Obtener detalle de rol por GUID
    /// </summary>
    [EnumMember(Value = "Obtener detalle de rol por GUID")]
    [FunctionalityMember(FunctionalitiesCodes.GET_ROLE_DETAIL)]
    GetRoleDetail = 110,

    /// <summary>
    /// Crear funcionalidad
    /// </summary>
    [EnumMember(Value = "Crear funcionalidad")]
    [FunctionalityMember(FunctionalitiesCodes.CREATE_FUNCTIONALITY, registerAuditLog: true)]
    CreateFunctionality = 111,

    /// <summary>
    /// Actualizar funcionalidad
    /// </summary>
    [EnumMember(Value = "Actualizar funcionalidad")]
    [FunctionalityMember(FunctionalitiesCodes.UPDATE_FUNCTIONALITY, registerAuditLog: true)]
    UpdateFunctionality = 112,

    /// <summary>
    /// Obtener todas las funciones
    /// </summary>
    [EnumMember(Value = "Obtener todas las funciones")]
    [FunctionalityMember(FunctionalitiesCodes.GET_FUNCTIONS)]
    GetFunctions = 113,

    /// <summary>
    /// Crear función
    /// </summary>
    [EnumMember(Value = "Crear función")]
    [FunctionalityMember(FunctionalitiesCodes.CREATE_FUNCTION, registerAuditLog: true)]
    CreateFunction = 114,

    /// <summary>
    /// Actualizar función
    /// </summary>
    [EnumMember(Value = "Actualizar función")]
    [FunctionalityMember(FunctionalitiesCodes.UPDATE_FUNCTION, registerAuditLog: true)]
    UpdateFunction = 115,
}
