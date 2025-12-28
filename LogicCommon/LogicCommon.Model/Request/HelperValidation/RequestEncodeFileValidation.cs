using Common.Utils.Extensions;
using LogicCommon.Model.Request.File;

namespace LogicCommon.Model.Request.HelperValidation;
/// <summary>
/// Validación de RequestEncodeFile
/// </summary>
public static class RequestEncodeFileValidation
{
    /// <summary>
    /// Valida la lista de RequestEncodeFile
    /// </summary>
    /// <param name="requestEncodeFiles"></param>
    public static void Validate(this List<RequestEncodeFile> requestEncodeFiles)
    {
        requestEncodeFiles.GroupBy(group => group.Action).ToList().ForEach(group =>
        {
            switch (group.Key)
            {
                case ActionFile.Create:
                    if (group.Any(select => select.FileExtension.IsNullOrEmpty()
                       || select.FileName.IsNullOrEmpty() || select.EncodeContent.IsNullOrEmpty()))
                        throw new InvalidOperationException("El archivo no tiene una extensión o contenido válido");
                    break;
                case ActionFile.Delete:
                    if (group.Any(select => select.Guid is null || select.Guid == Guid.Empty))
                        throw new InvalidOperationException("El archivo no tiene un guid válido");
                    break;
            }
        });
    }
}