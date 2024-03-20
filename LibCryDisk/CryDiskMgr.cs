using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace LibCryDisk
{
    public class CryDiskMgr
    {

        public static bool IsDiskMounted(string EncPath)
        {
            return CryDisk.IsDiskMounted(EncPath);
        }

        public static CryDisk NewCryDisk(int size, string encPath, char driveLetter, SecureString password)
        {
            string tempPath = System.IO.Path.GetTempFileName() + ".vhdx";
            LibVDisk.VDisk.CreateVDisk(tempPath, size, "CryDisk");
            CryDisk cd = new CryDisk(tempPath, encPath, driveLetter);
            cd.LoadPassword(password);
            cd.Encrypt();
            return cd;
        }

        public static CryDisk LoadCryDisk(string encPath, char driveLetter, SecureString password)
        {
            CryDisk cd = new CryDisk(encPath, driveLetter);
            cd.LoadPassword(password);
            return cd;
        }

        public static bool LockCryDisk(CryDisk cd)
        {
            return cd.Unmount();
        }

        public static bool UnlockCryDisk(CryDisk cd)
        {
            return cd.Mount();
        }
    }
}
