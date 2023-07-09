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

            _worker = new Worker(this);
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
                    _worker.loggingEnabled = true;
                    _worker.logFilename = textBoxLogFileName.Text;
                    _worker.logInterval = 300; 
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


    }
}