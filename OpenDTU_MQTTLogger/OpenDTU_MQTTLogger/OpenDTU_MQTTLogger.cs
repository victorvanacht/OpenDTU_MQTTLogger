using MQTTnet.Client.Options;
using System.ComponentModel;

namespace OpenDTU_MQTTLogger
{
    public partial class OpenDTU_MQTTLogger : Form
    {
        private Worker _worker;

        public OpenDTU_MQTTLogger()
        {
            InitializeComponent();

            notifyIcon.Visible = false;

            this.textBoxMQTTBrokerAddress.Text = Properties.Settings.Default.MQTTBrokerAddress;
            this.textBoxMQTTPort.Text = Properties.Settings.Default.MQTTBrokerPort.ToString();
            this.textBoxMQTTUsername.Text = Properties.Settings.Default.MQTTUsername;
            this.textBoxMQTTPassword.Text = Properties.Settings.Default.MQTTPassword;
            this.textBoxLogFileName.Text = Properties.Settings.Default.LogFilename;
            this.textBoxLogInterval.Text = Properties.Settings.Default.LogInterval.ToString();

            _worker = new Worker(this);

            if (Properties.Settings.Default.Started)
            {
                this.checkBoxStartStop.Checked = true;
            }
        }

        private void checkBoxStartStop_CheckedChanged(object sender, EventArgs e)
        {
            if (this.checkBoxStartStop.Checked)
            {
                // start
                this.checkBoxStartStop.Text = "Stop";


                int port = 0;
                if (int.TryParse(textBoxMQTTPort.Text, out port))
                {
                    _worker.brokerPort = port;
                    _worker.brokerAddress = textBoxMQTTBrokerAddress.Text;
                    _worker.logFilename = textBoxLogFileName.Text;
                    int logInterval = 300; 
                    int.TryParse(textBoxLogInterval.Text, out logInterval);
                    _worker.logInterval = logInterval;

                    _worker.loggingEnabled = true;

                }
            }
            else
            {
                // stop
                this.checkBoxStartStop.Text = "Start";

                this._worker.loggingEnabled = false;
            }
        }

        public void WriteToLog(string text)
        {
            if (this.listBoxMQTTLog.InvokeRequired)
            {
                this.listBoxMQTTLog.Invoke((Action)delegate { WriteToLog(text); });
            }
            else
            {
                string[] lines = text.Split("\r\n");
                foreach (string line in lines)
                {
                    this.listBoxMQTTLog.Items.Add(line);
                    if (this.listBoxMQTTLog.Items.Count > 100)
                    {
                        listBoxMQTTLog.Items.RemoveAt(0);
                    }
                    this.listBoxMQTTLog.SelectedIndex = this.listBoxMQTTLog.Items.Count - 1;
                }
            }
        }

        public void SetProgressBar(int value)
        {
            if (this.progressBar.InvokeRequired)
            {
                this.progressBar.Invoke((Action)delegate { SetProgressBar(value); });
            }
            else
            {
                this.progressBar.Value = value;
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int logInterval = 300;
            int.TryParse(textBoxLogInterval.Text, out logInterval);
            int port = 1883;
            int.TryParse(textBoxMQTTPort.Text, out port);

            Properties.Settings.Default.MQTTBrokerAddress = this.textBoxMQTTBrokerAddress.Text;
            Properties.Settings.Default.MQTTBrokerPort = port;
            Properties.Settings.Default.MQTTUsername = this.textBoxMQTTUsername.Text;
            Properties.Settings.Default.MQTTPassword = this.textBoxMQTTPassword.Text;
            Properties.Settings.Default.LogFilename = this.textBoxLogFileName.Text;
            Properties.Settings.Default.LogInterval = logInterval;
            Properties.Settings.Default.Started = this.checkBoxStartStop.Checked;

            Properties.Settings.Default.Save();

            this._worker.Close(5);
            System.Windows.Forms.Application.Exit();
        }

        private void OpenDTU_MQTTLoggerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                this.WindowState = FormWindowState.Minimized;
            }
            else
            {

            }
        }

        private void OpenDTU_MQTTLogger_Resize(object sender, EventArgs e)
        {
            //if the form is minimized  
            //hide it from the task bar  
            //and show the system tray icon (represented by the NotifyIcon control)  
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                notifyIcon.Visible = true;
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon.Visible = false;
        }
    }
}