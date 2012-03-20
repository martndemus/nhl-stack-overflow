using System;
using System.Security.Cryptography;
using System.Text;

namespace NHLStackOverflow.Classes
{
	public static class PassLostHasher
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

            // Hash the password with SHA512
            hash = hasher.ComputeHash(hash);

            // Return as BASE64 string
            HTMLSanitizer hs = new HTMLSanitizer();
            
            string dummy = hs.UrlEncode(Convert.ToBase64String(hash));
            return dummy.Replace('%', '1');
        }

	}
}