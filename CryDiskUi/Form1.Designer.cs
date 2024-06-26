﻿namespace CryDiskUi
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            openFileDialog1 = new OpenFileDialog();
            saveFileDialog1 = new SaveFileDialog();
            toolStrip1 = new ToolStrip();
            toolStripButton3 = new ToolStripButton();
            toolStripButton1 = new ToolStripButton();
            flowLayoutPanel1 = new FlowLayoutPanel();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // openFileDialog1
            // 
            openFileDialog1.Filter = "CryDisk|*.cyd";
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.Filter = "CryDisk|*.cyd";
            // 
            // toolStrip1
            // 
            toolStrip1.GripStyle = ToolStripGripStyle.Hidden;
            toolStrip1.ImageScalingSize = new Size(28, 28);
            toolStrip1.Items.AddRange(new ToolStripItem[] { toolStripButton3, toolStripButton1 });
            toolStrip1.LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow;
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.MinimumSize = new Size(0, 30);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Padding = new Padding(6, 0, 6, 0);
            toolStrip1.RenderMode = ToolStripRenderMode.System;
            toolStrip1.Size = new Size(622, 30);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton3
            // 
            toolStripButton3.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton3.Image = (Image)resources.GetObject("toolStripButton3.Image");
            toolStripButton3.ImageTransparentColor = Color.Magenta;
            toolStripButton3.Name = "toolStripButton3";
            toolStripButton3.Size = new Size(78, 27);
            toolStripButton3.Text = "New CryDisk";
            toolStripButton3.Click += toolStripButton3_Click;
            // 
            // toolStripButton1
            // 
            toolStripButton1.Alignment = ToolStripItemAlignment.Right;
            toolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Text;
            toolStripButton1.Image = (Image)resources.GetObject("toolStripButton1.Image");
            toolStripButton1.ImageTransparentColor = Color.Magenta;
            toolStripButton1.Name = "toolStripButton1";
            toolStripButton1.Size = new Size(44, 27);
            toolStripButton1.Text = "About";
            toolStripButton1.Click += toolStripButton1_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoScroll = true;
            flowLayoutPanel1.BackColor = SystemColors.Window;
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.FlowDirection = FlowDirection.TopDown;
            flowLayoutPanel1.Location = new Point(0, 30);
            flowLayoutPanel1.Margin = new Padding(2);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(622, 344);
            flowLayoutPanel1.TabIndex = 1;
            flowLayoutPanel1.WrapContents = false;
            flowLayoutPanel1.SizeChanged += flowLayoutPanel1_SizeChanged;
            flowLayoutPanel1.Resize += flowLayoutPanel1_Resize;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(622, 374);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(toolStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(2);
            MaximizeBox = false;
            MinimumSize = new Size(400, 200);
            Name = "Form1";
            Text = "CryDisk Manager";
            FormClosing += Form1_FormClosing;
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private ToolStrip toolStrip1;
        private ToolStripButton toolStripButton3;
        private FlowLayoutPanel flowLayoutPanel1;
        private ToolStripButton toolStripButton1;
    }
}
