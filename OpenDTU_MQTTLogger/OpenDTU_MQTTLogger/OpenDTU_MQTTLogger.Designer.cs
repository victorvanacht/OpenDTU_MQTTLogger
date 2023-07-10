namespace OpenDTU_MQTTLogger
{
    partial class OpenDTU_MQTTLogger
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenDTU_MQTTLogger));
            this.groupBoxMQTTSettings = new System.Windows.Forms.GroupBox();
            this.checkBoxStartStop = new System.Windows.Forms.CheckBox();
            this.textBoxMQTTPort = new System.Windows.Forms.TextBox();
            this.labelMQTTPort = new System.Windows.Forms.Label();
            this.textBoxMQTTPassword = new System.Windows.Forms.TextBox();
            this.labelMQTTPassword = new System.Windows.Forms.Label();
            this.textBoxMQTTUsername = new System.Windows.Forms.TextBox();
            this.labelMQTTUsername = new System.Windows.Forms.Label();
            this.textBoxMQTTBrokerAddress = new System.Windows.Forms.TextBox();
            this.labelMQTTBrokerAddress = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBoxLogfile = new System.Windows.Forms.GroupBox();
            this.textBoxLogFileName = new System.Windows.Forms.TextBox();
            this.labelLogfileName = new System.Windows.Forms.Label();
            this.labelMQTTLog = new System.Windows.Forms.Label();
            this.listBoxMQTTLog = new System.Windows.Forms.ListBox();
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.textBoxLogInterval = new System.Windows.Forms.TextBox();
            this.labelLogInterval = new System.Windows.Forms.Label();
            this.groupBoxMQTTSettings.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBoxLogfile.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxMQTTSettings
            // 
            this.groupBoxMQTTSettings.Controls.Add(this.textBoxLogInterval);
            this.groupBoxMQTTSettings.Controls.Add(this.labelLogInterval);
            this.groupBoxMQTTSettings.Controls.Add(this.checkBoxStartStop);
            this.groupBoxMQTTSettings.Controls.Add(this.textBoxMQTTPort);
            this.groupBoxMQTTSettings.Controls.Add(this.labelMQTTPort);
            this.groupBoxMQTTSettings.Controls.Add(this.textBoxMQTTPassword);
            this.groupBoxMQTTSettings.Controls.Add(this.labelMQTTPassword);
            this.groupBoxMQTTSettings.Controls.Add(this.textBoxMQTTUsername);
            this.groupBoxMQTTSettings.Controls.Add(this.labelMQTTUsername);
            this.groupBoxMQTTSettings.Controls.Add(this.textBoxMQTTBrokerAddress);
            this.groupBoxMQTTSettings.Controls.Add(this.labelMQTTBrokerAddress);
            this.groupBoxMQTTSettings.Location = new System.Drawing.Point(12, 27);
            this.groupBoxMQTTSettings.Name = "groupBoxMQTTSettings";
            this.groupBoxMQTTSettings.Size = new System.Drawing.Size(433, 194);
            this.groupBoxMQTTSettings.TabIndex = 0;
            this.groupBoxMQTTSettings.TabStop = false;
            this.groupBoxMQTTSettings.Text = "MQTT Settings";
            // 
            // checkBoxStartStop
            // 
            this.checkBoxStartStop.Appearance = System.Windows.Forms.Appearance.Button;
            this.checkBoxStartStop.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.checkBoxStartStop.Location = new System.Drawing.Point(367, 158);
            this.checkBoxStartStop.Name = "checkBoxStartStop";
            this.checkBoxStartStop.Size = new System.Drawing.Size(60, 30);
            this.checkBoxStartStop.TabIndex = 9;
            this.checkBoxStartStop.Text = "Start";
            this.checkBoxStartStop.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBoxStartStop.UseVisualStyleBackColor = true;
            this.checkBoxStartStop.CheckedChanged += new System.EventHandler(this.checkBoxStartStop_CheckedChanged);
            // 
            // textBoxMQTTPort
            // 
            this.textBoxMQTTPort.Location = new System.Drawing.Point(158, 62);
            this.textBoxMQTTPort.Name = "textBoxMQTTPort";
            this.textBoxMQTTPort.Size = new System.Drawing.Size(269, 23);
            this.textBoxMQTTPort.TabIndex = 8;
            this.textBoxMQTTPort.Text = "1883";
            // 
            // labelMQTTPort
            // 
            this.labelMQTTPort.AutoSize = true;
            this.labelMQTTPort.Location = new System.Drawing.Point(6, 65);
            this.labelMQTTPort.Name = "labelMQTTPort";
            this.labelMQTTPort.Size = new System.Drawing.Size(100, 15);
            this.labelMQTTPort.TabIndex = 7;
            this.labelMQTTPort.Text = "MQTT Broker Port";
            // 
            // textBoxMQTTPassword
            // 
            this.textBoxMQTTPassword.Location = new System.Drawing.Point(158, 120);
            this.textBoxMQTTPassword.Name = "textBoxMQTTPassword";
            this.textBoxMQTTPassword.Size = new System.Drawing.Size(269, 23);
            this.textBoxMQTTPassword.TabIndex = 5;
            // 
            // labelMQTTPassword
            // 
            this.labelMQTTPassword.AutoSize = true;
            this.labelMQTTPassword.Location = new System.Drawing.Point(6, 123);
            this.labelMQTTPassword.Name = "labelMQTTPassword";
            this.labelMQTTPassword.Size = new System.Drawing.Size(91, 15);
            this.labelMQTTPassword.TabIndex = 4;
            this.labelMQTTPassword.Text = "MQTT Password";
            // 
            // textBoxMQTTUsername
            // 
            this.textBoxMQTTUsername.Location = new System.Drawing.Point(158, 91);
            this.textBoxMQTTUsername.Name = "textBoxMQTTUsername";
            this.textBoxMQTTUsername.Size = new System.Drawing.Size(269, 23);
            this.textBoxMQTTUsername.TabIndex = 3;
            // 
            // labelMQTTUsername
            // 
            this.labelMQTTUsername.AutoSize = true;
            this.labelMQTTUsername.Location = new System.Drawing.Point(6, 94);
            this.labelMQTTUsername.Name = "labelMQTTUsername";
            this.labelMQTTUsername.Size = new System.Drawing.Size(94, 15);
            this.labelMQTTUsername.TabIndex = 2;
            this.labelMQTTUsername.Text = "MQTT Username";
            // 
            // textBoxMQTTBrokerAddress
            // 
            this.textBoxMQTTBrokerAddress.Location = new System.Drawing.Point(158, 33);
            this.textBoxMQTTBrokerAddress.Name = "textBoxMQTTBrokerAddress";
            this.textBoxMQTTBrokerAddress.Size = new System.Drawing.Size(269, 23);
            this.textBoxMQTTBrokerAddress.TabIndex = 1;
            this.textBoxMQTTBrokerAddress.Text = "192.168.50.141";
            // 
            // labelMQTTBrokerAddress
            // 
            this.labelMQTTBrokerAddress.AutoSize = true;
            this.labelMQTTBrokerAddress.Location = new System.Drawing.Point(6, 36);
            this.labelMQTTBrokerAddress.Name = "labelMQTTBrokerAddress";
            this.labelMQTTBrokerAddress.Size = new System.Drawing.Size(120, 15);
            this.labelMQTTBrokerAddress.TabIndex = 0;
            this.labelMQTTBrokerAddress.Text = "MQTT Broker Address";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // groupBoxLogfile
            // 
            this.groupBoxLogfile.Controls.Add(this.textBoxLogFileName);
            this.groupBoxLogfile.Controls.Add(this.labelLogfileName);
            this.groupBoxLogfile.Location = new System.Drawing.Point(12, 227);
            this.groupBoxLogfile.Name = "groupBoxLogfile";
            this.groupBoxLogfile.Size = new System.Drawing.Size(433, 61);
            this.groupBoxLogfile.TabIndex = 2;
            this.groupBoxLogfile.TabStop = false;
            this.groupBoxLogfile.Text = "Logfile Settings";
            // 
            // textBoxLogFileName
            // 
            this.textBoxLogFileName.Location = new System.Drawing.Point(158, 28);
            this.textBoxLogFileName.Name = "textBoxLogFileName";
            this.textBoxLogFileName.Size = new System.Drawing.Size(269, 23);
            this.textBoxLogFileName.TabIndex = 6;
            this.textBoxLogFileName.Text = "e:\\test.csv";
            // 
            // labelLogfileName
            // 
            this.labelLogfileName.AutoSize = true;
            this.labelLogfileName.Location = new System.Drawing.Point(6, 31);
            this.labelLogfileName.Name = "labelLogfileName";
            this.labelLogfileName.Size = new System.Drawing.Size(79, 15);
            this.labelLogfileName.TabIndex = 5;
            this.labelLogfileName.Text = "Log file name";
            // 
            // labelMQTTLog
            // 
            this.labelMQTTLog.AutoSize = true;
            this.labelMQTTLog.Location = new System.Drawing.Point(467, 63);
            this.labelMQTTLog.Name = "labelMQTTLog";
            this.labelMQTTLog.Size = new System.Drawing.Size(61, 15);
            this.labelMQTTLog.TabIndex = 3;
            this.labelMQTTLog.Text = "MQTT Log";
            // 
            // listBoxMQTTLog
            // 
            this.listBoxMQTTLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listBoxMQTTLog.FormattingEnabled = true;
            this.listBoxMQTTLog.ItemHeight = 15;
            this.listBoxMQTTLog.Location = new System.Drawing.Point(541, 62);
            this.listBoxMQTTLog.Name = "listBoxMQTTLog";
            this.listBoxMQTTLog.Size = new System.Drawing.Size(247, 259);
            this.listBoxMQTTLog.TabIndex = 4;
            // 
            // progressBar
            // 
            this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar.Location = new System.Drawing.Point(541, 342);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(247, 23);
            this.progressBar.TabIndex = 5;
            // 
            // notifyIcon
            // 
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "OpenDTU_MQTTLogger";
            this.notifyIcon.Visible = true;
            // 
            // textBoxLogInterval
            // 
            this.textBoxLogInterval.Location = new System.Drawing.Point(158, 163);
            this.textBoxLogInterval.Name = "textBoxLogInterval";
            this.textBoxLogInterval.Size = new System.Drawing.Size(53, 23);
            this.textBoxLogInterval.TabIndex = 11;
            this.textBoxLogInterval.Text = "300";
            // 
            // labelLogInterval
            // 
            this.labelLogInterval.AutoSize = true;
            this.labelLogInterval.Location = new System.Drawing.Point(6, 166);
            this.labelLogInterval.Name = "labelLogInterval";
            this.labelLogInterval.Size = new System.Drawing.Size(69, 15);
            this.labelLogInterval.TabIndex = 10;
            this.labelLogInterval.Text = "Log interval";
            // 
            // OpenDTU_MQTTLogger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 377);
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.listBoxMQTTLog);
            this.Controls.Add(this.labelMQTTLog);
            this.Controls.Add(this.groupBoxLogfile);
            this.Controls.Add(this.groupBoxMQTTSettings);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "OpenDTU_MQTTLogger";
            this.Text = "OpenDTU_MQTTLogger";
            this.groupBoxMQTTSettings.ResumeLayout(false);
            this.groupBoxMQTTSettings.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBoxLogfile.ResumeLayout(false);
            this.groupBoxLogfile.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private GroupBox groupBoxMQTTSettings;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private TextBox textBoxMQTTBrokerAddress;
        private Label labelMQTTBrokerAddress;
        private TextBox textBoxMQTTPassword;
        private Label labelMQTTPassword;
        private TextBox textBoxMQTTUsername;
        private Label labelMQTTUsername;
        private GroupBox groupBoxLogfile;
        private TextBox textBoxLogFileName;
        private Label labelLogfileName;
        private Label labelMQTTLog;
        private ListBox listBoxMQTTLog;
        private ProgressBar progressBar;
        private NotifyIcon notifyIcon;
        private TextBox textBoxMQTTPort;
        private Label labelMQTTPort;
        private CheckBox checkBoxStartStop;
        private TextBox textBoxLogInterval;
        private Label labelLogInterval;
    }
}