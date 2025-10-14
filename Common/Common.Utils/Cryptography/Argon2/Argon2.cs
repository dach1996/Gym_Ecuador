using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace Common.Utils.Cryptography.Argon2;

public static class Argon2
{

    /// <summary>
    /// Genera un hash de contraseña usando Argon2
    /// </summary>
    /// <param name="password">Contraseña a hashear</param>
    /// <param name="salt">Salt para el hash</param>
    /// <param name="degreeOfParallelism">Número de hilos (1-4)</param>
    /// <returns>Hash de contraseña</returns>
    public static string GenerateHash(
        string password,
        string salt,
        Argon2Options options = null
    )
    {
        options ??= Argon2Options.Generic;
        // Configurar parámetros de Argon2 (ajustables según tu hardware)
        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = Convert.FromBase64String(salt),
            DegreeOfParallelism = options.DegreeOfParallelism,
            MemorySize = options.MemorySize,
            Iterations = options.Iterations
        };
        // Generar hash (tamaño recomendado: 32 bytes)
        var hashBytes = argon2.GetBytes(options.HashLength);
        // Convertir a Base64 para almacenamiento
        return Convert.ToBase64String(hashBytes);
    }

    /// <summary>
    /// Genera un secreto aleatorio de la longitud especificada
    /// </summary>
    /// <param name="length">Longitud del secreto</param>
    /// <returns>Secreto aleatorio</returns>
    public static string GenerateRandomSecretBytes(int length = 16)
    {
        // Generar un salt aleatorio (16 bytes recomendado)
        var salt = new byte[length];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        return Convert.ToBase64String(salt);
    }

    /// <summary>
    /// Verifica si el texto plano coincide con el hash almacenado
    /// </summary>
    /// <param name="textPlain">Texto plano</param>
    /// <param name="hash">Hash almacenado</param>
    /// <param name="salt">Salt almacenado</param>
    /// <param name="options">Opciones de Argon2</param>
    /// <returns>True si el texto plano coincide con el hash, false en caso contrario</returns>
    public static bool Verify(
        string textPlain,
        string hash,
        string salt,
        Argon2Options options = default
    )
    {
        options ??= Argon2Options.Generic;
        // Convertir el salt y hash almacenados desde Base64
        var saltBytes = Convert.FromBase64String(salt);
        var hashBytes = Convert.FromBase64String(hash);
        // Configurar Argon2 con los mismos parámetros usados al crear el hash
        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(textPlain))
        {
            Salt = saltBytes,
            DegreeOfParallelism = options.DegreeOfParallelism,
            MemorySize = options.MemorySize,
            Iterations = options.Iterations
        };
        var inputHash = argon2.GetBytes(options.HashLength);
        // Comparar los hashes (a prueba de ataques de tiempo)
        return inputHash.SequenceEqual(hashBytes);
    }

    /// <summary>
    /// Verifica si el texto plano coincide con el hash almacenado
    /// </summary>
    /// <param name="textPlain">Texto plano</param>
    /// <param name="hash">Hash almacenado</param>
    /// <param name="salt">Salt almacenado</param>
    /// <param name="options">Opciones de Argon2</param>
    /// <returns>True si el texto plano coincide con el hash, false en caso contrario</returns>
    public static bool VerifyTexts(
        IEnumerable<string> textPlains,
        string hash,
        string salt,
        Argon2Options options = default
    ) => textPlains.Any(item => Verify(item, hash, salt, options));

    /// <summary>
    /// Verifica si el texto plano coincide con el hash almacenado
    /// </summary>
    /// <param name="textPlain">Texto plano</param>
    /// <param name="hash">Hash almacenado</param>
    /// <param name="salt">Salt almacenado</param>
    /// <param name="options">Opciones de Argon2</param>
    /// <returns>True si el texto plano coincide con el hash, false en caso contrario</returns>
    public static bool VerifyHashes(
        string textPlain,
        IEnumerable<string> hashes,
        string salt,
        Argon2Options options = default
    ) => hashes.Any(hash => Verify(textPlain, hash, salt, options));


    /// <summary>
    /// Opciones para el hash de Argon2
    /// </summary>
    public class Argon2Options(
        int degreeOfParallelism,
        int memorySize,
        int iterations,
        int hashLength
    )
    {
        /// <summary>
        /// Opciones genéricas
        /// </summary>
        /// <returns></returns>
        public static readonly Argon2Options Generic = new(2, 65536, 4, 32);

        /// <summary>
        /// Grado de paralelismo (1-4)
        /// </summary>
        public int DegreeOfParallelism { get; set; } = degreeOfParallelism;

        /// <summary>
        /// Tamaño de memoria (65536 bytes)
        /// </summary>
        public int MemorySize { get; set; } = memorySize;
        /// <summary>
        /// Iteraciones
        /// </summary>
        public int Iterations { get; set; } = iterations;

        /// <summary>
        /// Longitud del hash
        /// </summary>
        public int HashLength { get; set; } = hashLength;
    }

}