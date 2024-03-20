using System.Diagnostics;

namespace LibVDisk
{
    public static class VDisk
    {
        public struct DiskPartResult
        {
            public bool success;
            public int exitCode;
            public string stdOut;
        }

        public enum FileSystem
        {
            FAT32,
            NTFS
        }

        static Dictionary<FileSystem, string> DPFSNames = new Dictionary<FileSystem, string>()
        {
            {FileSystem.FAT32, "FAT32" },
            {FileSystem.NTFS, "NTFS" }
        };

        public static DiskPartResult CreateVDisk(string path, int sizeMb, string label="New VDisk", FileSystem fs = FileSystem.NTFS)
        {
            string dpscript = "create vdisk file=\"" + path + "\" maximum=" + sizeMb + " type=expandable" + Environment.NewLine;
            dpscript += "select vdisk file=\"" + path + "\"" + Environment.NewLine;
            dpscript += "attach vdisk" + Environment.NewLine;
            dpscript += "create partition primary" + Environment.NewLine;
            dpscript += "format fs=" + DPFSNames[fs] + " label=\"" + label + "\" quick" + Environment.NewLine;
            dpscript += "detach vdisk" + Environment.NewLine;
            DiskPartResult res = EvalDiskpartScript(dpscript);
            return res;
        }

        public static DiskPartResult MountVDisk(string path, char letter)
        {
            string dpscript = "select vdisk file=\"" + path + "\"" + Environment.NewLine;
            dpscript += "attach vdisk" + Environment.NewLine;
            dpscript += "select partition 1" + Environment.NewLine;
            dpscript += "assign letter=" + letter + Environment.NewLine;
            return EvalDiskpartScript(dpscript);
        }

        public static DiskPartResult UnmountVDisk(string path)
        {
            string dpscript = "select vdisk file=\"" + path + "\"" + Environment.NewLine;
            dpscript += "detach vdisk" + Environment.NewLine;
            return EvalDiskpartScript(dpscript);
        }

        static DiskPartResult EvalDiskpartScript(string script)
        {
            string ts = Path.GetTempFileName();
            File.WriteAllText(ts, script);
            ProcessStartInfo psi = new ProcessStartInfo("diskpart", "/s " + ts);
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            Process p = Process.Start(psi);
            p.WaitForExit();
            
            File.Delete(ts);

            DiskPartResult res = new DiskPartResult();
            res.success = p.ExitCode == 0;
            res.exitCode = p.ExitCode;
            res.stdOut = p.StandardOutput.ReadToEnd();

            return res;
        }

        public static List<char> GetFreeDriveLetters()
        {
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            List<char> used = new List<char>();
            foreach (var drive in DriveInfo.GetDrives())
            {
                used.Add(drive.Name[0]);
            }
            return letters.Except(used).ToList();
        }
    }
}
