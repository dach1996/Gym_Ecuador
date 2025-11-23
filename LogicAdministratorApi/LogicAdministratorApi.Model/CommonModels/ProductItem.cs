namespace LogicAdministratorApi.Model.CommonModels;

/// <summary>
/// Item de Producto
/// </summary>
public class ProductItem
{
    /// <summary>
    /// Id 
    /// </summary>
    public Guid Guid { get; set; }

    /// <summary>
    /// Código de producto
    /// </summary>
    /// <value></value>
    public string Code { get; set; }

    /// <summary>
    /// Descripción
    /// </summary>
    /// <value></value>
    public string Description { get; set; }

    /// <summary>
    /// Nombre 
    /// </summary>
    /// <value></value>
    public string Name { get; set; }

    /// <summary>
    /// Tiene eliminado
    /// </summary>
    /// <value></value>
    public bool HasDelete { get; set; }

    /// <summary>
    /// Imágenes
    /// </summary>
    /// <value></value>
    public IEnumerable<string> Images { get; set; }

}