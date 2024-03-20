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
            ((System.ComponentModel.ISupportInitialize)size).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(11, 9);
            label1.Margin = new Padding(2, 0, 2, 0);
            label1.Name = "label1";
            label1.Size = new Size(156, 15);
            label1.TabIndex = 0;
            label1.Text = "Filename (NOT ENCRYPTED)";
            // 
            // filename
            // 
            filename.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            filename.Location = new Point(11, 26);
            filename.Margin = new Padding(2);
            filename.MaxLength = 64;
            filename.Name = "filename";
            filename.Size = new Size(312, 23);
            filename.TabIndex = 1;
            // 
            // size
            // 
            size.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            size.LargeChange = 1024;
            size.Location = new Point(11, 112);
            size.Margin = new Padding(2);
            size.Maximum = 32;
            size.Minimum = 1;
            size.Name = "size";
            size.Size = new Size(312, 45);
            size.TabIndex = 4;
            size.Value = 8;
            size.ValueChanged += size_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(11, 95);
            label3.Margin = new Padding(2, 0, 2, 0);
            label3.Name = "label3";
            label3.Size = new Size(185, 15);
            label3.TabIndex = 5;
            label3.Text = "Size (Encrypted file may be larger)";
            // 
            // sizeLabel
            // 
            sizeLabel.AutoSize = true;
            sizeLabel.Location = new Point(11, 142);
            sizeLabel.Margin = new Padding(2, 0, 2, 0);
            sizeLabel.Name = "sizeLabel";
            sizeLabel.Size = new Size(52, 15);
            sizeLabel.TabIndex = 6;
            sizeLabel.Text = "8192 MB";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(11, 170);
            label5.Margin = new Padding(2, 0, 2, 0);
            label5.Name = "label5";
            label5.Size = new Size(57, 15);
            label5.TabIndex = 7;
            label5.Text = "Password";
            // 
            // pw1
            // 
            pw1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pw1.Location = new Point(11, 187);
            pw1.Margin = new Padding(2);
            pw1.Name = "pw1";
            pw1.Size = new Size(312, 23);
            pw1.TabIndex = 8;
            pw1.UseSystemPasswordChar = true;
            // 
            // pw2
            // 
            pw2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pw2.Location = new Point(11, 214);
            pw2.Margin = new Padding(2);
            pw2.Name = "pw2";
            pw2.Size = new Size(312, 23);
            pw2.TabIndex = 9;
            pw2.UseSystemPasswordChar = true;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button1.Location = new Point(247, 316);
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
            button2.Location = new Point(168, 316);
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
            label2.Location = new Point(11, 51);
            label2.Name = "label2";
            label2.Size = new Size(65, 15);
            label2.TabIndex = 13;
            label2.Text = "Drive Label";
            // 
            // driveLabel
            // 
            driveLabel.Location = new Point(11, 69);
            driveLabel.MaxLength = 32;
            driveLabel.Name = "driveLabel";
            driveLabel.Size = new Size(312, 23);
            driveLabel.TabIndex = 14;
            driveLabel.Text = "CryDisk";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(12, 239);
            label4.Name = "label4";
            label4.Size = new Size(62, 15);
            label4.TabIndex = 15;
            label4.Text = "Filesystem";
            // 
            // comboBox1
            // 
            comboBox1.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { "FAT32", "NTFS" });
            comboBox1.Location = new Point(11, 257);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(312, 23);
            comboBox1.TabIndex = 16;
            // 
            // NewCryDisk
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = button2;
            ClientSize = new Size(335, 351);
            Controls.Add(comboBox1);
            Controls.Add(label4);
            Controls.Add(driveLabel);
            Controls.Add(label2);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(pw2);
            Controls.Add(pw1);
            Controls.Add(label5);
            Controls.Add(sizeLabel);
            Controls.Add(label3);
            Controls.Add(size);
            Controls.Add(filename);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Margin = new Padding(2);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "NewCryDisk";
            StartPosition = FormStartPosition.CenterParent;
            Text = "New CryDisk";
            Load += NewCryDisk_Load;
            ((System.ComponentModel.ISupportInitialize)size).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
    }
}