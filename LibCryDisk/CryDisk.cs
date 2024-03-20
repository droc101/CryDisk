using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;

namespace LibCryDisk
{
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


        public static string Salt = "TempCryDiskSaltValue";

        byte[] SecurePassword;
        byte[] SecureIV;

        public CryDisk(string encryptedPath, char driveLetter)
        {
            EncryptedPath = encryptedPath;
            TempPlainPath = GetTempPlainPath();
            DriveLetter = driveLetter;
        }

        public CryDisk(string rawPlainPath, string encryptedPath, char driveLetter)
        {
            EncryptedPath = encryptedPath;
            TempPlainPath = GetTempPlainPath();
            File.Move(rawPlainPath, TempPlainPath); // Move the file to the temporary path (to be encrypted later)
            
            DriveLetter = driveLetter;
            
        }
        

        internal static bool IsDiskMounted(string EncPath)
        {
            FileInfo fi = new FileInfo(EncPath);
            string path = Path.GetTempPath() + Util.sha256_hash(fi.Name) + ".vhdx";
            return File.Exists(path);
        }

        public void LoadPassword(SecureString password)
        {
            using (Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(Util.SecureStringToString(password), Encoding.UTF8.GetBytes(Salt)))
            {
                SecurePassword = key.GetBytes(32);
                SecureIV = key.GetBytes(16);
            }
        }

        public void DestroyPassword()
        {
            SecurePassword = null; SecureIV = null;
        }

        string GetTempPlainPath()
        {
            FileInfo fi = new FileInfo(EncryptedPath);
            return Path.GetTempPath() + Util.sha256_hash(fi.Name) + ".vhdx";
        }

        public bool Mount()
        {
            try
            {
                Decrypt();
                LibVDisk.VDisk.MountVDisk(TempPlainPath, DriveLetter);
                return true;
            } catch (Exception e)
            {
                return false;
            }
        }

        public bool Unmount()
        {
            try {
                LibVDisk.VDisk.UnmountVDisk(TempPlainPath);
                Encrypt();
                return true;
            } catch (Exception e)
            {
                return false;
            }
        }

        public void Encrypt()
        {
            using (AesManaged aesAlg = new AesManaged())
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

        void Decrypt()
        {
            using (AesManaged aesAlg = new AesManaged())
            {
                // Set the encryption key and initialization vector (IV) from the password
                byte[] key = SecurePassword;
                byte[] iv = SecureIV;

                // Create a decryptor to perform the stream transform
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(key, iv);

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
