namespace LibVDiskUi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string r = LibVDisk.VDisk.CreateVDisk(saveFileDialog1.FileName, (int)numericUpDown1.Value);
                MessageBox.Show(r);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string r = LibVDisk.VDisk.MountVDisk(openFileDialog1.FileName, comboBox1.Text[0]);
                MessageBox.Show(r);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string r = LibVDisk.VDisk.UnmountVDisk(openFileDialog1.FileName);
                MessageBox.Show(r);
            }
        }
    }
}
