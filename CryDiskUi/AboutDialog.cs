using LibCryDisk;

namespace CryDiskUi
{
    public partial class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();
            bitWidth.Text = bitWidth.Text.Replace("256", CryDisk.Bits.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
