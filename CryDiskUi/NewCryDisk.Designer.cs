namespace CryDiskUi
{
    partial class NewCryDisk
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            filename = new TextBox();
            size = new TrackBar();
            label3 = new Label();
            sizeLabel = new Label();
            label5 = new Label();
            pw1 = new MaskedTextBox();
            pw2 = new MaskedTextBox();
            button1 = new Button();
            button2 = new Button();
            label2 = new Label();
            driveLabel = new TextBox();
            label4 = new Label();
            comboBox1 = new ComboBox();
            groupBox1 = new GroupBox();
            numericUpDown1 = new NumericUpDown();
            groupBox2 = new GroupBox();
            label6 = new Label();
            ((System.ComponentModel.ISupportInitialize)size).BeginInit();
            groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            groupBox2.SuspendLayout();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(6, 19);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(156, 15);
            label1.TabIndex = 0;
            label1.Text = "Filename (NOT ENCRYPTED)";
            // 
            // filename
            // 
            filename.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            filename.Location = new Point(6, 36);
            filename.Margin = new Padding(2);
            filename.MaxLength = 64;
            filename.Name = "filename";
            filename.Size = new Size(361, 23);
            filename.TabIndex = 1;
            // 
            // size
            // 
            size.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            size.LargeChange = 8;
            size.Location = new Point(6, 124);
            size.Margin = new Padding(2);
            size.Maximum = 32;
            size.Minimum = 1;
            size.Name = "size";
            size.Size = new Size(361, 45);
            size.TabIndex = 4;
            size.Value = 8;
            size.ValueChanged += size_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(6, 107);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(185, 15);
            label3.TabIndex = 5;
            label3.Text = "Size (Encrypted file may be larger)";
            // 
            // sizeLabel
            // 
            sizeLabel.AutoSize = true;
            sizeLabel.Location = new Point(81, 157);
            sizeLabel.Margin = new Padding(2, 0, 2, 0);
            sizeLabel.Name = "sizeLabel";
            sizeLabel.Size = new Size(25, 15);
            sizeLabel.TabIndex = 6;
            sizeLabel.Text = "MB";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(6, 61);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(57, 15);
            label5.TabIndex = 7;
            label5.Text = "Password";
            // 
            // pw1
            // 
            pw1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pw1.Location = new Point(6, 78);
            pw1.Margin = new Padding(2);
            pw1.Name = "pw1";
            pw1.Size = new Size(361, 23);
            pw1.TabIndex = 8;
            pw1.UseSystemPasswordChar = true;
            // 
            // pw2
            // 
            pw2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pw2.Location = new Point(6, 105);
            pw2.Margin = new Padding(2);
            pw2.Name = "pw2";
            pw2.Size = new Size(361, 23);
            pw2.TabIndex = 9;
            pw2.UseSystemPasswordChar = true;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button1.Location = new Point(309, 392);
            button1.Margin = new Padding(2);
            button1.Name = "button1";
            button1.Size = new Size(76, 24);
            button1.TabIndex = 11;
            button1.Text = "Create";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button2.DialogResult = DialogResult.Cancel;
            button2.Location = new Point(230, 392);
            button2.Margin = new Padding(2);
            button2.Name = "button2";
            button2.Size = new Size(76, 24);
            button2.TabIndex = 12;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 19);
            label2.Name = "label2";
            label2.Size = new Size(65, 15);
            label2.TabIndex = 13;
            label2.Text = "Drive Label";
            // 
            // driveLabel
            // 
            driveLabel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            driveLabel.Location = new Point(6, 37);
            driveLabel.MaxLength = 32;
            driveLabel.Name = "driveLabel";
            driveLabel.Size = new Size(361, 23);
            driveLabel.TabIndex = 14;
            driveLabel.Text = "CryDisk";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 63);
            label4.Name = "label4";
            label4.Size = new Size(62, 15);
            label4.TabIndex = 15;
            label4.Text = "Filesystem";
            // 
            // comboBox1
            // 
            comboBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "FAT32", "NTFS" });
            comboBox1.Location = new Point(6, 81);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(361, 23);
            comboBox1.TabIndex = 16;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox1.Controls.Add(numericUpDown1);
            groupBox1.Controls.Add(sizeLabel);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(comboBox1);
            groupBox1.Controls.Add(driveLabel);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(size);
            groupBox1.Location = new Point(12, 189);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(373, 189);
            groupBox1.TabIndex = 17;
            groupBox1.TabStop = false;
            groupBox1.Text = "Disk Settings";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(6, 155);
            numericUpDown1.Maximum = new decimal(new int[] { 131072, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 64, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(75, 23);
            numericUpDown1.TabIndex = 17;
            numericUpDown1.TextAlign = HorizontalAlignment.Right;
            numericUpDown1.UpDownAlign = LeftRightAlignment.Left;
            numericUpDown1.Value = new decimal(new int[] { 64, 0, 0, 0 });
            numericUpDown1.ValueChanged += numericUpDown1_ValueChanged;
            // 
            // groupBox2
            // 
            groupBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(label1);
            groupBox2.Controls.Add(filename);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(pw1);
            groupBox2.Controls.Add(pw2);
            groupBox2.Location = new Point(12, 12);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(373, 171);
            groupBox2.TabIndex = 18;
            groupBox2.TabStop = false;
            groupBox2.Text = "Storage Settings";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(6, 130);
            label6.Name = "label6";
            label6.Size = new Size(327, 30);
            label6.TabIndex = 10;
            label6.Text = "WARNING: If you forget this password, this CryDisk CANNOT\r\nBE RECOVERED.";
            // 
            // NewCryDisk
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = button2;
            ClientSize = new Size(397, 427);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "NewCryDisk";
            ShowIcon = false;
            ShowInTaskbar = false;
            StartPosition = FormStartPosition.CenterParent;
            Text = "New CryDisk";
            ((System.ComponentModel.ISupportInitialize)size).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label label1;
        private TextBox filename;
        private TrackBar size;
        private Label label3;
        private Label sizeLabel;
        private Label label5;
        private MaskedTextBox pw1;
        private MaskedTextBox pw2;
        private Button button1;
        private Button button2;
        private Label label2;
        private TextBox driveLabel;
        private Label label4;
        private ComboBox comboBox1;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private Label label6;
        private NumericUpDown numericUpDown1;
    }
}