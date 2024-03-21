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
            int dc = 0;
            foreach (var file in Directory.GetFiles(CryDiskStoragePath))
            {
                if (Path.GetExtension(file) != ".cyd")
                {
                    continue;
                }
                CryDiskEntry cde = new CryDiskEntry(file);
                flowLayoutPanel1.Controls.Add(cde);
                dc++;
            }
            if (dc != 1)
            {
                toolStripLabel1.Text = dc.ToString() + " CryDisks";
            }
            else
            {
                toolStripLabel1.Text = dc.ToString() + " CryDisk";
            }
            flowLayoutPanel1_Resize(null, null);
            Refresh();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            var nd = new NewCryDisk();
            if (nd.ShowDialog() == DialogResult.OK)
            {
                ReloadList();
            }
        }

        private void flowLayoutPanel1_Resize(object? sender, EventArgs? e)
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
                var r = MessageBox.Show("Not all CryDisks are locked. Are you sure you want to exit?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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

        private void flowLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {
            flowLayoutPanel1_Resize(null, null);
        }
    }
}
