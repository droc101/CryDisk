using LibCryDisk;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CryDiskUi
{
    public partial class CryDiskEntry : UserControl
    {
        public CryDiskEntry()
        {
            InitializeComponent();
        }

        string DiskPath;
        public bool locked = true;
        bool hasDisk = false;
        string tempStatus;
        CryDisk disk;

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
            label2.Text = locked ? "Locked" : "Unlocked";
            button1.Text = locked ? "Unlock" : "Lock";
            if (!hasDisk && !locked)
            {
                label2.Text += " (Needs Password to Lock)";
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
                    disk = CryDiskMgr.LoadCryDisk(DiskPath, ud.GetDriveLetter(), LibCryDisk.Util.ConvertToSecureString(ud.GetPassword()));
                    if (disk.Mount())
                    {
                        hasDisk = true;
                        locked = false;
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
                        disk = CryDiskMgr.LoadCryDisk(DiskPath, ud.GetDriveLetter(), LibCryDisk.Util.ConvertToSecureString(ud.GetPassword()));
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
                File.Delete(DiskPath);
                Parent.Controls.Remove(this);
            }
        }

        private void CryDiskEntry_Load(object sender, EventArgs e)
        {

        }
    }
}
