namespace CryDiskUi
{
    public partial class Form1 : Form
    {

        public static readonly string CryDiskStoragePath =
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\CryDisk";

        public Form1()
        {
            InitializeComponent();
            if (!Directory.Exists(CryDiskStoragePath))
            {
                Directory.CreateDirectory(CryDiskStoragePath);
            }
            ReloadList();
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            switch (m.Msg)
            {
                case 0x84: //WM_NCHITTEST
                    HitTest result = (HitTest)m.Result.ToInt32();
                    // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
                    switch (result)
                    {
                        case HitTest.Left:
                        case HitTest.Right:
                            m.Result = new IntPtr((int)HitTest.Caption);
                            break;
                        case HitTest.TopLeft:
                        case HitTest.TopRight:
                            m.Result = new IntPtr((int)HitTest.Top);
                            break;
                        case HitTest.BottomLeft:
                        case HitTest.BottomRight:
                            m.Result = new IntPtr((int)HitTest.Bottom);
                            break;
                    }

                    break;
            }
        }

        private void ReloadList()
        {
            flowLayoutPanel1.Controls.Clear();
            foreach (string file in Directory.GetFiles(CryDiskStoragePath))
            {
                if (Path.GetExtension(file) != ".cyd")
                {
                    continue;
                }
                CryDiskEntry cde = new(file);
                flowLayoutPanel1.Controls.Add(cde);
            }
            flowLayoutPanel1_Resize(null, null);
            Refresh();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            NewCryDisk nd = new NewCryDisk();
            if (nd.ShowDialog() == DialogResult.OK) ReloadList();
        }

        private void flowLayoutPanel1_Resize(object? sender, EventArgs? e)
        {
            foreach (object? c in flowLayoutPanel1.Controls)
            {
                CryDiskEntry entry = c as CryDiskEntry ?? throw new InvalidOperationException();
                entry.Width = flowLayoutPanel1.Width - 24;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            bool areAllLocked = true;
            foreach (object? c in flowLayoutPanel1.Controls)
            {
                if (c is not CryDiskEntry entry) continue;
                if (entry.locked) continue;
                areAllLocked = false;
                break;
            }

            if (areAllLocked) return;
            DialogResult r = MessageBox.Show(@"Not all CryDisks are locked. Are you sure you want to exit?", @"Confirm", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);
            if (r == DialogResult.No)
            {
                e.Cancel = true;
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

        private enum HitTest
        {
            Caption = 2,
            Left = 10,
            Right = 11,
            Top = 12,
            TopLeft = 13,
            TopRight = 14,
            Bottom = 15,
            BottomLeft = 16,
            BottomRight = 17
        }
    }
}
