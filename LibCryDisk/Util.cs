using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace LibCryDisk
{
    /// <summary>
    ///     Crypto Utils
    /// </summary>
    public static class Util
    {

        /// <summary>
        ///     Converts a SecureString back into a string.
        /// </summary>
        /// <param name="value">SecureString to convert</param>
        /// <returns>Plaintext</returns>
        public static string SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr) ?? throw new InvalidOperationException();
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

        /// <summary>
        ///     SHA256 hash text of a string
        /// </summary>
        /// <param name="value">The string to hash</param>
        /// <returns>The SHA256 hash text</returns>
        public static string sha256_hash(string value)
        {
            StringBuilder Sb = new();
            Encoding enc = Encoding.UTF8;
            byte[] result = SHA256.HashData(enc.GetBytes(value));

            foreach (byte b in result)
            {
                Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        /// <summary>
        ///     Converts a plaintext string into a SecureString
        /// </summary>
        /// <param name="password">plaintext</param>
        /// <returns>The SecureString</returns>
        /// <exception cref="ArgumentNullException">plaintext cannot be null</exception>
        public static SecureString ConvertToSecureString(string password)
        {
            ArgumentNullException.ThrowIfNull(password);

            SecureString securePassword = new SecureString();

            foreach (char c in password)
            {
                securePassword.AppendChar(c);
            }

            securePassword.MakeReadOnly();
            return securePassword;
        }
    }
}
