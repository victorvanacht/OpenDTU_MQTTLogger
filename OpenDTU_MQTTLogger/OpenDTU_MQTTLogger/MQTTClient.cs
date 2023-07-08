using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;

namespace OpenDTU_MQTTLogger
{
    internal class MQTTClient
    {
        private IMqttClient _client;
        private IMqttClientOptions _clientOptions;
        private OpenDTUData _openDTUData;
        private string[] _keepConnectedTopics;

        public MQTTClient(IMqttClientOptions clientOptions)
        {
            _clientOptions = clientOptions;
            _client = new MqttFactory().CreateMqttClient();
            RegisterHandlers();
        }

        public MQTTClient(IMqttClientOptions clientOptions, OpenDTUData openDTUdata) : this(clientOptions)
        {
            _openDTUData = openDTUdata;
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

            Console.WriteLine($"Subscribed to {string.Join(',', topics)} topics");
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

        private void RegisterHandlers()
        {
            _client.UseConnectedHandler(e =>
            {
                Console.WriteLine("### Connected with server ###");
            });

            _client.UseApplicationMessageReceivedHandler(new Action<MqttApplicationMessageReceivedEventArgs>(Test));

            _client.UseDisconnectedHandler(async e =>
            {
                Console.WriteLine("### Disconnected from server ###");
                await ReConnect();
            });
        }

        private void Test(MqttApplicationMessageReceivedEventArgs e)
        {
            _openDTUData.Parse(e.ApplicationMessage.Topic, Encoding.UTF8.GetString(e.ApplicationMessage.Payload));

            /*
                        string topic = e.ApplicationMessage.Topic;
                        string payLoad = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);




                        bool parsed = false;
                        foreach (KeyValuePair<string, Item> kvp in _openDTUData.items)
                        {
                            if (topic.Equals(kvp.Value.topic))
                            {
                                kvp.Value.value = payLoad;
                                parsed = true;
                            }
                        }

                        if (!parsed)
                        {
                            string[] subTopic = topic.Split('/');
                            if (subTopic[0].Equals("solar")) // make sure we are listening to the solar topic
                            {
                                System.Int128 serialNumber;

                                if (Int128.TryParse(subTopic[1], out serialNumber))
                                {
                                    _openDTUData.AddInverter(subTopic[1]);  //@@@@@ This doesnt work, because now the order in which the inverters are in the list is random!!!
                                    parsed = true;
                                }
                            }
                        }
                        //if (!parsed)
                        {
                            Console.WriteLine(topic + " --> " + payLoad);
                        }


                        /*



                        Console.WriteLine();
                        Console.WriteLine("# Received application message #");
                        Console.WriteLine($"From topic: {topic}");
                        Console.WriteLine($"Payload: {payLoad}");
                        Console.WriteLine($"Qos: {e.ApplicationMessage.QualityOfServiceLevel}");
                        Console.WriteLine($"Retain: {e.ApplicationMessage.Retain}");
                        Console.WriteLine();
                        */
        }
    }
}
