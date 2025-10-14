using System.Runtime.Serialization;

namespace Common.WebCommon.Models.Enum;

/// <summary>
/// Plataformas soportadas por el Gateway API
/// </summary>
public enum Platform
{
    /// <summary>
    /// Android
    /// </summary>
    [EnumMember(Value = "Android")]
    Android = 1,

    /// <summary>
    /// iOS
    /// </summary>
    [EnumMember(Value = "Ios")]
    iOS = 2,

    /// <summary>
    /// Web Browser
    /// </summary>
    [EnumMember(Value = "Web")]
    Web = 3
}

