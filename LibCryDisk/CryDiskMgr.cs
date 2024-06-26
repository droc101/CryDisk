﻿using LibVDisk;
using System.Security;

namespace LibCryDisk
{
    /// <summary>
    ///     CryDisk Manager
    /// </summary>
    public static class CryDiskMgr
    {

        /// <summary>
        ///     Check if a CryDisk is unlocked
        /// </summary>
        /// <param name="EncPath">The encrypted CryDisk</param>
        /// <returns>True if unlocked</returns>
        public static bool IsDiskMounted(string EncPath)
        {
            return CryDisk.IsDiskMounted(EncPath);
        }

        /// <summary>
        ///     Creates a new CryDisk. Does not unlock or mount it.
        /// </summary>
        /// <param name="size">The size of the CryDisk in MB</param>
        /// <param name="encPath">The path to use for the encrypted CryDisk</param>
        /// <param name="driveLetter">Unused</param>
        /// <param name="password">The password for encryption</param>
        /// <param name="DriveLabel">The label for the drive</param>
        /// <param name="fs">The Filesystem to use</param>
        /// <returns>The CryDisk object</returns>
        public static CryDisk NewCryDisk(int size, string encPath, char driveLetter, SecureString password, string DriveLabel, VDisk.FileSystem fs)
        {
            string tempPath = $"{Path.GetTempFileName()}.vhdx";
            VDisk.CreateVDisk(tempPath, size, DriveLabel, fs);
            CryDisk cd = new(tempPath, encPath, driveLetter);
            cd.LoadPassword(password);
            cd.Encrypt();
            return cd;
        }

        /// <summary>
        ///     Loads a CryDisk object from an existing file
        /// </summary>
        /// <param name="encPath">The encrypted CryDisk</param>
        /// <param name="driveLetter">Unused</param>
        /// <param name="password">The password for encryption</param>
        /// <returns>The CryDisk object</returns>
        public static CryDisk LoadCryDisk(string encPath, char driveLetter, SecureString password)
        {
            CryDisk cd = new(encPath, driveLetter);
            cd.LoadPassword(password);
            return cd;
        }
    }
}
