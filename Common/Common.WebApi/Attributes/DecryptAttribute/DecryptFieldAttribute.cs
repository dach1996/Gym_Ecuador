using System.Collections;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Common.PluginFactory.Interface;
using Common.Security.Interface;
using Common.Security.Model.Enum;
using Common.Utils.CustomExceptions;
using Common.Utils.Extensions;
using Common.WebApi.Models;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
namespace Common.WebApi.Attributes.DecryptAttribute;
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
public class DecryptFieldAttribute(IPluginFactory pluginFactory) : ActionFilterAttribute
{
    private readonly IRsaSecurity _rsaSecurity = pluginFactory.GetPlugin<IRsaSecurity>($"{RsaSecurityImplementation.ServerGeneral}", true);
    private static readonly ConcurrentDictionary<string, bool> _dictionaryControllerWithEncrypt = [];
    /// <summary>
    /// Action
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var existDecrypt = false;
        var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
        //Creamos un nombre único
        var endpointName = $"{descriptor.DisplayName}-{descriptor.ActionName}-{context.HttpContext.Request.Method}".ToSha256();
        //Guardar en memoria los paths
        if (_dictionaryControllerWithEncrypt.TryGetValue(endpointName, out var result))
        {
            if (result)
                SearchPropertys(context, existDecrypt);
        }
        else
        {
            existDecrypt = SearchPropertys(context, existDecrypt);
            _dictionaryControllerWithEncrypt.TryAdd(endpointName, existDecrypt);
        }
        base.OnActionExecuting(context);
    }

    private bool SearchPropertys(ActionExecutingContext context, bool existDecrypt)
    {
        foreach (var argument in context.ActionDescriptor.Parameters.Where(where => context.ActionArguments[where.Name] is not null).Select(select => context.ActionArguments[select.Name]))
            BrowseProperties(argument, ref existDecrypt);
        return existDecrypt;
    }



    /// <summary>
    /// Recorre las propiedades
    /// </summary>
    /// <param name="obj"></param>
    private void BrowseProperties(object obj, ref bool existDecrypt)
    {
        //Si es null retorna
        if (obj is null)
            return;
        var type = obj.GetType();
        //Recorre las propiedades que no sean null ni que sean excluiddos por primitivos o simples
        foreach (var property in type.GetProperties().Where(WhereFunc(obj)))
        {
            var propertyValue = property.GetValue(obj);
            if (property.PropertyType.Equals(typeof(string))
                && property.GetCustomAttributes(typeof(EncryptedFieldAttribute), false).Length != 0)
            {
                existDecrypt = DecryptStringField(obj, property, propertyValue);
                continue;
            }
            if (propertyValue is null)
                continue;
            existDecrypt = VerifyComplexObjectOrList(existDecrypt, property, propertyValue);
        }
    }

    /// <summary>
    /// Desencripta el string 
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="property"></param>
    /// <param name="propertyValue"></param>
    /// <returns></returns>
    private bool DecryptStringField(object obj, PropertyInfo property, object propertyValue)
    {
        bool existDecrypt = true;
        var stringPropertyValue = propertyValue as string;
        if (property.GetCustomAttributes(typeof(RequiredAttribute), false).Length != 0 && stringPropertyValue.IsNullOrEmpty())
            throw new ModelException($"No se pudo des-encriptar el campo: {property.Name}, está vacío y es obligatorio");
        if (stringPropertyValue.IsNullOrEmpty())
            return existDecrypt;
        var encryptedFieldAttribute = property.GetCustomAttributes(typeof(EncryptedFieldAttribute), false)[0] as EncryptedFieldAttribute;
        try
        {
            if (encryptedFieldAttribute.HasBase64)
                stringPropertyValue = stringPropertyValue.Decode();
            var propertyValueDecrypt = _rsaSecurity.Decrypt(stringPropertyValue);
            property.SetValue(obj, Convert.ChangeType(propertyValueDecrypt, property.PropertyType), null);
        }
        catch
        {
            throw new ModelException($"No se pudo des-encriptar el campo: {property.Name}");
        }
        return existDecrypt;
    }

    /// <summary>
    /// Verifica un objeto complejo o lista
    /// </summary>
    /// <param name="existDecrypt"></param>
    /// <param name="property"></param>
    /// <param name="propertyValue"></param>
    /// <returns></returns>
    private bool VerifyComplexObjectOrList(bool existDecrypt, PropertyInfo property, object propertyValue)
    {
        //Obtiene el tipo de valor de la propiedad a revisar
        // Si es una colección, iterar sobre sus elementos
        if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
            foreach (var element in (IEnumerable)propertyValue)
                BrowseProperties(element, ref existDecrypt);
        else
            // Si es un objeto complejo, llamar recursivamente
            BrowseProperties(propertyValue, ref existDecrypt);
        return existDecrypt;
    }

    /// <summary>
    /// Función Where
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    private static Func<PropertyInfo, bool> WhereFunc(object obj) => where => !IsSimpleType(where.PropertyType) && (where.PropertyType.Equals(typeof(string)) || where.GetValue(obj) is not null);

    /// <summary>
    /// List Exception
    /// </summary>
    /// <returns></returns>
    static readonly Type[] TypesExcludes = [
        typeof(IEnumerable<string>),
        typeof(decimal),
        typeof(DateTime),
        typeof(DateTimeOffset),
        typeof(TimeSpan),
        typeof(Guid),
        typeof(ContextRequest)
        ];



    /// <summary>
    /// Valida si es un tipo simple o excluido
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    private static bool IsSimpleType(Type type) => type.IsPrimitive || type.IsEnum || TypesExcludes.Contains(type);
}


[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
public class EncryptedFieldAttribute : Attribute
{
    /// <summary>
    /// Verifica si tiene base 64
    /// </summary>
    /// <value></value>
    public bool HasBase64 { get; set; }

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="hasBase64"></param>
    public EncryptedFieldAttribute(bool hasBase64 = true) => HasBase64 = hasBase64;
}


