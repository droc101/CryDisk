using LibCryDisk;
using LibVDisk;

namespace CryDiskUi
{
    public partial class NewCryDisk : Form
    {
        public NewCryDisk()
        {
            InitializeComponent();
            size_ValueChanged(null, null);
            comboBox1.SelectedIndex = 1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (filename.Text == "")
            {
                MessageBox.Show("You must enter a filename.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (pw1.Text == "")
            {
                MessageBox.Show("You must enter a password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (pw1.Text != pw2.Text)
            {
                MessageBox.Show("Passwords must match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string p = Form1.CryDiskStoragePath + "\\" + filename.Text + ".cyd";

            if (File.Exists(p))
            {
                MessageBox.Show("File already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Cursor = Cursors.WaitCursor;
            CryDiskMgr.NewCryDisk((int)numericUpDown1.Value, p, 'Z', Util.ConvertToSecureString(pw1.Text), driveLabel.Text, (VDisk.FileSystem)comboBox1.SelectedIndex);
            DialogResult = DialogResult.OK;
            Close();
        }

        private void size_ValueChanged(object? sender, EventArgs? e)
        {
            numericUpDown1.Value = size.Value * 1024;
        }

        private void NewCryDisk_Load(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int val = (int)numericUpDown1.Value / 1024;
            if (val < 1)
                val = 1;
            if (val > 32)
                val = 32;
            if (val == size.Value)
                return;
            size.Value = val;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            { // FAT32
                driveLabel.MaxLength = 11;
            } else if (comboBox1.SelectedIndex == 1)
            { // NTFS
                driveLabel.MaxLength = 32;
            }
        }
    }
}
