using System.Security.Cryptography;
using System.Text;
using Common.Utils.CustomExceptions;

namespace Common.Utils.Extensions;

public static class AesExtension
{
    /// <summary>
    /// Encrypta con AES
    /// </summary>
    /// <param name="plainText"></param>
    /// <param name="key"></param>
    /// <param name="iv"></param>
    /// <returns></returns>
    public static string EncryptAesToHex(this string plainText, string key, string iv)
    {
        using var aes = Aes.Create();
        var encryptor = aes.CreateEncryptor(Encoding.Default.GetBytes(key), Encoding.Default.GetBytes(iv));
        using MemoryStream ms = new();
        using CryptoStream cs = new(ms, encryptor, CryptoStreamMode.Write);
        using (StreamWriter sw = new(cs))
            sw.Write(plainText);
        var encrypted = ms.ToArray();
        return encrypted.ToHexString();
    }

    /// <summary>
    /// Encripta información con AES
    /// </summary>
    /// <param name="plainText"></param>
    /// <param name="key"></param>
    /// <param name="iv"></param>
    /// <returns></returns>
    public static string EncryptAes(this string plainText, string key, string iv = null)
    {
        //Validaciones
        NullException.ThrowIfNullOrEmpty(plainText, nameof(plainText), nameof(EncryptAes));
        NullException.ThrowIfNullOrEmpty(key, nameof(key), nameof(EncryptAes));
        ModelException.ThrowIfLengthIsLessThan(key, 15, nameof(key));
        using var aesAlg = Aes.Create();
        aesAlg.Key = Encoding.UTF8.GetBytes(key);
        if (!iv.IsNullOrEmpty())
        {
            ModelException.ThrowIfLengthIsLessThan(iv, 15, nameof(iv));
            aesAlg.IV = Encoding.UTF8.GetBytes(iv);
        }
        aesAlg.Mode = CipherMode.ECB;
        aesAlg.Padding = PaddingMode.PKCS7;
        //Creamos el encriptro
        var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
        using var msEncrypt = new MemoryStream();
        using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
        using (var swEncrypt = new StreamWriter(csEncrypt))
        {
            swEncrypt.Write(plainText);
        }
        return Convert.ToBase64String(msEncrypt.ToArray());
    }

    /// <summary>
    /// Desencripta Información con AES
    /// </summary>
    /// <param name="dataToDecrypt"></param>
    /// <param name="key"></param>
    /// <param name="iv"></param>
    /// <returns></returns>
    public static string DecryptAes(this string dataToDecrypt, string key, string iv = null)
    {
        //Validaciones
        NullException.ThrowIfNullOrEmpty(dataToDecrypt, nameof(dataToDecrypt), nameof(DecryptAes));
        NullException.ThrowIfNullOrEmpty(key, nameof(key), nameof(DecryptAes));
        ModelException.ThrowIfLengthIsLessThan(key, 15, nameof(key));
        try
        {
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key);
            if (!iv.IsNullOrEmpty())
            {
                ModelException.ThrowIfLengthIsLessThan(iv, 15, nameof(iv));
                aes.IV = Encoding.UTF8.GetBytes(iv);
            }
            aes.Mode = CipherMode.ECB;
            aes.Padding = PaddingMode.PKCS7;
            var descriptor = aes.CreateDecryptor(aes.Key, aes.IV);
            var buffer = Convert.FromBase64String(dataToDecrypt);
            using var memoryStream = new MemoryStream(buffer);
            using var cryptoStream = new CryptoStream(memoryStream, descriptor, CryptoStreamMode.Read);
            using var streamReader = new StreamReader(cryptoStream);
            return streamReader.ReadToEnd();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error al Desencriptar con AES el Texto: {dataToDecrypt} - {ex.Message}", ex);
        }
    }
}