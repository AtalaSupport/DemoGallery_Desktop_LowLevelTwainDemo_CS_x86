namespace LowLevelTwainDemo
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.stepsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openSourceManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeSourceManagerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.deviceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.openSourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.closeSourceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.getCapabilityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setCapabilityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.acquireToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nativeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.memoryFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusState = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusInformation = new System.Windows.Forms.ToolStripStatusLabel();
            this.panelImage = new System.Windows.Forms.Panel();
            this.picImage = new System.Windows.Forms.PictureBox();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpAboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.panelImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stepsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(597, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // stepsToolStripMenuItem
            // 
            this.stepsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openSourceManagerToolStripMenuItem,
            this.closeSourceManagerToolStripMenuItem,
            this.toolStripMenuItem1,
            this.deviceToolStripMenuItem,
            this.toolStripMenuItem2,
            this.openSourceToolStripMenuItem,
            this.closeSourceToolStripMenuItem,
            this.toolStripMenuItem3,
            this.getCapabilityToolStripMenuItem,
            this.setCapabilityToolStripMenuItem,
            this.toolStripMenuItem4,
            this.acquireToolStripMenuItem,
            this.toolStripMenuItem5,
            this.exitToolStripMenuItem});
            this.stepsToolStripMenuItem.Name = "stepsToolStripMenuItem";
            this.stepsToolStripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.stepsToolStripMenuItem.Text = "Steps";
            // 
            // openSourceManagerToolStripMenuItem
            // 
            this.openSourceManagerToolStripMenuItem.Name = "openSourceManagerToolStripMenuItem";
            this.openSourceManagerToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.openSourceManagerToolStripMenuItem.Text = "Open Source Manager";
            this.openSourceManagerToolStripMenuItem.Click += new System.EventHandler(this.openSourceManagerToolStripMenuItem_Click);
            // 
            // closeSourceManagerToolStripMenuItem
            // 
            this.closeSourceManagerToolStripMenuItem.Name = "closeSourceManagerToolStripMenuItem";
            this.closeSourceManagerToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.closeSourceManagerToolStripMenuItem.Text = "Close Source Manager";
            this.closeSourceManagerToolStripMenuItem.Click += new System.EventHandler(this.closeSourceManagerToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(178, 6);
            // 
            // deviceToolStripMenuItem
            // 
            this.deviceToolStripMenuItem.Name = "deviceToolStripMenuItem";
            this.deviceToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.deviceToolStripMenuItem.Text = "Device";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(178, 6);
            // 
            // openSourceToolStripMenuItem
            // 
            this.openSourceToolStripMenuItem.Name = "openSourceToolStripMenuItem";
            this.openSourceToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.openSourceToolStripMenuItem.Text = "Open Source";
            this.openSourceToolStripMenuItem.Click += new System.EventHandler(this.openSourceToolStripMenuItem_Click);
            // 
            // closeSourceToolStripMenuItem
            // 
            this.closeSourceToolStripMenuItem.Name = "closeSourceToolStripMenuItem";
            this.closeSourceToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.closeSourceToolStripMenuItem.Text = "Close Source";
            this.closeSourceToolStripMenuItem.Click += new System.EventHandler(this.closeSourceToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(178, 6);
            // 
            // getCapabilityToolStripMenuItem
            // 
            this.getCapabilityToolStripMenuItem.Name = "getCapabilityToolStripMenuItem";
            this.getCapabilityToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.getCapabilityToolStripMenuItem.Text = "Get Capability";
            // 
            // setCapabilityToolStripMenuItem
            // 
            this.setCapabilityToolStripMenuItem.Name = "setCapabilityToolStripMenuItem";
            this.setCapabilityToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.setCapabilityToolStripMenuItem.Text = "Set Capability";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(178, 6);
            // 
            // acquireToolStripMenuItem
            // 
            this.acquireToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nativeToolStripMenuItem,
            this.memoryToolStripMenuItem,
            this.fileToolStripMenuItem,
            this.memoryFileToolStripMenuItem});
            this.acquireToolStripMenuItem.Name = "acquireToolStripMenuItem";
            this.acquireToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.acquireToolStripMenuItem.Text = "Acquire";
            // 
            // nativeToolStripMenuItem
            // 
            this.nativeToolStripMenuItem.Name = "nativeToolStripMenuItem";
            this.nativeToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.nativeToolStripMenuItem.Text = "Native";
            this.nativeToolStripMenuItem.Click += new System.EventHandler(this.nativeToolStripMenuItem_Click);
            // 
            // memoryToolStripMenuItem
            // 
            this.memoryToolStripMenuItem.Name = "memoryToolStripMenuItem";
            this.memoryToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.memoryToolStripMenuItem.Text = "Memory";
            this.memoryToolStripMenuItem.Click += new System.EventHandler(this.memoryToolStripMenuItem_Click);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.fileToolStripMenuItem.Text = "File";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // memoryFileToolStripMenuItem
            // 
            this.memoryFileToolStripMenuItem.Name = "memoryFileToolStripMenuItem";
            this.memoryFileToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
            this.memoryFileToolStripMenuItem.Text = "Memory File";
            this.memoryFileToolStripMenuItem.Click += new System.EventHandler(this.memoryFileToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(178, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusVersion,
            this.statusState,
            this.statusInformation});
            this.statusStrip1.Location = new System.Drawing.Point(0, 387);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(597, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusVersion
            // 
            this.statusVersion.AutoSize = false;
            this.statusVersion.Name = "statusVersion";
            this.statusVersion.Size = new System.Drawing.Size(150, 17);
            this.statusVersion.Text = "DotTwain 8.0.0.0";
            this.statusVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusState
            // 
            this.statusState.AutoSize = false;
            this.statusState.Name = "statusState";
            this.statusState.Size = new System.Drawing.Size(150, 17);
            this.statusState.Text = "Loaded";
            // 
            // statusInformation
            // 
            this.statusInformation.Name = "statusInformation";
            this.statusInformation.Size = new System.Drawing.Size(282, 17);
            this.statusInformation.Spring = true;
            // 
            // panelImage
            // 
            this.panelImage.AutoScroll = true;
            this.panelImage.Controls.Add(this.picImage);
            this.panelImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelImage.Location = new System.Drawing.Point(0, 24);
            this.panelImage.Name = "panelImage";
            this.panelImage.Size = new System.Drawing.Size(597, 363);
            this.panelImage.TabIndex = 2;
            // 
            // picImage
            // 
            this.picImage.Location = new System.Drawing.Point(0, 0);
            this.picImage.Name = "picImage";
            this.picImage.Size = new System.Drawing.Size(184, 177);
            this.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picImage.TabIndex = 0;
            this.picImage.TabStop = false;
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helpAboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // helpAboutToolStripMenuItem
            // 
            this.helpAboutToolStripMenuItem.Name = "helpAboutToolStripMenuItem";
            this.helpAboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.helpAboutToolStripMenuItem.Text = "About ...";
            this.helpAboutToolStripMenuItem.Click += new System.EventHandler(this.helpAboutToolStripMenuItem_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(597, 409);
            this.Controls.Add(this.panelImage);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Low Level DotTwain Demo";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.panelImage.ResumeLayout(false);
            this.panelImage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel statusVersion;
        private System.Windows.Forms.ToolStripStatusLabel statusState;
        private System.Windows.Forms.ToolStripStatusLabel statusInformation;
        private System.Windows.Forms.Panel panelImage;
        private System.Windows.Forms.PictureBox picImage;
        private System.Windows.Forms.ToolStripMenuItem stepsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openSourceManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeSourceManagerToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem openSourceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem closeSourceToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem getCapabilityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setCapabilityToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripMenuItem acquireToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nativeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem memoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem memoryFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deviceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpAboutToolStripMenuItem;
    }
}


