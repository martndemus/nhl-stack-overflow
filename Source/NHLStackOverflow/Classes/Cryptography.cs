using System;
using System.Security.Cryptography;
using System.Text;

namespace NHLStackOverflow.Classes
{
	public static class Cryptography
	{
        private static SHA512 shaHasher;
        private static MD5 md5Hasher;
        private static UTF8Encoding encoder;

        // Initialize the static class
        static Cryptography()
        {
            shaHasher = new SHA512Managed();
            md5Hasher = new MD5CryptoServiceProvider();
            encoder = new UTF8Encoding();
        }

        /// <summary>
        /// Hashes a string that is url encoded as output.
        /// </summary>
        /// <param name="s">Input string</param>
        /// <returns>Hashed string with url encoding</returns>
        public static string UrlHash(string s)
        {
            // Can't encrypt empty strings
            if (s == null)
                return "";

            // Tansform the string into a byte array
            byte[] hash = encoder.GetBytes(s);

            // Hash the password with SHA512
            hash = shaHasher.ComputeHash(hash);

            // Return as BASE64 string
            HTMLSanitizer hs = new HTMLSanitizer();
            
            string dummy = hs.UrlEncode(Convert.ToBase64String(hash));
            return dummy.Replace('%', '1');
        }

        /// <summary>
        /// Hashes a 'password' for secure storage
        /// </summary>
        /// <param name="password">String containing password</param>
        /// <returns>Hashed password</returns>
        public static string PasswordHash(string password)
        {
            // Can't encrypt empty strings
            if (password == "" || password == null)
                return "";

            // Tansform the string into a byte array
            byte[] hash = encoder.GetBytes(password);

            // Hash the password 1000x with SHA512
            for (int i = 0; i < 1365; i++)
            {
                hash = shaHasher.ComputeHash(hash);
            }

            // Return as BASE64 string
            return Convert.ToBase64String(hash);
        }

        /// <summary>
        /// Computes the MD5 hash from the input string
        /// </summary>
        /// <param name="s">Input string</param>
        /// <returns>MD5 hashed string</returns>
        public static string MD5Hash(string s)
        {
            if (s == null)
                s = string.Empty;

            // Cast string to byte array
            byte[] hash = encoder.GetBytes(s);

            // Hash with md5
            hash = md5Hasher.ComputeHash(hash);

            // Return as string.
            return Convert.ToString(hash);
        }

        /// <summary>
        /// Hashes an email adress to the spec for Gravatar
        /// </summary>
        /// <param name="s">Email adress as string</param>
        /// <returns>Hashed email</returns>
        public static string GravatarHash(string s)
        {
            // Lowercase the email address & trim whitespace
            s = s.Trim().ToLower();           

            // Hash it
            s = MD5Hash(s);

            return s;
        }

	}
}