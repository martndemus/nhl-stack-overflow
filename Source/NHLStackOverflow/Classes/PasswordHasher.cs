using System;
using System.Text;
using System.Security.Cryptography;

namespace NHLStackOverflow.Classes
{
    public static class PasswordHasher
    {
        public static string Hash(string password)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            byte[] hash = encoder.GetBytes(password);
            SHA256 hasher = new SHA256Managed();

            // Hash the password 1000x with SHA256
            for (int i = 0; i < 1000; i++)
            {
                hash = hasher.ComputeHash(hash);
            }

            // Return as BASE64 string
            return Convert.ToBase64String(hash);
        }
    }
}