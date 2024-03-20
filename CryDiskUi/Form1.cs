using LibCryDisk;
using System.Security;
using System.IO;

namespace CryDiskUi
{
    public partial class Form1 : Form
    {

        public static string CryDiskStoragePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\CryDisk";

        public Form1()
        {
            InitializeComponent();
            if (!Directory.Exists(CryDiskStoragePath))
            {
                Directory.CreateDirectory(CryDiskStoragePath);
            }
            ReloadList();

        }

        void ReloadList()
        {
            flowLayoutPanel1.Controls.Clear();
            foreach (var file in Directory.GetFiles(CryDiskStoragePath))
            {
                CryDiskEntry cde = new CryDiskEntry(file);
                flowLayoutPanel1.Controls.Add(cde);
            }
            flowLayoutPanel1_Resize(null, null);
            Refresh();
            if (flowLayoutPanel1.Controls.Count != 1)
            {
                toolStripLabel1.Text = flowLayoutPanel1.Controls.Count + " CryDisks";
            }
            else
            {
                toolStripLabel1.Text = flowLayoutPanel1.Controls.Count + " CryDisk";
            }

        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            var nd = new NewCryDisk();
            if (nd.ShowDialog() == DialogResult.OK)
            {
                ReloadList();
            }
        }

        private void flowLayoutPanel1_Resize(object sender, EventArgs e)
        {
            foreach (var c in flowLayoutPanel1.Controls)
            {
                if (c is CryDiskEntry)
                {
                    ((CryDiskEntry)c).Width = flowLayoutPanel1.Width - 24;
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool areAllLocked = true;
            foreach (var c in flowLayoutPanel1.Controls)
            {
                if (c is CryDiskEntry)
                {
                    if (!((CryDiskEntry)c).locked)
                    {
                        areAllLocked = false;
                        break;
                    }
                }
            }
            if (!areAllLocked)
            {
                var r = MessageBox.Show("Not all CryDisks are locked. Are you sure you want to exit?", "Confirm", MessageBoxButtons.YesNo);
                if (r == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            ReloadList();
        }
    }
}
