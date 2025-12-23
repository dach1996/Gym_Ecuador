
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
    /// Selección de Establecimiento a Manejar
    /// </summary>
    [EnumMember(Value = "Selección de Establecimiento a Manejar")]
    [FunctionalityMember(FunctionalitiesCodes.SELECT_ESTABLISHMENT, registerAuditLog: true)]
    SelectedEstablishment = 2,

    /// <summary>
    /// Actualizar Producto
    /// </summary>
    [EnumMember(Value = "Actualizar Producto")]
    [FunctionalityMember(FunctionalitiesCodes.UPDATE_PRODUCT, registerAuditLog: true)]
    UpdateProduct = 3,

    /// <summary>
    /// Eliminar Producto
    /// </summary>
    [EnumMember(Value = "Eliminar Producto")]
    [FunctionalityMember(FunctionalitiesCodes.DELETE_PRODUCT, registerAuditLog: true)]
    DeleteProduct = 4,

    /// <summary>
    /// Agregar Producto a Sucursal
    /// </summary>
    [EnumMember(Value = "Agregar Producto a Sucursal")]
    [FunctionalityMember(FunctionalitiesCodes.INSERT_PRODUCT_TO_ESTABLISHMENT_BRANCH, registerAuditLog: true)]
    InsertProductToEstablishmentBranch = 5,

    /// <summary>
    /// Obtener Productos paginados por Establecimiento
    /// </summary>
    [EnumMember(Value = "Obtener Productos paginados por Establecimiento")]
    [FunctionalityMember(FunctionalitiesCodes.GET_PRODUCTS_BY_ESTABLISHMENT_BRANCH_PAGINATED)]
    GetProductsByEstablishmentBranchPaginated = 6,

    /// <summary>
    /// Obtener Producto por Id
    /// </summary>
    [EnumMember(Value = "Obtener Producto por Id")]
    [FunctionalityMember(FunctionalitiesCodes.GET_PRODUCT_DETAILS_BY_ID)]
    GetProductDetailsById = 7,

    /// <summary>
    /// Actualizar Información de Cliente
    /// </summary>
    [EnumMember(Value = "Actualizar Información de Cliente")]
    [FunctionalityMember(FunctionalitiesCodes.UPDATE_USER_CLIENT_INFORMATION, registerAuditLog: true)]
    UpdateUserClientInformation = 8,

    /// <summary>
    /// Obtener Clientes
    /// </summary>
    [EnumMember(Value = "Obtener Clientes")]
    [FunctionalityMember(FunctionalitiesCodes.GET_USER_CLIENTS_PAGINATED)]
    GetUserClientsPaginated = 9,

    /// <summary>
    /// Crear Persona
    /// </summary>
    [EnumMember(Value = "Crear Persona")]
    [FunctionalityMember(FunctionalitiesCodes.CREATE_PERSON, registerAuditLog: true)]
    CreatePerson = 10,

    /// <summary>
    /// Actualizar Persona
    /// </summary>
    [EnumMember(Value = "Actualizar Persona")]
    [FunctionalityMember(FunctionalitiesCodes.UPDATE_PERSON, registerAuditLog: true)]
    UpdatePerson = 11,

    /// <summary>
    /// Obtener detalles de Persona
    /// </summary>
    [EnumMember(Value = "Obtener detalles de Persona")]
    [FunctionalityMember(FunctionalitiesCodes.GET_PERSON_DETAILS)]
    GetPersonDetails = 12,

    /// <summary>
    /// Obtener Personas Paginadas
    /// </summary>
    [EnumMember(Value = "Obtener Personas Paginadas")]
    [FunctionalityMember(FunctionalitiesCodes.GET_PERSONS_PAGINATED)]
    GetPersonsPaginated = 13,

    /// <summary>
    /// Actualizar Mascota
    /// </summary>
    [EnumMember(Value = "Actualizar Mascota")]
    [FunctionalityMember(FunctionalitiesCodes.UPDATE_PET, registerAuditLog: true)]
    UpdatePet = 14,

    /// <summary>
    /// Obtener Mascotas Paginadas
    /// </summary>
    [EnumMember(Value = "Obtener Mascotas Paginadas")]
    [FunctionalityMember(FunctionalitiesCodes.GET_PETS_PAGINATED)]
    GetPetsPaginated = 15,

    /// <summary>
    /// Obtener Detalle de Mascota
    /// </summary>
    [EnumMember(Value = "Obtener Detalle de Mascota")]
    [FunctionalityMember(FunctionalitiesCodes.GET_PET_DETAILS)]
    GetPetDetails = 16,

    /// <summary>
    /// Crear Mascota
    /// </summary>
    [EnumMember(Value = "Crear Mascota")]
    [FunctionalityMember(FunctionalitiesCodes.CREATE_PET, registerAuditLog: true)]
    CreatePet = 17,

    /// <summary>
    /// Obtiene catálogo de Archivo
    /// </summary>
    [EnumMember(Value = "Obtiene catálogo de Archivo")]
    [FunctionalityMember(FunctionalitiesCodes.GET_CATALOGUES_FILE)]
    GetCataloguesFile = 18,

    /// <summary>
    /// Envía un código de Asignación
    /// </summary>
    [EnumMember(Value = "Envía un código de Asignación")]
    [FunctionalityMember(FunctionalitiesCodes.SEND_ASSIGNMENT_USER_CODE, registerAuditLog: true)]
    SendAssignmentUserCode = 19,

    /// <summary>
    /// Registro de Historial Médico
    /// </summary>
    [EnumMember(Value = "Registro de Historial Médico")]
    [FunctionalityMember(FunctionalitiesCodes.CREATE_MEDICAL_HISTORY, registerAuditLog: true)]
    CreateMedicalHistory = 20,

    /// <summary>
    /// Obtiene detalle de Historial Médico
    /// </summary>
    [EnumMember(Value = "Obtiene detalle de Historial Médico")]
    [FunctionalityMember(FunctionalitiesCodes.GET_MEDICAL_HISTORY_DETAILS)]
    GetMedicalHistoryDetails = 21,

    /// <summary>
    /// Obtiene Historial Médico Paginado
    /// </summary>
    [EnumMember(Value = "Obtiene Historial Médico de Mascotas")]
    [FunctionalityMember(FunctionalitiesCodes.GET_PET_MEDICAL_HISTORY)]
    GetPetMedicalHistory = 22,

    /// <summary>
    /// Actualiza un Historial Médico
    /// </summary>
    [EnumMember(Value = "Actualiza un Historial Médico")]
    [FunctionalityMember(FunctionalitiesCodes.UPDATE_MEDICAL_HISTORY, registerAuditLog: true)]
    UpdateMedicalHistory = 23,

    /// <summary>
    /// Obtiene detalle de Vacuna
    /// </summary>
    [EnumMember(Value = "Obtiene detalle de Vacuna")]
    [FunctionalityMember(FunctionalitiesCodes.GET_VACCINE_DETAILS)]
    GetVaccineDetails = 24,

    /// <summary>
    /// Crear Vacuna
    /// </summary>
    [EnumMember(Value = "Crear Vacuna")]
    [FunctionalityMember(FunctionalitiesCodes.CREATE_VACCINE, registerAuditLog: true)]
    CreateVaccine = 25,

    /// <summary>
    /// Obtiene Vacunas Paginadas
    /// </summary>
    [EnumMember(Value = "Obtiene Vacunas Paginadas")]
    [FunctionalityMember(FunctionalitiesCodes.GET_VACCINES_PAGINATED)]
    GetVaccinesPaginated = 26,

    /// <summary>
    /// Actualizar Vacuna
    /// </summary>
    [EnumMember(Value = "Actualizar Vacuna")]
    [FunctionalityMember(FunctionalitiesCodes.UPDATE_VACCINE, registerAuditLog: true)]
    UpdateVaccine = 27,

    /// <summary>
    /// Eliminar Vacuna
    /// </summary>
    [EnumMember(Value = "Eliminar Vacuna")]
    [FunctionalityMember(FunctionalitiesCodes.DELETE_VACCINE, registerAuditLog: true)]
    DeleteVaccine = 28,

    /// <summary>
    /// Obtiene detalle de Vacuna de Mascota
    /// </summary>
    [EnumMember(Value = "Obtiene detalle de Vacuna de Mascota")]
    [FunctionalityMember(FunctionalitiesCodes.GET_VACCINE_PET_DETAILS)]
    GetVaccinePetDetails = 29,

    /// <summary>
    /// Crear Vacuna de Mascota
    /// </summary>
    [EnumMember(Value = "Crear Vacuna de Mascota")]
    [FunctionalityMember(FunctionalitiesCodes.CREATE_VACCINE_PET, registerAuditLog: true)]
    CreateVaccinePet = 30,

    /// <summary>
    /// Obtiene Vacunas de Mascota Paginadas
    /// </summary>
    [EnumMember(Value = "Obtiene Vacunas de Mascota Paginadas")]
    [FunctionalityMember(FunctionalitiesCodes.GET_VACCINE_PETS_PAGINATED)]
    GetVaccinePetsPaginated = 31,

    /// <summary>
    /// Actualizar Vacuna de Mascota
    /// </summary>
    [EnumMember(Value = "Actualizar Vacuna de Mascota")]
    [FunctionalityMember(FunctionalitiesCodes.UPDATE_VACCINE_PET, registerAuditLog: true)]
    UpdateVaccinePet = 32,

    /// <summary>
    /// Eliminar Vacuna de Mascota
    /// </summary>
    [EnumMember(Value = "Eliminar Vacuna de Mascota")]
    [FunctionalityMember(FunctionalitiesCodes.DELETE_VACCINE_PET, registerAuditLog: true)]
    DeleteVaccinePet = 33,

    /// <summary>
    /// Obtiene detalle de Proveedor
    /// </summary>
    [EnumMember(Value = "Obtiene detalle de Proveedor")]
    [FunctionalityMember(FunctionalitiesCodes.GET_SUPPLIER_DETAILS)]
    GetSupplierDetails = 34,

    /// <summary>
    /// Crear Proveedor
    /// </summary>
    [EnumMember(Value = "Crear Proveedor")]
    [FunctionalityMember(FunctionalitiesCodes.CREATE_SUPPLIER, registerAuditLog: true)]
    CreateSupplier = 35,

    /// <summary>
    /// Obtiene Proveedores Paginados
    /// </summary>
    [EnumMember(Value = "Obtiene Proveedores Paginados")]
    [FunctionalityMember(FunctionalitiesCodes.GET_SUPPLIERS_PAGINATED)]
    GetSuppliersPaginated = 36,

    /// <summary>
    /// Actualizar Proveedor
    /// </summary>
    [EnumMember(Value = "Actualizar Proveedor")]
    [FunctionalityMember(FunctionalitiesCodes.UPDATE_SUPPLIER, registerAuditLog: true)]
    UpdateSupplier = 37,

    /// <summary>
    /// Eliminar Proveedor
    /// </summary>
    [EnumMember(Value = "Eliminar Proveedor")]
    [FunctionalityMember(FunctionalitiesCodes.DELETE_SUPPLIER, registerAuditLog: true)]
    DeleteSupplier = 38,

    /// <summary>
    /// Crear Tipo de Vacuna
    /// </summary>
    [EnumMember(Value = "Crear Tipo de Vacuna")]
    [FunctionalityMember(FunctionalitiesCodes.CREATE_TYPE_VACCINE, registerAuditLog: true)]
    CreateTypeVaccine = 39,

    /// <summary>
    /// Eliminar Tipo de Vacuna
    /// </summary>
    [EnumMember(Value = "Eliminar Tipo de Vacuna")]
    [FunctionalityMember(FunctionalitiesCodes.DELETE_TYPE_VACCINE, registerAuditLog: true)]
    DeleteTypeVaccine = 40,

    /// <summary>
    /// Obtiene Tipos de Vacuna Paginados
    /// </summary>
    [EnumMember(Value = "Obtiene Tipos de Vacuna Paginados")]
    [FunctionalityMember(FunctionalitiesCodes.GET_TYPE_VACCINES_PAGINATED)]
    GetTypeVaccinesPaginated = 41,

    /// <summary>
    /// Obtiene llave pública
    /// </summary>
    [EnumMember(Value = "Obtiene llave pública")]
    [FunctionalityMember(FunctionalitiesCodes.GET_PUBLIC_KEY)]
    GetPublicKey = 42,

    /// <summary>
    /// Obtiene los roles
    /// </summary>
    [EnumMember(Value = "Obtiene los roles")]
    [FunctionalityMember(FunctionalitiesCodes.GET_ROLES)]
    GetRoles = 43,

    /// <summary>
    /// Crea un rol
    /// </summary>
    [EnumMember(Value = "Crea un rol")]
    [FunctionalityMember(FunctionalitiesCodes.CREATE_ROLE, registerAuditLog: true)]
    CreateRole = 44,

    /// <summary>
    /// Elimina un rol
    /// </summary>
    [EnumMember(Value = "Elimina un rol")]
    [FunctionalityMember(FunctionalitiesCodes.DELETE_ROLE, registerAuditLog: true)]
    DeleteRole = 45,

    /// <summary>
    /// Obtiene todas las funcionalidades
    /// </summary>
    [EnumMember(Value = "Obtiene todas las funcionalidades")]
    [FunctionalityMember(FunctionalitiesCodes.GET_ALL_FUNCTIONALITIES)]
    GetAllFunctionalities = 46,

    /// <summary>
    /// Actualiza un rol
    /// </summary>
    [EnumMember(Value = "Actualiza un rol")]
    [FunctionalityMember(FunctionalitiesCodes.UPDATE_ROLE, registerAuditLog: true)]
    UpdateRole = 47,

    /// <summary>
    /// Obtiene los detalles de una sucursal
    /// </summary>
    [EnumMember(Value = "Obtiene los detalles de una sucursal")]
    [FunctionalityMember(FunctionalitiesCodes.GET_ESTABLISHMENT_BRANCH_DETAILS)]
    GetEstablishmentBranchDetails = 48,

    /// <summary>
    /// Obtiene la información del establecimiento
    /// </summary>
    [EnumMember(Value = "Obtiene la información del establecimiento")]
    [FunctionalityMember(FunctionalitiesCodes.GET_ESTABLISHMENT_INFORMATION)]
    GetEstablishmentInformation = 49,

    /// <summary>
    /// Actualiza información de un establecimiento
    /// </summary>
    [EnumMember(Value = "Actualiza información de un establecimiento")]
    [FunctionalityMember(FunctionalitiesCodes.EDIT_ESTABLISHMENT_INFORMATION, registerAuditLog: true)]
    UpdateEstablishment = 50,

    /// <summary>
    /// Obtiene las sucursales paginadas
    /// </summary>
    [EnumMember(Value = "Obtiene las sucursales paginadas")]
    [FunctionalityMember(FunctionalitiesCodes.GET_ESTABLISHMENT_BRANCH_PAGINATED)]
    GetEstablishmentBranchPaginated = 51,

    /// <summary>
    /// Elimina una sucursal
    /// </summary>
    [EnumMember(Value = "Elimina una sucursal")]
    [FunctionalityMember(FunctionalitiesCodes.DELETE_ESTABLISHMENT_BRANCH, registerAuditLog: true)]
    DeleteEstablishmentBranch = 52,

    /// <summary>
    /// Obtiene el catálogo relacional
    /// </summary>
    [EnumMember(Value = "Obtiene el catálogo relacional")]
    [FunctionalityMember(FunctionalitiesCodes.GET_RELATIONAL_CATALOGUE)]
    GetRelationalCatalogue = 53,

    /// <summary>
    /// Obtiene los detalles de un usuario administrador
    /// </summary>
    [EnumMember(Value = "Obtiene los detalles de un usuario administrador")]
    [FunctionalityMember(FunctionalitiesCodes.GET_USER_ADMINISTRATOR_DETAILS)]
    GetUserAdministratorDetails = 54,

    /// <summary>
    /// Obtiene los usuarios administradores paginados
    /// </summary>
    [EnumMember(Value = "Obtiene los usuarios administradores paginados")]
    [FunctionalityMember(FunctionalitiesCodes.GET_USER_ADMINISTRATOR_PAGINATED)]
    GetUserAdministratorPaginated = 55,

    /// <summary>
    /// Actualiza un usuario administrador
    /// </summary>
    [EnumMember(Value = "Actualiza un usuario administrador")]
    [FunctionalityMember(FunctionalitiesCodes.UPDATE_USER_ADMINISTRATOR, registerAuditLog: true)]
    UpdateUserAdministrator = 56,

    /// <summary>
    ///  Asigna un usuario administrador a una sucursal
    /// </summary>
    [EnumMember(Value = "Asigna un usuario administrador a una sucursal")]
    [FunctionalityMember(FunctionalitiesCodes.ASSIGN_USER_ADMINISTRATOR_IN_ESTABLISHMENT_BRANCH, registerAuditLog: true)]
    AssignUserAdministratorInEstablishmentBranchRequest = 57,

    /// <summary>
    ///   Elimina un usuario administrador de una sucursal
    /// </summary>
    [EnumMember(Value = "Elimina un usuario administrador de una sucursal")]
    [FunctionalityMember(FunctionalitiesCodes.REMOVE_USER_ADMINISTRATOR_IN_ESTABLISHMENT_BRANCH, registerAuditLog: true)]
    RemoveUserAdministratorInEstablishmentBranchRequest = 58,

    /// <summary>
    /// Obtiene usuarios administradores asignados a una sucursal
    /// </summary>
    [EnumMember(Value = "Obtiene usuarios administradores asignados a una sucursal")]
    [FunctionalityMember(FunctionalitiesCodes.GET_USER_ADMINISTRATOR_ESTABLISHMENT_BRANCH)]
    GetUserAdministratorEstablishmentBranchRequest = 59,

    /// <summary>
    /// Obtiene los roles por establecimiento
    /// </summary>
    [EnumMember(Value = "Obtiene los roles por establecimiento")]
    [FunctionalityMember(FunctionalitiesCodes.GET_ROLES_BY_ESTABLISHMENT)]
    GetRolesByEstablishment = 60,

    /// <summary>
    /// Obtiene los roles de usuarios administradores por sucursal
    /// </summary>
    [EnumMember(Value = "Obtiene los roles de usuarios administradores por sucursal")]
    [FunctionalityMember(FunctionalitiesCodes.GET_USER_ADMINISTRATOR_ROLES_BY_ESTABLISHMENT_BRANCH)]
    GetUserAdministratorRolesByEstablishmentBranch = 61,

    /// <summary>
    /// Obtiene los detalles de una imagen
    /// </summary>
    [EnumMember(Value = "Obtiene los detalles de una imagen")]
    [FunctionalityMember(FunctionalitiesCodes.DETAILS_IMAGE)]
    DetailsImage = 62,

    /// <summary>
    /// Actualiza una imagen
    /// </summary>
    [EnumMember(Value = "Actualiza una imagen")]
    [FunctionalityMember(FunctionalitiesCodes.UPDATE_IMAGE, registerAuditLog: true)]
    UpdateImage = 63,

    /// <summary>
    /// Obtiene los logs paginados
    /// </summary>
    [EnumMember(Value = "Obtiene los logs paginados por establecimiento")]
    [FunctionalityMember(FunctionalitiesCodes.GET_LOGS_BY_ESTABLISHMENT)]
    GetLogsByEstablishmentPaginated = 64,

    /// <summary>
    /// Obtiene los logs paginados por sucursal
    /// </summary>
    [EnumMember(Value = "Obtiene los logs paginados por sucursal")]
    [FunctionalityMember(FunctionalitiesCodes.GET_LOGS_BY_ESTABLISHMENT_BRANCH)]
    GetLogsByEstablishmentBranchPaginated = 65,

    /// <summary>
    /// Obtiene los datos para el contexto de la página de logs
    /// </summary>
    [EnumMember(Value = "Obtiene los datos para el contexto de la página de logs")]
    [FunctionalityMember(FunctionalitiesCodes.GET_LOG_AUDIT_PAGE_CONTEXT)]
    GetLogAuditPageContext = 66,

    /// <summary>
    /// Obtiene los detalles de un log
    /// </summary>
    [EnumMember(Value = "Obtiene los detalles de un log")]
    [FunctionalityMember(FunctionalitiesCodes.GET_LOG_DETAILS)]
    GetLogDetails = 67,

    /// <summary>
    /// Actualiza una sucursal
    /// </summary>
    [EnumMember(Value = "Actualiza Información de Sucursal")]
    [FunctionalityMember(FunctionalitiesCodes.UPDATE_ESTABLISHMENT_BRANCH)]
    UpdateEstablishmentBranch = 68,

    /// <summary>
    /// Registrar una sucursal
    /// </summary>
    [EnumMember(Value = "Registra nueva de Sucursal")]
    [FunctionalityMember(FunctionalitiesCodes.REGISTER_ESTABLISHMENT_BRANCH)]
    CreateEstablishmentBranch = 69,

    /// <summary>
    /// Obtiene las visitas médicas paginadas
    /// </summary>
    [EnumMember(Value = "Obtiene las visitas médicas paginadas")]
    [FunctionalityMember(FunctionalitiesCodes.GET_PET_MEDICAL_HISTORY)]
    GetMedicalVisitsPaginated = 70,

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
    /// Crear Sucursal de Gimnasio
    /// </summary>
    [EnumMember(Value = "Crear Sucursal de Gimnasio")]
    [FunctionalityMember(FunctionalitiesCodes.CREATE_GYM_BRANCH, registerAuditLog: true)]
    CreateGymBranch = 73,

    /// <summary>
    /// Crear Horario de Sucursal
    /// </summary>
    [EnumMember(Value = "Crear Horario de Sucursal")]
    [FunctionalityMember(FunctionalitiesCodes.CREATE_GYM_BRANCH_SCHEDULE, registerAuditLog: true)]
    CreateGymBranchSchedule = 74,

    /// <summary>
    /// Actualizar Horario de Sucursal
    /// </summary>
    [EnumMember(Value = "Actualizar Horario de Sucursal")]
    [FunctionalityMember(FunctionalitiesCodes.UPDATE_GYM_BRANCH_SCHEDULE, registerAuditLog: true)]
    UpdateGymBranchSchedule = 75,

    /// <summary>
    /// Eliminar Horario de Sucursal
    /// </summary>
    [EnumMember(Value = "Eliminar Horario de Sucursal")]
    [FunctionalityMember(FunctionalitiesCodes.DELETE_GYM_BRANCH_SCHEDULE, registerAuditLog: true)]
    DeleteGymBranchSchedule = 76
}