namespace Common.ArtificialIntelligence.JsonIgnore;

/// <summary>
/// Atributo para ignorar información sensible en el Log
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
internal class IgnoreSensitveAttribute : Attribute
{
}