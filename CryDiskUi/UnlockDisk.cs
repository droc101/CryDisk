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
    public partial class UnlockDisk : Form
    {
        public UnlockDisk()
        {
            InitializeComponent();
            var ltrs = LibVDisk.VDisk.GetFreeDriveLetters();
            comboBox1.Items.Clear();
            foreach (var l in ltrs)
            {
                comboBox1.Items.Add(l + ":");
            }
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            } else
            {
                MessageBox.Show("No free drive letters available");
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
