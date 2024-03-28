using CryDiskUi.Properties;
using LibCryDisk;
using System.Diagnostics;

namespace CryDiskUi
{
    public partial class CryDiskEntry : UserControl
    {
        private CryDisk? disk;

        // ReSharper disable once FieldCanBeMadeReadOnly.Local
        private string? DiskPath;
        private bool hasDisk = false;
        public bool locked = true;
        private string? mountPath;
        private string? tempStatus;

        public CryDiskEntry()
        {
            InitializeComponent();
        }

        public CryDiskEntry(string FilePath)
        {
            InitializeComponent();
            DiskPath = FilePath;
            FileInfo fi = new(FilePath);
            label1.Text = fi.Name.Remove(fi.Name.Length - 4); // remove .cyd
            locked = !CryDiskMgr.IsDiskMounted(FilePath);
            UpdateState();
        }

        private void UpdateState()
        {
            if (locked)
            {
                pictureBox1.Image = Resources.lockedp;
            }
            else
            {
                pictureBox1.Image = hasDisk ? Resources.unlockp : Resources.unlock_errp;
            }
            label2.Text = locked ? "Locked" : "Unlocked";
            button1.Text = locked ? "Unlock && Open" : "Lock";
            if (!hasDisk && !locked)
            {
                label2.Text += @" (Needs Password to Lock)";
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
                UnlockDisk ud = new();
                if (ud.ShowDialog() != DialogResult.OK) return;
                tempStatus = "Unlocking...";
                UpdateState();
                FindForm().Cursor = Cursors.WaitCursor;
                disk = CryDiskMgr.LoadCryDisk(DiskPath ?? throw new InvalidOperationException(),
                    ud.GetDriveLetter(),
                    Util.ConvertToSecureString(ud.GetPassword()));

                if (disk.Mount())
                {
                    hasDisk = true;
                    locked = false;
                    mountPath = ud.GetDriveLetter() + ":\\";
                    ProcessStartInfo psi = new()
                    {
                        UseShellExecute = false,
                        FileName = "explorer",
                        Arguments = mountPath
                    };
                    Process.Start(psi);
                }
                else
                {
                    MessageBox.Show(@"Failed to unlock CryDisk. Please check your password and try again.",
                        @"Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                tempStatus = null;
                UpdateState();
                FindForm().Cursor = Cursors.Default;
            }
            else
            {
                if (!hasDisk)
                {
                    UnlockDisk ud = new();
                    ud.HideDriveSelector();
                    if (ud.ShowDialog() != DialogResult.OK) return;

                    disk = CryDiskMgr.LoadCryDisk(DiskPath ?? throw new InvalidOperationException(),
                        ud.GetDriveLetter(),
                        Util.ConvertToSecureString(ud.GetPassword()));

                    hasDisk = true;
                    UpdateState();
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

                }
                else
                {
                    MessageBox.Show(@"Failed to unmount CryDisk. Please ensure nothing is using it, then try again.",
                        @"Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                tempStatus = null;
                UpdateState();
                FindForm().Cursor = Cursors.Default;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"Are you sure you want to delete this CryDisk? This action cannot be undone.",
                    @"Confirm", MessageBoxButtons.YesNo) != DialogResult.Yes) return;

            File.Delete(DiskPath ?? throw new InvalidOperationException());
            Parent.Controls.Remove(this);
        }
    }
}
