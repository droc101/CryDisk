namespace CryDiskUi
{
    public partial class Form1 : Form
    {

        public static string CryDiskStoragePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\CryDisk";

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case 0x84: //WM_NCHITTEST
                    var result = (HitTest)m.Result.ToInt32();
                    if (result == HitTest.Left || result == HitTest.Right)
                        m.Result = new IntPtr((int)HitTest.Caption);
                    if (result == HitTest.TopLeft || result == HitTest.TopRight)
                        m.Result = new IntPtr((int)HitTest.Top);
                    if (result == HitTest.BottomLeft || result == HitTest.BottomRight)
                        m.Result = new IntPtr((int)HitTest.Bottom);

                    break;
            }
        }
        enum HitTest
        {
            Caption = 2,
            Transparent = -1,
            Nowhere = 0,
            Client = 1,
            Left = 10,
            Right = 11,
            Top = 12,
            TopLeft = 13,
            TopRight = 14,
            Bottom = 15,
            BottomLeft = 16,
            BottomRight = 17,
            Border = 18
        }

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
                if (Path.GetExtension(file) != ".cyd")
                {
                    continue;
                }
                CryDiskEntry cde = new CryDiskEntry(file);
                flowLayoutPanel1.Controls.Add(cde);
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

        private void flowLayoutPanel1_SizeChanged(object sender, EventArgs e)
        {
            flowLayoutPanel1_Resize(null, null);
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            new AboutDialog().ShowDialog();
        }
    }
}
