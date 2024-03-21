using System.Diagnostics;

namespace LibVDisk
{
    /// <summary>
    /// Virtual Disk Tools
    /// </summary>
    public static class VDisk
    {
        /// <summary>
        /// Result of a DiskPart operation
        /// </summary>
        public struct DiskPartResult
        {
            /// <summary>
            /// Whether the operation was successful (exit code 0)
            /// </summary>
            public bool success;
            /// <summary>
            /// The exit code from DiskPart
            /// </summary>
            public int exitCode;
            /// <summary>
            /// The stdout from DiskPart
            /// </summary>
            public string stdOut;
        }

        /// <summary>
        /// Filesystem to use when formatting a disk
        /// </summary>
        public enum FileSystem
        {
            /// <summary>
            /// FAT32 file system
            /// </summary>
            FAT32,
            /// <summary>
            /// NTFS file system
            /// </summary>
            NTFS
        }

        /// <summary>
        /// FileSystem enum to DiskPart script name
        /// </summary>
        static Dictionary<FileSystem, string> DPFSNames = new Dictionary<FileSystem, string>()
        {
            {FileSystem.FAT32, "FAT32" },
            {FileSystem.NTFS, "NTFS" }
        };

        /// <summary>
        /// Create a VHDX file
        /// </summary>
        /// <param name="path">File to save to</param>
        /// <param name="sizeMb">Size in MB</param>
        /// <param name="label">Volume label</param>
        /// <param name="fs">FileSystem</param>
        /// <returns>Results from DiskPart</returns>
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

        /// <summary>
        /// Mount a VHDX file
        /// </summary>
        /// <param name="path">The VHDX file</param>
        /// <param name="letter">Drive letter</param>
        /// <returns>Results from DiskPart</returns>
        public static DiskPartResult MountVDisk(string path, char letter)
        {
            string dpscript = "select vdisk file=\"" + path + "\"" + Environment.NewLine;
            dpscript += "attach vdisk" + Environment.NewLine;
            dpscript += "select partition 1" + Environment.NewLine;
            dpscript += "assign letter=" + letter + Environment.NewLine;
            return EvalDiskpartScript(dpscript);
        }

        /// <summary>
        /// Unmount a VHDX file
        /// </summary>
        /// <remarks>
        /// If the user ejected the VDisk by themselves, this may fail.
        /// </remarks>
        /// <param name="path">The VHDX file</param>
        /// <returns>Results from DiskPart</returns>
        public static DiskPartResult UnmountVDisk(string path)
        {
            string dpscript = "select vdisk file=\"" + path + "\"" + Environment.NewLine;
            dpscript += "detach vdisk" + Environment.NewLine;
            return EvalDiskpartScript(dpscript);
        }

        /// <summary>
        /// Runs a string of text as a DiskPart script
        /// </summary>
        /// <param name="script">The script to run</param>
        /// <returns>Results of the script</returns>
        static DiskPartResult EvalDiskpartScript(string script)
        {
            string ts = Path.GetTempFileName();
            File.WriteAllText(ts, script);
            ProcessStartInfo psi = new ProcessStartInfo("diskpart", "/s " + ts);
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
            Process p = Process.Start(psi);
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
            p.WaitForExit();
            
            File.Delete(ts);

            DiskPartResult res = new DiskPartResult();
            res.success = p.ExitCode == 0;
            res.exitCode = p.ExitCode;
            res.stdOut = p.StandardOutput.ReadToEnd();

            return res;
        }

        /// <summary>
        /// Get a list of free drive letters.
        /// </summary>
        /// <remarks>
        /// Does NOT include mapped network shares when running as admin.
        /// </remarks>
        /// <returns>List of free drive letters</returns>
        public static List<char> GetFreeDriveLetters()
        {
            string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            List<char> used = new List<char>();
            used.Add('A'); // don't allow floppy drive letters
            used.Add('B');
            foreach (var drive in DriveInfo.GetDrives())
            {
                used.Add(drive.Name[0]);
            }
            return letters.Except(used).ToList();
        }
    }
}
