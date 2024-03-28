using LibVDisk;

namespace CryDiskUi
{
    public partial class UnlockDisk : Form
    {
        public UnlockDisk()
        {
            InitializeComponent();
            var ltrs = VDisk.GetFreeDriveLetters();
            comboBox1.Items.Clear();
            foreach (char l in ltrs)
            {
                comboBox1.Items.Add($"{l}:");
            }
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
            else
            {
                MessageBox.Show(@"No free drive letters available");
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }

        public void HideDriveSelector()
        {
            comboBox1.Visible = false;
            label2.Visible = false;
        }

        public string GetPassword()
        {
            return maskedTextBox1.Text;
        }

        public char GetDriveLetter()
        {
            return comboBox1.Text[0];
        }
    }
}
