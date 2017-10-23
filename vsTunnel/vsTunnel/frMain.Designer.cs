namespace vsTunnel
{
    partial class frMain
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
            this.txtIPAddress = new System.Windows.Forms.TextBox();
            this.txtPortFromVS = new System.Windows.Forms.TextBox();
            this.txtLog = new System.Windows.Forms.TextBox();
            this.btStop = new System.Windows.Forms.Button();
            this.btStart = new System.Windows.Forms.Button();
            this.txtPortToRD = new System.Windows.Forms.TextBox();
            this.lbIPAddress = new System.Windows.Forms.Label();
            this.rbListenFromVS = new System.Windows.Forms.RadioButton();
            this.lbPortFromVS = new System.Windows.Forms.Label();
            this.lbPortToRD = new System.Windows.Forms.Label();
            this.lbVSToRDDirectory = new System.Windows.Forms.Label();
            this.txtVSToRDDirectory = new System.Windows.Forms.TextBox();
            this.lbRDToVSDirectory = new System.Windows.Forms.Label();
            this.txtRDToVSDirectory = new System.Windows.Forms.TextBox();
            this.lbLog = new System.Windows.Forms.Label();
            this.btClearLog = new System.Windows.Forms.Button();
            this.cbScrollLog = new System.Windows.Forms.CheckBox();
            this.lbSessionsDirectory = new System.Windows.Forms.Label();
            this.txtSessionsDirectory = new System.Windows.Forms.TextBox();
            this.gbMode = new System.Windows.Forms.GroupBox();
            this.rbConnectToRD = new System.Windows.Forms.RadioButton();
            this.gbDirectoryMode = new System.Windows.Forms.GroupBox();
            this.rbSeparately = new System.Windows.Forms.RadioButton();
            this.rbSession = new System.Windows.Forms.RadioButton();
            this.chWatchDirectory = new System.Windows.Forms.CheckBox();
            this.gbMode.SuspendLayout();
            this.gbDirectoryMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtIPAddress
            // 
            this.txtIPAddress.Location = new System.Drawing.Point(12, 61);
            this.txtIPAddress.Name = "txtIPAddress";
            this.txtIPAddress.Size = new System.Drawing.Size(198, 20);
            this.txtIPAddress.TabIndex = 0;
            this.txtIPAddress.Text = "127.0.0.1";
            // 
            // txtPortFromVS
            // 
            this.txtPortFromVS.Location = new System.Drawing.Point(12, 104);
            this.txtPortFromVS.Name = "txtPortFromVS";
            this.txtPortFromVS.Size = new System.Drawing.Size(60, 20);
            this.txtPortFromVS.TabIndex = 1;
            this.txtPortFromVS.Text = "4020";
            // 
            // txtLog
            // 
            this.txtLog.BackColor = System.Drawing.SystemColors.Window;
            this.txtLog.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtLog.Location = new System.Drawing.Point(11, 266);
            this.txtLog.Multiline = true;
            this.txtLog.Name = "txtLog";
            this.txtLog.ReadOnly = true;
            this.txtLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(323, 110);
            this.txtLog.TabIndex = 2;
            // 
            // btStop
            // 
            this.btStop.Location = new System.Drawing.Point(238, 35);
            this.btStop.Name = "btStop";
            this.btStop.Size = new System.Drawing.Size(96, 23);
            this.btStop.TabIndex = 3;
            this.btStop.Text = "Stop";
            this.btStop.UseVisualStyleBackColor = true;
            this.btStop.Click += new System.EventHandler(this.btStop_Click);
            // 
            // btStart
            // 
            this.btStart.Location = new System.Drawing.Point(239, 6);
            this.btStart.Name = "btStart";
            this.btStart.Size = new System.Drawing.Size(96, 23);
            this.btStart.TabIndex = 4;
            this.btStart.Text = "Start";
            this.btStart.UseVisualStyleBackColor = true;
            this.btStart.Click += new System.EventHandler(this.btStart_Click);
            // 
            // txtPortToRD
            // 
            this.txtPortToRD.Location = new System.Drawing.Point(87, 104);
            this.txtPortToRD.Name = "txtPortToRD";
            this.txtPortToRD.Size = new System.Drawing.Size(56, 20);
            this.txtPortToRD.TabIndex = 5;
            this.txtPortToRD.Text = "4022";
            // 
            // lbIPAddress
            // 
            this.lbIPAddress.AutoSize = true;
            this.lbIPAddress.Location = new System.Drawing.Point(9, 45);
            this.lbIPAddress.Name = "lbIPAddress";
            this.lbIPAddress.Size = new System.Drawing.Size(61, 13);
            this.lbIPAddress.TabIndex = 6;
            this.lbIPAddress.Text = "IP Address:";
            // 
            // rbListenFromVS
            // 
            this.rbListenFromVS.AutoSize = true;
            this.rbListenFromVS.Checked = true;
            this.rbListenFromVS.Location = new System.Drawing.Point(5, 15);
            this.rbListenFromVS.Name = "rbListenFromVS";
            this.rbListenFromVS.Size = new System.Drawing.Size(93, 17);
            this.rbListenFromVS.TabIndex = 8;
            this.rbListenFromVS.TabStop = true;
            this.rbListenFromVS.Text = "Listen from VS";
            this.rbListenFromVS.UseVisualStyleBackColor = true;
            this.rbListenFromVS.CheckedChanged += new System.EventHandler(this.rbListenFromVS_CheckedChanged);
            // 
            // lbPortFromVS
            // 
            this.lbPortFromVS.AutoSize = true;
            this.lbPortFromVS.Location = new System.Drawing.Point(9, 88);
            this.lbPortFromVS.Name = "lbPortFromVS";
            this.lbPortFromVS.Size = new System.Drawing.Size(69, 13);
            this.lbPortFromVS.TabIndex = 10;
            this.lbPortFromVS.Text = "Port from VS:";
            // 
            // lbPortToRD
            // 
            this.lbPortToRD.AutoSize = true;
            this.lbPortToRD.Location = new System.Drawing.Point(84, 88);
            this.lbPortToRD.Name = "lbPortToRD";
            this.lbPortToRD.Size = new System.Drawing.Size(60, 13);
            this.lbPortToRD.TabIndex = 11;
            this.lbPortToRD.Text = "Port to RD:";
            // 
            // lbVSToRDDirectory
            // 
            this.lbVSToRDDirectory.AutoSize = true;
            this.lbVSToRDDirectory.Location = new System.Drawing.Point(8, 168);
            this.lbVSToRDDirectory.Name = "lbVSToRDDirectory";
            this.lbVSToRDDirectory.Size = new System.Drawing.Size(95, 13);
            this.lbVSToRDDirectory.TabIndex = 12;
            this.lbVSToRDDirectory.Text = "VS toRD directory:";
            // 
            // txtVSToRDDirectory
            // 
            this.txtVSToRDDirectory.Location = new System.Drawing.Point(11, 184);
            this.txtVSToRDDirectory.Name = "txtVSToRDDirectory";
            this.txtVSToRDDirectory.Size = new System.Drawing.Size(323, 20);
            this.txtVSToRDDirectory.TabIndex = 13;
            // 
            // lbRDToVSDirectory
            // 
            this.lbRDToVSDirectory.AutoSize = true;
            this.lbRDToVSDirectory.Location = new System.Drawing.Point(8, 211);
            this.lbRDToVSDirectory.Name = "lbRDToVSDirectory";
            this.lbRDToVSDirectory.Size = new System.Drawing.Size(98, 13);
            this.lbRDToVSDirectory.TabIndex = 14;
            this.lbRDToVSDirectory.Text = "RD to VS directory:";
            // 
            // txtRDToVSDirectory
            // 
            this.txtRDToVSDirectory.Location = new System.Drawing.Point(11, 227);
            this.txtRDToVSDirectory.Name = "txtRDToVSDirectory";
            this.txtRDToVSDirectory.Size = new System.Drawing.Size(323, 20);
            this.txtRDToVSDirectory.TabIndex = 15;
            // 
            // lbLog
            // 
            this.lbLog.AutoSize = true;
            this.lbLog.Location = new System.Drawing.Point(8, 250);
            this.lbLog.Name = "lbLog";
            this.lbLog.Size = new System.Drawing.Size(28, 13);
            this.lbLog.TabIndex = 16;
            this.lbLog.Text = "Log:";
            // 
            // btClearLog
            // 
            this.btClearLog.Location = new System.Drawing.Point(260, 381);
            this.btClearLog.Name = "btClearLog";
            this.btClearLog.Size = new System.Drawing.Size(75, 23);
            this.btClearLog.TabIndex = 17;
            this.btClearLog.Text = "Clear log";
            this.btClearLog.UseVisualStyleBackColor = true;
            this.btClearLog.Click += new System.EventHandler(this.btClearLog_Click);
            // 
            // cbScrollLog
            // 
            this.cbScrollLog.AutoSize = true;
            this.cbScrollLog.Checked = true;
            this.cbScrollLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbScrollLog.Location = new System.Drawing.Point(11, 385);
            this.cbScrollLog.Name = "cbScrollLog";
            this.cbScrollLog.Size = new System.Drawing.Size(69, 17);
            this.cbScrollLog.TabIndex = 18;
            this.cbScrollLog.Text = "Scroll log";
            this.cbScrollLog.UseVisualStyleBackColor = true;
            // 
            // lbSessionsDirectory
            // 
            this.lbSessionsDirectory.AutoSize = true;
            this.lbSessionsDirectory.Location = new System.Drawing.Point(8, 128);
            this.lbSessionsDirectory.Name = "lbSessionsDirectory";
            this.lbSessionsDirectory.Size = new System.Drawing.Size(95, 13);
            this.lbSessionsDirectory.TabIndex = 19;
            this.lbSessionsDirectory.Text = "Sessions directory:";
            // 
            // txtSessionsDirectory
            // 
            this.txtSessionsDirectory.Location = new System.Drawing.Point(11, 144);
            this.txtSessionsDirectory.Name = "txtSessionsDirectory";
            this.txtSessionsDirectory.Size = new System.Drawing.Size(324, 20);
            this.txtSessionsDirectory.TabIndex = 20;
            this.txtSessionsDirectory.Text = ".";
            // 
            // gbMode
            // 
            this.gbMode.Controls.Add(this.rbConnectToRD);
            this.gbMode.Controls.Add(this.rbListenFromVS);
            this.gbMode.Location = new System.Drawing.Point(11, 1);
            this.gbMode.Name = "gbMode";
            this.gbMode.Size = new System.Drawing.Size(199, 39);
            this.gbMode.TabIndex = 21;
            this.gbMode.TabStop = false;
            this.gbMode.Text = "Mode:";
            // 
            // rbConnectToRD
            // 
            this.rbConnectToRD.AutoSize = true;
            this.rbConnectToRD.Location = new System.Drawing.Point(102, 16);
            this.rbConnectToRD.Name = "rbConnectToRD";
            this.rbConnectToRD.Size = new System.Drawing.Size(96, 17);
            this.rbConnectToRD.TabIndex = 10;
            this.rbConnectToRD.Text = "Connect to RD";
            this.rbConnectToRD.UseVisualStyleBackColor = true;
            this.rbConnectToRD.CheckedChanged += new System.EventHandler(this.rbConnectToRD_CheckedChanged);
            // 
            // gbDirectoryMode
            // 
            this.gbDirectoryMode.Controls.Add(this.rbSeparately);
            this.gbDirectoryMode.Controls.Add(this.rbSession);
            this.gbDirectoryMode.Location = new System.Drawing.Point(162, 88);
            this.gbDirectoryMode.Name = "gbDirectoryMode";
            this.gbDirectoryMode.Size = new System.Drawing.Size(173, 50);
            this.gbDirectoryMode.TabIndex = 22;
            this.gbDirectoryMode.TabStop = false;
            this.gbDirectoryMode.Text = "Directory mode:";
            // 
            // rbSeparately
            // 
            this.rbSeparately.AutoSize = true;
            this.rbSeparately.Location = new System.Drawing.Point(77, 20);
            this.rbSeparately.Name = "rbSeparately";
            this.rbSeparately.Size = new System.Drawing.Size(75, 17);
            this.rbSeparately.TabIndex = 1;
            this.rbSeparately.Text = "Separately";
            this.rbSeparately.UseVisualStyleBackColor = true;
            this.rbSeparately.CheckedChanged += new System.EventHandler(this.rbSeparately_CheckedChanged);
            // 
            // rbSession
            // 
            this.rbSession.AutoSize = true;
            this.rbSession.Checked = true;
            this.rbSession.Location = new System.Drawing.Point(7, 20);
            this.rbSession.Name = "rbSession";
            this.rbSession.Size = new System.Drawing.Size(67, 17);
            this.rbSession.TabIndex = 0;
            this.rbSession.TabStop = true;
            this.rbSession.Text = "Sessions";
            this.rbSession.UseVisualStyleBackColor = true;
            this.rbSession.CheckedChanged += new System.EventHandler(this.rbSession_CheckedChanged);
            // 
            // chWatchDirectory
            // 
            this.chWatchDirectory.AutoSize = true;
            this.chWatchDirectory.Checked = true;
            this.chWatchDirectory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chWatchDirectory.Location = new System.Drawing.Point(233, 65);
            this.chWatchDirectory.Name = "chWatchDirectory";
            this.chWatchDirectory.Size = new System.Drawing.Size(101, 17);
            this.chWatchDirectory.TabIndex = 23;
            this.chWatchDirectory.Text = "Watch directory";
            this.chWatchDirectory.UseVisualStyleBackColor = true;
            // 
            // frMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(346, 408);
            this.Controls.Add(this.chWatchDirectory);
            this.Controls.Add(this.gbDirectoryMode);
            this.Controls.Add(this.gbMode);
            this.Controls.Add(this.txtSessionsDirectory);
            this.Controls.Add(this.lbSessionsDirectory);
            this.Controls.Add(this.cbScrollLog);
            this.Controls.Add(this.btClearLog);
            this.Controls.Add(this.lbLog);
            this.Controls.Add(this.txtRDToVSDirectory);
            this.Controls.Add(this.lbRDToVSDirectory);
            this.Controls.Add(this.txtVSToRDDirectory);
            this.Controls.Add(this.lbVSToRDDirectory);
            this.Controls.Add(this.lbPortToRD);
            this.Controls.Add(this.lbPortFromVS);
            this.Controls.Add(this.lbIPAddress);
            this.Controls.Add(this.txtPortToRD);
            this.Controls.Add(this.btStart);
            this.Controls.Add(this.btStop);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.txtPortFromVS);
            this.Controls.Add(this.txtIPAddress);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "frMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "vsTunnel";
            this.Load += new System.EventHandler(this.frMain_Load);
            this.gbMode.ResumeLayout(false);
            this.gbMode.PerformLayout();
            this.gbDirectoryMode.ResumeLayout(false);
            this.gbDirectoryMode.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIPAddress;
        private System.Windows.Forms.TextBox txtPortFromVS;
        private System.Windows.Forms.TextBox txtLog;
        private System.Windows.Forms.Button btStop;
        private System.Windows.Forms.Button btStart;
        private System.Windows.Forms.TextBox txtPortToRD;
        private System.Windows.Forms.Label lbIPAddress;
        private System.Windows.Forms.RadioButton rbListenFromVS;
        private System.Windows.Forms.Label lbPortFromVS;
        private System.Windows.Forms.Label lbPortToRD;
        private System.Windows.Forms.Label lbVSToRDDirectory;
        private System.Windows.Forms.TextBox txtVSToRDDirectory;
        private System.Windows.Forms.Label lbRDToVSDirectory;
        private System.Windows.Forms.TextBox txtRDToVSDirectory;
        private System.Windows.Forms.Label lbLog;
        private System.Windows.Forms.Button btClearLog;
        private System.Windows.Forms.CheckBox cbScrollLog;
        private System.Windows.Forms.Label lbSessionsDirectory;
        private System.Windows.Forms.TextBox txtSessionsDirectory;
        private System.Windows.Forms.GroupBox gbMode;
        private System.Windows.Forms.RadioButton rbConnectToRD;
        private System.Windows.Forms.GroupBox gbDirectoryMode;
        private System.Windows.Forms.RadioButton rbSeparately;
        private System.Windows.Forms.RadioButton rbSession;
        private System.Windows.Forms.CheckBox chWatchDirectory;
    }
}

