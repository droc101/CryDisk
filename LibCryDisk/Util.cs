using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LibCryDisk
{
    /// <summary>
    /// Crypto Utils
    /// </summary>
    public static class Util
    {

        /// <summary>
        /// Converts a SecureString back into a string.
        /// </summary>
        /// <param name="value">SecureString to convert</param>
        /// <returns>Plaintext</returns>
        public static string SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
#pragma warning disable CS8603 // Possible null reference return.
                return Marshal.PtrToStringUni(valuePtr);
#pragma warning restore CS8603 // Possible null reference return.
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

        /// <summary>
        /// SHA256 hash text of a string
        /// </summary>
        /// <param name="value">The string to hash</param>
        /// <returns>The SHA256 hash text</returns>
        public static String sha256_hash(String value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        /// <summary>
        /// Converts a plaintext string into a SecureString
        /// </summary>
        /// <param name="password">plaintext</param>
        /// <returns>The SecureString</returns>
        /// <exception cref="ArgumentNullException">plaintext cannot be null</exception>
        public static SecureString ConvertToSecureString(string password)
        {
            if (password == null)
                throw new ArgumentNullException("password");

            var securePassword = new SecureString();

            foreach (char c in password)
                securePassword.AppendChar(c);

            securePassword.MakeReadOnly();
            return securePassword;
        }

    }
}
