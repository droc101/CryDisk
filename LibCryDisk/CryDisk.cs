//#define TRY_CATCH

using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace LibCryDisk
{
    /// <summary>
    /// CryDisk instance
    /// </summary>
    public class CryDisk
    {
        /// <summary>
        /// The temporary path of the decrypted VHDX when it is mounted.
        /// </summary>
        public string TempPlainPath { get; set; }

        /// <summary>
        /// The storage path of the encrypted VHDX.
        /// </summary>
        public string EncryptedPath { get; set; }

        /// <summary>
        /// The drive letter to mount the VHDX to.
        /// </summary>
        public char DriveLetter { get; set; }

        /// <summary>
        /// Salt/IV for encryption. If you use this lib, CHANGE IT.
        /// </summary>
        public static string Salt = "TempCryDiskSaltValue";

        /// <summary>
        /// Raw password key
        /// TODO: Protect them
        /// </summary>
        byte[]? SecurePassword;
        /// <summary>
        /// Raw password iv key
        /// TODO: Protect them
        /// </summary>
        byte[]? SecureIV;

        /// <summary>
        /// Creates a CryDisk object from an existing encrypted disk.
        /// </summary>
        /// <param name="encryptedPath">The path of the encrypted disk image</param>
        /// <param name="driveLetter">Drive letter to use when mounting</param>
        public CryDisk(string encryptedPath, char driveLetter)
        {
            EncryptedPath = encryptedPath;
            TempPlainPath = GetTempPlainPath();
            DriveLetter = driveLetter;
        }

        /// <summary>
        /// Create a CryDisk object from an existing VHDX. The existing file will be DESTROYED.
        /// </summary>
        /// <param name="rawPlainPath">Existing plaintext VHDX file</param>
        /// <param name="encryptedPath">The path to store the encrypted disk image</param>
        /// <param name="driveLetter">Drive letter to use when mounting</param>
        public CryDisk(string rawPlainPath, string encryptedPath, char driveLetter)
        {
            EncryptedPath = encryptedPath;
            TempPlainPath = GetTempPlainPath();
            File.Move(rawPlainPath, TempPlainPath); // Move the file to the temporary path (to be encrypted later)
            
            DriveLetter = driveLetter;
            
        }
        
        /// <summary>
        /// Checks if a CryDisk is unlocked
        /// </summary>
        /// <param name="EncPath">The path to the encrypted CryDisk</param>
        /// <returns>True if unlocked</returns>
        internal static bool IsDiskMounted(string EncPath)
        {
            FileInfo fi = new FileInfo(EncPath);
            string path = Path.GetTempPath() + Util.sha256_hash(fi.Name) + ".vhdx";
            return File.Exists(path);
        }

        /// <summary>
        /// Loads the password for this CryDisk
        /// </summary>
        /// <param name="password">The password</param>
        public void LoadPassword(SecureString password)
        {
            string ps = Util.SecureStringToString(password);
            byte[] salt = Encoding.UTF8.GetBytes(Salt);
            using (Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(ps, salt, 32, HashAlgorithmName.SHA512))
            {
                SecurePassword = key.GetBytes(32);
                SecureIV = key.GetBytes(16);
            }
        }

        /// <summary>
        /// Clear the password from this disk
        /// </summary>
        public void DestroyPassword()
        {
            SecurePassword = null; SecureIV = null;
        }

        /// <summary>
        /// Gets the temp file used for the unlocked vhdx
        /// </summary>
        /// <returns>The path</returns>
        string GetTempPlainPath()
        {
            FileInfo fi = new FileInfo(EncryptedPath);
            return Path.GetTempPath() + Util.sha256_hash(fi.Name) + ".vhdx";
        }

        /// <summary>
        /// Unlocks and mounts the CryDisk
        /// </summary>
        /// <returns>True if success, otherwise false</returns>
        public bool Mount()
        {
            try
            {
                Decrypt();
                var rs = LibVDisk.VDisk.MountVDisk(TempPlainPath, DriveLetter);
                if (!rs.success)
                {
                    throw new Exception("VDisk failed to mount: " + rs.stdOut);
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Unmount and lock the CryDisk
        /// </summary>
        /// <returns>True if success, otherwise false</returns>
        public bool Unmount()
        {
            try
            {
                var rs = LibVDisk.VDisk.UnmountVDisk(TempPlainPath);
                if (!rs.success)
                {
                    throw new Exception("VDisk failed to mount: " + rs.stdOut);
                }
                Encrypt();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Locks the CryDisk (does NOT unmount)
        /// </summary>
        public void Encrypt()
        {
            if (SecurePassword == null || SecureIV == null)
                throw new ArgumentNullException("You must LoadPassword before locking or unlocking");

            using (Aes aesAlg = Aes.Create())
            {
                // Set the encryption key and initialization vector (IV) from the password
                byte[] key = SecurePassword;
                byte[] iv = SecureIV;

                // Create an encryptor to perform the stream transform
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(key, iv);

                using (FileStream inputFileStream = new FileStream(TempPlainPath, FileMode.Open))
                using (FileStream outputFileStream = new FileStream(EncryptedPath, FileMode.Create))
                using (CryptoStream cryptoStream = new CryptoStream(outputFileStream, encryptor, CryptoStreamMode.Write))
                {
                    int data;
                    while ((data = inputFileStream.ReadByte()) != -1)
                    {
                        cryptoStream.WriteByte((byte)data);
                    }
                }
            }
            File.Delete(TempPlainPath); // get rid of the temporary plain file
        }

        /// <summary>
        /// Unlocks the CryDisk (does NOT mount)
        /// </summary>
        void Decrypt()
        {
            using (Aes aesAlg = Aes.Create())
            {

                // Create a decryptor to perform the stream transform
#pragma warning disable CS8604 // Possible null reference argument.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(SecurePassword, SecureIV);
#pragma warning restore CS8604 // Possible null reference argument.

                using (FileStream inputFileStream = new FileStream(EncryptedPath, FileMode.Open))
                using (FileStream outputFileStream = new FileStream(TempPlainPath, FileMode.Create))
                using (CryptoStream cryptoStream = new CryptoStream(inputFileStream, decryptor, CryptoStreamMode.Read))
                {
                    int data;
                    while ((data = cryptoStream.ReadByte()) != -1)
                    {
                        outputFileStream.WriteByte((byte)data);
                    }
                }
            }
        }
    }
}
