using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using MQTTnet.Server;

namespace OpenDTU_MQTTLogger
{
    internal class MQTTClient
    {
        private IMqttClient _client;
        private IMqttClientOptions _clientOptions;
        private string[] _keepConnectedTopics;
        private MQTTLogger _logger;

        public delegate void MQTTConnectedEventHandler(MqttClientConnectedEventArgs e);
        public delegate void MQTTMessageEventHandler(MqttApplicationMessageReceivedEventArgs e);
        public delegate void MQTTDisconnectedEventHandler(MqttClientDisconnectedEventArgs e);
        public delegate void MQTTLogger(string line);

        public MQTTClient(IMqttClientOptions clientOptions)
        {
            _clientOptions = clientOptions;
            _client = new MqttFactory().CreateMqttClient();

        }

        public MQTTClient(IMqttClientOptions clientOptions, MQTTConnectedEventHandler connectedHandler, MQTTMessageEventHandler messageHandler, MQTTDisconnectedEventHandler disconnectedHandler, MQTTLogger logger) : this(clientOptions)
        {
            _logger = logger;
            RegisterHandlers(connectedHandler, messageHandler, disconnectedHandler);
        }

        public async Task PublishAsync(MqttApplicationMessage message)
        {
            await _client.PublishAsync(message);
        }

        public async Task StartAsync()
        {
            await _client.ConnectAsync(_clientOptions, CancellationToken.None);
        }

        public async Task SubscribeAsync(params string[] topics)
        {
            await Task.WhenAll(topics.Select(async topic => await _client.SubscribeAsync(topic)));

            Log($"Subscribed to {string.Join(',', topics)} topics");
        }

        public async Task DisconnectAsync()
        {
            await _client.DisconnectAsync();
        }

        public async Task KeepConnectedAndSubscribed(params string[] topics)
        {
            _keepConnectedTopics = topics;

            await ReConnect();
        }

        public async Task ReConnect()
        {
            if ((_keepConnectedTopics != null) && (_keepConnectedTopics.Length > 0))
            {
                await StartAsync();
                await SubscribeAsync(_keepConnectedTopics);
            }
        }

        public void RegisterHandlers(MQTTConnectedEventHandler connectedHandler, MQTTMessageEventHandler messageHandler, MQTTDisconnectedEventHandler disconnectedHandler)
        {
            _client.UseConnectedHandler(e => { connectedHandler(e); });
            _client.UseApplicationMessageReceivedHandler( e => { messageHandler(e); });
            _client.UseDisconnectedHandler(e => { disconnectedHandler(e); });
        }

        private void Log(string line)
        {
            this._logger(line);
        }

    }
}
