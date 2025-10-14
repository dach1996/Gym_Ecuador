using System.Security.Cryptography;
using System.Text;
using Common.Security.Interface;
using Common.Security.Model.Enum;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Common.Security.Implementation.Rsa;
public abstract class RsaSecurityBase(ILogger<RsaSecurityBase> logger, IConfiguration configuration) : IRsaSecurity
{
    protected abstract RsaSecurityImplementation RsaSecurityImplementationName { get; }
    protected readonly ILogger<RsaSecurityBase> Logger = logger;
    protected readonly IConfiguration Configuration = configuration;

    /// <summary>
    /// Obtiene la llave publica en formato PEM
    /// </summary>
    /// <returns></returns>
    public abstract string GetPublicKey();

    /// <summary>
    /// Encripta
    /// </summary>
    /// <param name="plainText"></param>
    /// <returns></returns>
    public abstract string Encrypt(string plainText, RSAEncryptionPadding rSAEncryptionPadding = null);

    /// <summary>
    /// Desencripta
    /// </summary>
    /// <param name="encryptText"></param>
    /// <returns></returns>
    public abstract string Decrypt(string encryptText, RSAEncryptionPadding rSAEncryptionPadding = null);

    /// <summary>
    /// Verifica la Firma
    /// </summary>
    /// <param name="encryptText"></param>
    /// <returns></returns>
    public abstract bool VerifySign(string encryptText, string signText, HashAlgorithmName hashAlgorithmName, RSASignaturePadding rSAEncryptionPadding);

    /// <summary>
    /// Firma
    /// </summary>
    /// <param name="encryptText"></param>
    /// <returns></returns>
    public abstract string SignData(string data, HashAlgorithmName hashAlgorithmName, RSASignaturePadding rSAEncryptionPadding);

    /// <summary>
    /// Decodifica la Llave
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    protected string DecodeKey(string key)
    {
        if (string.IsNullOrEmpty(key))
            throw new ArgumentNullException(nameof(key), "Llave vacía");
        try
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(key));
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "No se pudo decodificar la llave Configurada para Implementación: {@Implementation}", RsaSecurityImplementationName);
            throw new InvalidOperationException($"No se pudo decodificar la llave Configurada para Implementación: {RsaSecurityImplementationName}");
        }
    }
}