using System;
using System.Security.Cryptography;
using System.Text;

namespace ReportForge.Data
{
    /// <summary>
    /// AES-256-CBC wrapper for secrets stored in the database. Key comes from
    /// SECRET_KEY in mssql.env. Anyone holding mssql.env can still decrypt.
    /// </summary>
    internal static class Secret
    {
        private const int IvSize = 16;

        private static byte[] _key;
        private static byte[] Key
        {
            get
            {
                if (_key != null) return _key;
                var raw = Convert.FromBase64String(EnvFile.Get("SECRET_KEY"));
                if (raw.Length != 32)
                    throw new InvalidOperationException("SECRET_KEY must decode to 32 bytes for AES-256.");
                _key = raw;
                return _key;
            }
        }

        public static string Protect(string plaintext)
        {
            if (string.IsNullOrEmpty(plaintext)) return string.Empty;

            using (var aes = Aes.Create())
            {
                aes.Key = Key;
                aes.GenerateIV();

                using (var encryptor = aes.CreateEncryptor())
                {
                    var bytes = Encoding.UTF8.GetBytes(plaintext);
                    var cipher = encryptor.TransformFinalBlock(bytes, 0, bytes.Length);
                    var output = new byte[aes.IV.Length + cipher.Length];

                    Buffer.BlockCopy(aes.IV, 0, output, 0, aes.IV.Length);
                    Buffer.BlockCopy(cipher, 0, output, aes.IV.Length, cipher.Length);

                    return Convert.ToBase64String(output);
                }
            }
        }

        public static string Unprotect(string ciphertext)
        {
            if (string.IsNullOrEmpty(ciphertext)) return string.Empty;

            var input = Convert.FromBase64String(ciphertext);
            using (var aes = Aes.Create())
            {
                aes.Key = Key;

                var iv = new byte[IvSize];
                Buffer.BlockCopy(input, 0, iv, 0, IvSize);
                aes.IV = iv;

                using (var decryptor = aes.CreateDecryptor())
                {
                    return Encoding.UTF8.GetString(
                        decryptor.TransformFinalBlock(input, IvSize, input.Length - IvSize));
                }
            }
        }

        public static bool TryUnprotect(string ciphertext, out string plaintext)
        {
            try
            {
                plaintext = Unprotect(ciphertext);
                return true;
            }
            catch (FormatException)
            {
                plaintext = null;
                return false;
            }
            catch (CryptographicException)
            {
                plaintext = null;
                return false;
            }
            catch (ArgumentException)
            {
                plaintext = null;
                return false;
            }
        }
    }
}
