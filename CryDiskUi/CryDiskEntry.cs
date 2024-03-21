using LibCryDisk;
using System.Diagnostics;

namespace CryDiskUi
{
    public partial class CryDiskEntry : UserControl
    {
        public CryDiskEntry()
        {
            InitializeComponent();
        }

        string? DiskPath;
        public bool locked = true;
        bool hasDisk = false;
        string? tempStatus;
        string? mountPath;
        CryDisk? disk;

        public CryDiskEntry(string FilePath)
        {
            InitializeComponent();
            DiskPath = FilePath;
            FileInfo fi = new FileInfo(FilePath);
            label1.Text = fi.Name.Remove(fi.Name.Length - 4); // remove .cyd
            locked = !CryDiskMgr.IsDiskMounted(FilePath);
            UpdateState();
        }

        void UpdateState()
        {
            if (locked)
            {
                pictureBox1.Image = Properties.Resources.lockedp;
            } else
            {
                if (hasDisk)
                {
                    pictureBox1.Image = Properties.Resources.unlockp;
                } else
                {
                    pictureBox1.Image = Properties.Resources.unlock_errp;
                }
            }
            label2.Text = locked ? "Locked" : "Unlocked";
            button1.Text = locked ? "Unlock && Open" : "Lock";
            if (!hasDisk && !locked)
            {
                label2.Text += " (Needs Password to Lock)";
            }
            if (mountPath != null)
            {
                label2.Text += Environment.NewLine + mountPath;
            }
            button2.Enabled = locked;
            if (tempStatus != null)
            {
                label2.Text = tempStatus;
            }
            Refresh();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (locked)
            {
                UnlockDisk ud = new UnlockDisk();
                if (ud.ShowDialog() == DialogResult.OK)
                {
                    tempStatus = "Unlocking...";
                    UpdateState();
                    FindForm().Cursor = Cursors.WaitCursor;
#pragma warning disable CS8604 // Possible null reference argument.
                    disk = CryDiskMgr.LoadCryDisk(DiskPath, ud.GetDriveLetter(), LibCryDisk.Util.ConvertToSecureString(ud.GetPassword()));
#pragma warning restore CS8604 // Possible null reference argument.
                    if (disk.Mount())
                    {
                        hasDisk = true;
                        locked = false;
                        mountPath = ud.GetDriveLetter() + ":\\";
                        ProcessStartInfo psi = new ProcessStartInfo();
                        psi.UseShellExecute = false;
                        psi.FileName = "explorer";
                        psi.Arguments = mountPath;
                        Process.Start(psi);
                    }
                    else
                    {
                        MessageBox.Show("Failed to unlock CryDisk. Please check your password and try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    tempStatus = null;
                    UpdateState();
                    FindForm().Cursor = Cursors.Default;
                }
            }
            else
            {
                if (!hasDisk)
                {
                    UnlockDisk ud = new UnlockDisk();
                    ud.HideDriveSelector();
                    if (ud.ShowDialog() == DialogResult.OK)
                    {
#pragma warning disable CS8604 // Possible null reference argument.
                        disk = CryDiskMgr.LoadCryDisk(DiskPath, ud.GetDriveLetter(), LibCryDisk.Util.ConvertToSecureString(ud.GetPassword()));
#pragma warning restore CS8604 // Possible null reference argument.
                        hasDisk = true;
                        UpdateState();
                    }
                    else
                    {
                        return;
                    }
                }
                tempStatus = "Locking...";
                UpdateState();
                FindForm().Cursor = Cursors.WaitCursor;
                if (disk.Unmount())
                {
                    disk = null;
                    locked = true;
                    hasDisk = false;
                    mountPath = null;
                    
                } else
                {
                    MessageBox.Show("Failed to unmount CryDisk. Please ensure nothing is using it, then try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                tempStatus = null;
                UpdateState();
                FindForm().Cursor = Cursors.Default;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to delete this CryDisk? This action cannot be undone.", "Confirm", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
#pragma warning disable CS8604 // Possible null reference argument.
                File.Delete(DiskPath);
#pragma warning restore CS8604 // Possible null reference argument.
                Parent.Controls.Remove(this);
            }
        }

        private void CryDiskEntry_Load(object sender, EventArgs e)
        {

        }
    }
}
