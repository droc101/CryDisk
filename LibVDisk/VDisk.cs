using System.Diagnostics;

namespace LibVDisk
{
    public static class VDisk
    {
        public static string CreateVDisk(string path, int sizeMb, string label="New VDisk")
        {
            string dpscript = "create vdisk file=\"" + path + "\" maximum=" + sizeMb + " type=expandable" + Environment.NewLine;
            dpscript += "select vdisk file=\"" + path + "\"" + Environment.NewLine;
            dpscript += "attach vdisk" + Environment.NewLine;
            dpscript += "create partition primary" + Environment.NewLine;
            dpscript += "format fs=ntfs label=\"" + label + "\" quick" + Environment.NewLine;
            dpscript += "detach vdisk" + Environment.NewLine;
            return EvalDiskpartScript(dpscript);
        }

        public static string MountVDisk(string path, char letter)
        {
            string dpscript = "select vdisk file=\"" + path + "\"" + Environment.NewLine;
            dpscript += "attach vdisk" + Environment.NewLine;
            dpscript += "select partition 1" + Environment.NewLine;
            dpscript += "assign letter=" + letter + Environment.NewLine;
            return EvalDiskpartScript(dpscript);
        }

        public static string UnmountVDisk(string path)
        {
            string dpscript = "select vdisk file=\"" + path + "\"" + Environment.NewLine;
            dpscript += "detach vdisk" + Environment.NewLine;
            return EvalDiskpartScript(dpscript);
        }

        static string EvalDiskpartScript(string script)
        {
            string ts = Path.GetTempFileName();
            File.WriteAllText(ts, script);
            ProcessStartInfo psi = new ProcessStartInfo("diskpart", "/s " + ts);
            //ProcessStartInfo psi = new ProcessStartInfo("notepad", ts);
            psi.UseShellExecute = false;
            psi.CreateNoWindow = true;
            psi.RedirectStandardOutput = true;
            Process p = Process.Start(psi);
            p.WaitForExit();
            
            File.Delete(ts);

            string rc = p.ExitCode.ToString() + Environment.NewLine + p.StandardOutput.ReadToEnd();

            return rc;
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
