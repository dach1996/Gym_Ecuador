using System.Security.Cryptography;
using System.Text;
using Common.Security.BlobException;
using Common.Security.Interface;
using Common.Security.Model.Configuration;
using Common.Security.Model.Configuration.File;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Common.Security.Implementation.Rsa;
public abstract class FileRsaSecurityBase : RsaSecurityBase
{
    protected readonly FileRsaSecurityConfiguration KeyConfiguration;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    protected FileRsaSecurityBase(ILogger<FileRsaSecurityBase> logger, IConfiguration configuration) : base(logger, configuration)
    {
        KeyConfiguration = configuration.GetSection(nameof(RsaSecurityConfiguration)).Get<RsaSecurityConfiguration<FileRsaSecurityConfiguration>>()
                ?.Implementations?.FirstOrDefault(where => where.Identifier == $"{RsaSecurityImplementationName}")?.Information
                 ?? throw new InvalidOperationException($"No se encontró la configuración de {nameof(IRsaSecurity)} con identificador: {RsaSecurityImplementationName}");
    }


    /// <summary>
    /// Obtiene la llave publica en formato PEM
    /// </summary>
    /// <returns></returns>
    public override string GetPublicKey()
    {
        if (string.IsNullOrEmpty(KeyConfiguration.PublicKeyBase64))
            throw new InvalidOperationException($"La llave Pública para el Identificador: {RsaSecurityImplementationName} no fué encontrada");
        return DecodeKey(KeyConfiguration.PublicKeyBase64);
    }

    /// <summary>
    /// Encripta
    /// </summary>
    /// <param name="plainText"></param>
    /// <returns></returns>
    public override string Encrypt(string plainText, RSAEncryptionPadding rSAEncryptionPadding = null)
    {
        try
        {
            rSAEncryptionPadding ??= RSAEncryptionPadding.OaepSHA1;
            if (string.IsNullOrEmpty(plainText))
                throw new CustomSecurityException("No hay texto para encriptar");
            var dataToEncrypt = Encoding.UTF8.GetBytes(plainText);
            using var rsa = new RSACryptoServiceProvider(2048);
            if (string.IsNullOrEmpty(KeyConfiguration.PublicKeyBase64))
                throw new CustomSecurityException($"La llave Pública para el Identificador: {RsaSecurityImplementationName} no fué encontrada");
            var key = DecodeKey(KeyConfiguration.PublicKeyBase64);
            rsa.ImportFromPem(key.ToCharArray());
            var dataEncripted = rsa.Encrypt(dataToEncrypt, rSAEncryptionPadding);
            return Convert.ToBase64String(dataEncripted);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error al intentar encriptar el texto: {@Text} con el Padding: {@Paggind} {@InnerException}", plainText, rSAEncryptionPadding, ex.InnerException?.Message);
            throw new CustomSecurityException($"Error mientras se intentaba encriptar texto");
        }
    }

    /// <summary>
    /// Desencripta
    /// </summary>
    /// <param name="encryptText"></param>
    /// <returns></returns>
    public override string Decrypt(string encryptText, RSAEncryptionPadding rSAEncryptionPadding = null)
    {
        try
        {
            rSAEncryptionPadding ??= RSAEncryptionPadding.OaepSHA1;
            if (string.IsNullOrEmpty(encryptText))
                throw new CustomSecurityException("No hay texto para des-encriptar");
            var dataToDecrypt = Convert.FromBase64String(encryptText);
            if (string.IsNullOrEmpty(KeyConfiguration.PrivateKeyBase64))
                throw new CustomSecurityException($"La llave Privada para el Identificador: {RsaSecurityImplementationName} no fué encontrada");
            var key = DecodeKey(KeyConfiguration.PrivateKeyBase64);
            using var rsa = new RSACryptoServiceProvider(2048);
            rsa.ImportFromPem(key.ToCharArray());
            var dataDecrypted = rsa.Decrypt(dataToDecrypt, rSAEncryptionPadding);
            return Encoding.UTF8.GetString(dataDecrypted);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error al intentar desencriptar el texto: {@Text} con el Padding: {@Paggind} {@InnerException}", encryptText, rSAEncryptionPadding, ex.InnerException?.Message);
            throw new CustomSecurityException($"Error mientras se intentaba desencriptar texto");
        }
    }


    /// <summary>
    /// Desencripta
    /// </summary>
    /// <param name="encryptText"></param>
    /// <returns></returns>
    public override bool VerifySign(string encryptText, string signText, HashAlgorithmName hashAlgorithmName, RSASignaturePadding rSAEncryptionPadding)
    {
        try
        {
            if (string.IsNullOrEmpty(KeyConfiguration.PublicKeyBase64))
                throw new CustomSecurityException($"La llave Privada para el Identificador: {RsaSecurityImplementationName} no fué encontrada");
            var key = DecodeKey(KeyConfiguration.PublicKeyBase64);
            using var rsa = new RSACryptoServiceProvider(2048);
            rsa.ImportFromPem(key.ToCharArray());
            return rsa.VerifyData(Encoding.UTF8.GetBytes(encryptText), Convert.FromBase64String(signText), hashAlgorithmName, rSAEncryptionPadding);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error Validando la Firma del Texto: {@EncrypText} con la Firma: {@Sign}", encryptText, signText);
            return false;
        }
    }

    /// <summary>
    /// Desencripta
    /// </summary>
    /// <param name="encryptText"></param>
    /// <returns></returns>
    public override string SignData(string data, HashAlgorithmName hashAlgorithmName, RSASignaturePadding rSAEncryptionPadding)
    {
        try
        {
            if (string.IsNullOrEmpty(KeyConfiguration.PrivateKeyBase64))
                throw new CustomSecurityException($"La llave Privada para el Identificador: {RsaSecurityImplementationName} no fué encontrada");
            var key = DecodeKey(KeyConfiguration.PrivateKeyBase64);
            using var rsa = new RSACryptoServiceProvider(2048);
            rsa.ImportFromPem(key.ToCharArray());
            var dataToSign = Encoding.UTF8.GetBytes(data);
            var signature = rsa.SignData(dataToSign, hashAlgorithmName, rSAEncryptionPadding);
            return Convert.ToBase64String(signature);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error al intentar firmar el texto: {@Text} con el Hash: {@Hash} y el padding :{@Padding} {@InnerException}", data, hashAlgorithmName, rSAEncryptionPadding, ex.InnerException?.Message);
            throw new CustomSecurityException($"Error mientras se intentaba firmar texto,");
        }
    }
}
