using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace eClaim.Shared.Security
{
  public static  class Cryptography
    {
        private static int _iterations = 2;
        private static int _keySize = 256;

        private static string _hash = "SHA1";
        private static string _salt = "aselrias38490a32"; // Random
        private static string _vector = "8947az34awl34kjq"; // Random

        public static string Encrypt(string value, string password)
        {
            return Encrypt<AesManaged>(value, password);
        }
        public static string Encrypt<T>(string value, string password)
                where T : SymmetricAlgorithm, new()
        {
            byte[] vectorBytes = ASCIIEncoding.ASCII.GetBytes(_vector);
            byte[] saltBytes = ASCIIEncoding.ASCII.GetBytes(_salt);
            byte[] valueBytes = ASCIIEncoding.ASCII.GetBytes(value);

            byte[] encrypted;
            using (T cipher = new T())
            {
                PasswordDeriveBytes _passwordBytes =
                    new PasswordDeriveBytes(password, saltBytes, _hash, _iterations);
                byte[] keyBytes = _passwordBytes.GetBytes(_keySize / 8);

                cipher.Mode = CipherMode.CBC;

                using (ICryptoTransform encryptor = cipher.CreateEncryptor(keyBytes, vectorBytes))
                {
                    using (MemoryStream to = new MemoryStream())
                    {
                        using (CryptoStream writer = new CryptoStream(to, encryptor, CryptoStreamMode.Write))
                        {
                            writer.Write(valueBytes, 0, valueBytes.Length);
                            writer.FlushFinalBlock();
                            encrypted = to.ToArray();
                        }
                    }
                }
                cipher.Clear();
            }
            return Convert.ToBase64String(encrypted);
        }

      
    }
}
