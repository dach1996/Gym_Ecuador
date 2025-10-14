
using System.Security.Cryptography;

namespace Common.Security.Interface;
public interface IRsaSecurity
{
    /// <summary>
    /// Obtiene la llave pública
    /// </summary>
    /// <param name="format"></param>
    /// <returns></returns>
    string GetPublicKey();

    /// <summary>
    /// Encripta un Texto
    /// </summary>
    /// <param name="plainText"></param>
    /// <param name="rSAEncryptionPadding"></param>
    /// <returns></returns>
    string Encrypt(string plainText, RSAEncryptionPadding rSAEncryptionPadding = null);

    /// <summary>
    /// Desencripta un Texto
    /// </summary>
    /// <param name="encryptText"></param>
    /// <param name="rSAEncryptionPadding"></param>
    /// <returns></returns>
    string Decrypt(string encryptText, RSAEncryptionPadding rSAEncryptionPadding = null);

    /// <summary>
    /// Verifica la Firma
    /// </summary>
    /// <param name="encryptText"></param>
    /// <param name="signText"></param>
    /// <param name="hashAlgorithmName"></param>
    /// <param name="rSAEncryptionPadding"></param>
    /// <returns></returns>
    bool VerifySign(string encryptText, string signText, HashAlgorithmName hashAlgorithmName, RSASignaturePadding rSAEncryptionPadding);

    /// <summary>
    /// Firma un texto
    /// </summary>
    /// <param name="data"></param>
    /// <param name="hashAlgorithmName"></param>
    /// <param name="rSAEncryptionPadding"></param>
    /// <returns></returns>
    string SignData(string data, HashAlgorithmName hashAlgorithmName, RSASignaturePadding rSAEncryptionPadding);
}
