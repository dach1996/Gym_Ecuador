namespace LogicAdministratorApi.Model.CommonRecords;
/// <summary>
/// Modelo para Secreto de Login
/// </summary>
/// <param name="UserGuid"></param>
/// <param name="DateTimeRegister"></param>
/// <returns></returns>
public record SecretLoginModel(Guid UserGuid, DateTime DateTimeRegister);

