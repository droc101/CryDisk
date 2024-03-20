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
            ((System.ComponentModel.ISupportInitialize)size).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(7, 9);
            label1.Name = "label1";
            label1.Size = new Size(276, 30);
            label1.TabIndex = 0;
            label1.Text = "Filename (NOT ENCRYPTED)";
            // 
            // filename
            // 
            filename.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            filename.Location = new Point(12, 42);
            filename.Name = "filename";
            filename.Size = new Size(542, 35);
            filename.TabIndex = 1;
            // 
            // size
            // 
            size.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            size.LargeChange = 1024;
            size.Location = new Point(12, 113);
            size.Maximum = 8;
            size.Minimum = 1;
            size.Name = "size";
            size.Size = new Size(542, 80);
            size.TabIndex = 4;
            size.Value = 8;
            size.ValueChanged += size_ValueChanged;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 80);
            label3.Name = "label3";
            label3.Size = new Size(149, 30);
            label3.TabIndex = 5;
            label3.Text = "Maximum Size";
            // 
            // sizeLabel
            // 
            sizeLabel.AutoSize = true;
            sizeLabel.Location = new Point(12, 163);
            sizeLabel.Name = "sizeLabel";
            sizeLabel.Size = new Size(94, 30);
            sizeLabel.TabIndex = 6;
            sizeLabel.Text = "8192 MB";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(7, 222);
            label5.Name = "label5";
            label5.Size = new Size(99, 30);
            label5.TabIndex = 7;
            label5.Text = "Password";
            // 
            // pw1
            // 
            pw1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pw1.Location = new Point(12, 255);
            pw1.Name = "pw1";
            pw1.Size = new Size(542, 35);
            pw1.TabIndex = 8;
            pw1.UseSystemPasswordChar = true;
            // 
            // pw2
            // 
            pw2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            pw2.Location = new Point(12, 296);
            pw2.Name = "pw2";
            pw2.Size = new Size(542, 35);
            pw2.TabIndex = 9;
            pw2.UseSystemPasswordChar = true;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button1.Location = new Point(431, 365);
            button1.Name = "button1";
            button1.Size = new Size(131, 40);
            button1.TabIndex = 11;
            button1.Text = "Create";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button2.DialogResult = DialogResult.Cancel;
            button2.Location = new Point(294, 365);
            button2.Name = "button2";
            button2.Size = new Size(131, 40);
            button2.TabIndex = 12;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = true;
            // 
            // NewCryDisk
            // 
            AutoScaleDimensions = new SizeF(12F, 30F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(574, 417);
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
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "NewCryDisk";
            StartPosition = FormStartPosition.CenterParent;
            Text = "New CryDisk";
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
    }
}