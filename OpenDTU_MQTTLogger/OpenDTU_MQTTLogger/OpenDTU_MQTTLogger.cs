using MQTTnet.Client.Options;

namespace OpenDTU_MQTTLogger
{
    public partial class OpenDTU_MQTTLogger : Form
    {
        private MQTTClient _mqttClient;
        OpenDTUData _openDTUdata;

        public OpenDTU_MQTTLogger()
        {
            InitializeComponent();

            _openDTUdata = new OpenDTUData();
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            int port = 0;
            if (int.TryParse(textBoxMQTTPort.Text, out port))
            {
                ConnectToMqtt(textBoxMQTTBrokerAddress.Text, 1883);
            }
        }

        private async Task ConnectToMqtt(string broker, int port)
        {
            IMqttClientOptions options = new MqttClientOptionsBuilder()
                .WithClientId("OpenDTU-MQTTLogger")
                .WithTcpServer(broker, port)
                .Build();

            _mqttClient = new MQTTClient(options, _openDTUdata);

            await _mqttClient.KeepConnectedAndSubscribed("solar/#");
        }

        private void groupBoxMQTTSettings_Enter(object sender, EventArgs e)
        {

        }
    }
}