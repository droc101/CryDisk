﻿using LibVDisk;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace LibCryDisk
{
    /// <summary>
    ///     CryDisk instance
    /// </summary>
    public class CryDisk
    {
        /// <summary>
        ///     Raw password iv key
        ///     TODO: Protect them
        /// </summary>
        private byte[]? SecureIV;

        /// <summary>
        ///     Raw password key
        ///     TODO: Protect them
        /// </summary>
        private byte[]? SecurePassword;

        /// <summary>
        ///     Creates a CryDisk object from an existing encrypted disk.
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
        ///     Create a CryDisk object from an existing VHDX. The existing file will be DESTROYED.
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
        ///     The temporary path of the decrypted VHDX when it is mounted.
        /// </summary>
        public string TempPlainPath { get; set; }

        /// <summary>
        ///     The storage path of the encrypted VHDX.
        /// </summary>
        public string EncryptedPath { get; set; }

        /// <summary>
        ///     The drive letter to mount the VHDX to.
        /// </summary>
        public char DriveLetter { get; set; }

        /// <summary>
        ///     Salt/IV for encryption. If you use this lib, CHANGE IT.
        /// </summary>
        public static string Salt { get; set; } = "TempCryDiskSaltValue";

        /// <summary>
        ///     The key size to use for AES keys
        /// </summary>
        public static int Bits { get; set; } = 256;

        /// <summary>
        ///     The key size to use for AES IVs
        /// </summary>
        public static int IvBits { get; set; } = 128;

        /// <summary>
        ///     Checks if a CryDisk is unlocked
        /// </summary>
        /// <param name="EncPath">The path to the encrypted CryDisk</param>
        /// <returns>True if unlocked</returns>
        internal static bool IsDiskMounted(string EncPath)
        {
            FileInfo fi = new(EncPath);
            string path = Path.GetTempPath() + Util.sha256_hash(fi.Name) + ".vhdx";
            return File.Exists(path);
        }

        /// <summary>
        ///     Loads the password for this CryDisk
        /// </summary>
        /// <param name="password">The password</param>
        public void LoadPassword(SecureString password)
        {
            if (Bits % 8 != 0)
            {
                throw new InvalidOperationException("The AES bit width must be divisible by 8.");
            }
            if (IvBits % 8 != 0)
            {
                throw new InvalidOperationException("The AES IV bit width must be divisible by 8.");
            }
            string ps = Util.SecureStringToString(password);
            byte[] salt = Encoding.UTF8.GetBytes(Salt);
            using Rfc2898DeriveBytes key = new(ps, salt, 32, HashAlgorithmName.SHA512);
            SecurePassword = key.GetBytes(Bits / 8);
            SecureIV = key.GetBytes(IvBits / 8);
        }

        /// <summary>
        ///     Clear the password from this disk
        /// </summary>
        public void DestroyPassword()
        {
            SecurePassword = null;
            SecureIV = null;
            GC.Collect(); // scrub the memory
        }

        /// <summary>
        ///     Gets the temp file used for the unlocked vhdx
        /// </summary>
        /// <returns>The path</returns>
        private string GetTempPlainPath()
        {
            FileInfo fi = new(EncryptedPath);
            return Path.GetTempPath() + Util.sha256_hash(fi.Name) + ".vhdx";
        }

        /// <summary>
        ///     Unlocks and mounts the CryDisk
        /// </summary>
        /// <returns>True if success, otherwise false</returns>
        public bool Mount()
        {
            try
            {
                Decrypt();
                VDisk.DiskPartResult rs = VDisk.MountVDisk(TempPlainPath, DriveLetter);
                if (!rs.success)
                {
                    throw new Exception("VDisk failed to mount: " + rs.stdOut);
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to mount CryDisk: " + e.Message);
                return false;
            }
        }

        /// <summary>
        ///     Unmount and lock the CryDisk
        /// </summary>
        /// <returns>True if success, otherwise false</returns>
        public bool Unmount()
        {
            try
            {
                VDisk.DiskPartResult rs = VDisk.UnmountVDisk(TempPlainPath);
                if (!rs.success)
                {
                    if (!rs.stdOut.Contains("The virtual disk is already detached.")) // user may have ejected the vdisk
                    {
                        throw new Exception("VDisk failed to mount: " + rs.stdOut);
                    }
                }
                Encrypt();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to unmount CryDisk: " + e.Message);
                return false;
            }
        }

        /// <summary>
        ///     Locks the CryDisk (does NOT unmount)
        /// </summary>
        public void Encrypt()
        {
            ArgumentNullException.ThrowIfNull(SecurePassword);
            ArgumentNullException.ThrowIfNull(SecureIV);

            using (Aes aesAlg = Aes.Create())
            {
                // Set the encryption key and initialization vector (IV) from the password
                byte[] key = SecurePassword;
                byte[] iv = SecureIV;

                // Create an encryptor to perform the stream transform
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(key, iv);

                using FileStream inputFileStream = new(TempPlainPath, FileMode.Open);
                using FileStream outputFileStream = new(EncryptedPath, FileMode.Create);
                using CryptoStream cryptoStream = new(outputFileStream, encryptor, CryptoStreamMode.Write);
                int data;
                while ((data = inputFileStream.ReadByte()) != -1)
                {
                    cryptoStream.WriteByte((byte)data);
                }
            }
            File.Delete(TempPlainPath); // get rid of the temporary plain file
        }

        /// <summary>
        ///     Unlocks the CryDisk (does NOT mount)
        /// </summary>
        private void Decrypt()
        {
            ArgumentNullException.ThrowIfNull(SecurePassword);
            ArgumentNullException.ThrowIfNull(SecureIV);

            using Aes aesAlg = Aes.Create();

            // Create a decryptor to perform the stream transform
            ICryptoTransform decryptor = aesAlg.CreateDecryptor(SecurePassword, SecureIV);
            using FileStream inputFileStream = new(EncryptedPath, FileMode.Open);
            using FileStream outputFileStream = new(TempPlainPath, FileMode.Create);
            using CryptoStream cryptoStream = new(inputFileStream, decryptor, CryptoStreamMode.Read);
            int data = cryptoStream.ReadByte();
            while (data != -1)
            {
                outputFileStream.WriteByte((byte)data);
                data = cryptoStream.ReadByte();
            }
        }
    }
}
