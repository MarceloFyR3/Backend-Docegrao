using System;
using System.Security.Cryptography;
using System.Text;

namespace DoceGrao.Api.Shared.Utilits
{
    public static class Hash
    {
        private static readonly int saltLengthLimit = 32;
        private static readonly string frase = "O SENHOR é o meu pastor, nada me faltará.";

        public static string Get_HASH_SHA512(string password, string username, byte[] salt)
        {
            try
            {
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(password + username);

                byte[] plainTextWithSaltBytes = new byte[plainTextBytes.Length + salt.Length];

                for (int i = 0; i < plainTextBytes.Length; i++)
                {
                    plainTextWithSaltBytes[i] = plainTextBytes[i];
                }

                for (int i = 0; i < salt.Length; i++)
                {
                    plainTextWithSaltBytes[plainTextBytes.Length + i] = salt[i];
                }

                HashAlgorithm hash = new SHA512Managed();
                byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);
                byte[] hashWithSaltBytes = new byte[hashBytes.Length + salt.Length];

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    hashWithSaltBytes[i] = hashBytes[i];
                }

                for (int i = 0; i < salt.Length; i++)
                {
                    hashWithSaltBytes[hashBytes.Length + i] = salt[i];
                }

                return Convert.ToBase64String(hashWithSaltBytes);
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string Get_HASH_SHA512New(string password, string username, byte[] salt)
        {
            try
            {
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(frase.Substring(0, (frase.Length / 2)) + password + frase.Substring((frase.Length / 2)) + username);

                byte[] plainTextWithSaltBytes = new byte[plainTextBytes.Length + salt.Length];

                for (int i = 0; i < plainTextBytes.Length; i++)
                {
                    plainTextWithSaltBytes[i] = plainTextBytes[i];
                }

                for (int i = 0; i < salt.Length; i++)
                {
                    plainTextWithSaltBytes[plainTextBytes.Length + i] = salt[i];
                }

                HashAlgorithm hash = new SHA512Managed();
                byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);
                byte[] hashWithSaltBytes = new byte[hashBytes.Length + salt.Length];

                for (int i = 0; i < hashBytes.Length; i++)
                {
                    hashWithSaltBytes[i] = hashBytes[i];
                }

                for (int i = 0; i < salt.Length; i++)
                {
                    hashWithSaltBytes[hashBytes.Length + i] = salt[i];
                }

                return Convert.ToBase64String(hashWithSaltBytes);
            }
            catch
            {
                return string.Empty;
            }
        }

        public static byte[] Get_SALT()
        {
            return Get_SALT(saltLengthLimit);
        }

        private static byte[] Get_SALT(int maximumSaltLength)
        {
            byte[] salt = new byte[maximumSaltLength];

            using (RNGCryptoServiceProvider random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }

            return salt;
        }

        public static bool CompareHashValue(string password, string username, string userPassword, byte[] salt)//, bool atualizada)
        {
            try
            {
                //string expectedHashString = atualizada ? Get_HASH_SHA512New(password, username, SALT) : Get_HASH_SHA512(password, username, SALT);
                string actualHash = Get_HASH_SHA512(password, username, salt);

                return (userPassword == actualHash);
            }
            catch
            {
                return false;
            }
        }
    }
}
