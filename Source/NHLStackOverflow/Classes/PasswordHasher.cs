using System;
using System.Text;
using System.Security.Cryptography;

namespace NHLStackOverflow.Classes
{
    /// <summary>
    /// Hashes your string, a thousand times over and then spits it out in Base64 awesomeness
    /// </summary>
    public static class PasswordHasher
    {
        public static string Hash(string password)
        {
            // Can't encrypt empty strings
            if (password == "" || password == null)
                return "";

            // Create the encoder && hasher
            UTF8Encoding encoder = new UTF8Encoding();
            byte[] hash = encoder.GetBytes(password);
            SHA512 hasher = new SHA512Managed();

            // Hash the password 1000x with SHA512
            for (int i = 0; i < 1365; i++)
            {
                hash = hasher.ComputeHash(hash);
            }

            // Return as BASE64 string
            return Convert.ToBase64String(hash);
        }
    }
}