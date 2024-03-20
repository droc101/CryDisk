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
    public partial class NewCryDisk : Form
    {
        public NewCryDisk()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (pw1.Text != pw2.Text)
            {
                MessageBox.Show("Passwords must match");
                return;
            }
            Cursor = Cursors.WaitCursor;
            CryDisk cd = CryDiskMgr.NewCryDisk((int)size.Value * 1024, Form1.CryDiskStoragePath + "\\" + filename.Text + ".cyd", 'Z', LibCryDisk.Util.ConvertToSecureString(pw1.Text));
            DialogResult = DialogResult.OK;
            Close();
        }

        private void size_ValueChanged(object sender, EventArgs e)
        {
            sizeLabel.Text = size.Value + " GB";
        }
    }
}
