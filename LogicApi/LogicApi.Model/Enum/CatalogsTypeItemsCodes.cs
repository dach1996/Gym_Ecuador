using System.Runtime.Serialization;
namespace LogicApi.Model.Enum;
/// <summary>
/// Códigos para catálogos de Archivo
/// </summary>
public enum CatalogsTypeItemsCodes
{
    /// <summary>
    /// Tipos de Documento
    /// </summary>
    /// <value></value>
    [EnumMember(Value = "DOCUMENT_TYPE")]
    DocumentType,

    /// <summary>
    /// Catálogo de Nacionalidades
    /// </summary>
    /// <value></value>
    [EnumMember(Value = "NATIONALITY")]
    Nationality,
}
