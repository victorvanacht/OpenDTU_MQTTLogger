using MQTTnet;
using MQTTnet.Client.Connecting;
using MQTTnet.Client.Disconnecting;
using MQTTnet.Client.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static OpenDTU_MQTTLogger.MQTTClient;

namespace OpenDTU_MQTTLogger
{
    internal class Worker
    {
        public volatile bool workerShouldClose;
        public volatile bool workerHasClosed;

        private bool _loggingEnabled;
        private string _brokerAddress;
        private int _brokerPort;
        private string _logFilename;


        public bool loggingEnabled { set { this._loggingEnabled = value; } }
        public string brokerAddress { set { this._brokerAddress = value; } }
        public int brokerPort { set { this._brokerPort = value; } }
        public string logFilename { set { this._logFilename = value; } }
        /*
        public string broadcastAddress { set { this._broadcastAddress = value; } }

        /*
        public string PVOutputSystemID { set { this._PVOutputSystemID = value; } }
        public string PVOutputAPIKey { set { this._PVOutputAPIKey = value; } }
        public string PVOutputRequestURL { set { this._PVOutputRequestURL = value; } }
        
        public int logInterval { set { this._logInterval = value; } }
        */

        private Thread workerThread;
        private OpenDTU_MQTTLogger form;
        
        private OpenDTUData _openDTUData;
        private MQTTClient _mqttClient;

        private bool IsConnected;

        /*
        private volatile bool _loggingEnabled;
        private volatile string _hostAddress;
        private volatile string _broadcastAddress;
        private volatile string _PVOutputSystemID;
        private volatile string _PVOutputAPIKey;
        private volatile string _PVOutputRequestURL;
        private volatile string _logFileName;
        private volatile int _logInterval;

        private bool discoveryComplete;
        private GoodwePoller poller;
        private string hostIPaddress;
        private PvOutput pvOutput;
        */

        public Worker(OpenDTU_MQTTLogger form)
        {
            this.form = form;
            _openDTUData = new OpenDTUData();


            this.workerShouldClose = false;
            this.workerHasClosed = false;
            this.loggingEnabled = false;
            this.IsConnected = false;

            /*
            this.discoveryComplete = false;

            this.poller = new GoodwePoller(TimeSpan.FromSeconds(3));
            this.pvOutput = new GoodweLib.PvOutput();
            */


            this.workerThread = new Thread(WorkerThread);
            this.workerThread.IsBackground = true;
            this.workerThread.Start();
        }


        // returns true if closed successfully
        public bool Close(int maximumWaitingSeconds)
        {
            this.workerShouldClose = true;
            DateTime t0 = DateTime.Now;
            while ((this.workerHasClosed == false) && (DateTime.Now - t0).TotalSeconds < maximumWaitingSeconds)
            {
                Thread.Sleep(10);
            }
            return this.workerHasClosed;
        }


        private bool csvHeaderChecked;
        private DateTime nextEvent;
        private void WorkerThread()
        {
            DateTime previousLogEntry = new DateTime(1900, 1, 1); // a date long ago

            while (workerShouldClose == false)
            {
                if (_loggingEnabled == true)
                {
                    if (!this.IsConnected)
                    {
                        // first get connected 
                        ConnectToMQTT(this._brokerAddress, this._brokerPort);
                        this.IsConnected = true;

                        csvHeaderChecked = false;
                        nextEvent = DateTime.Now + new TimeSpan(0, 0, 30); // 30 seconds. for start-up and getting all information.
                    }

                    if (this.IsConnected)
                    {
                        if (DateTime.Now > nextEvent)
                        {
                            if (!csvHeaderChecked)
                            {
                                bool fileDoesExist = File.Exists(_logFilename);
                                if (fileDoesExist)
                                { // the file exists, let's see if the header is correct
                                    using (StreamReader reader = new StreamReader(_logFilename))
                                    {
                                        string line = reader.ReadLine();
                                        if (!line.Equals(_openDTUData.CsvFileHeader()))
                                        {
                                            // we dont actually to delete the file. Just overwriting the header will do as well.
                                            fileDoesExist = false;
                                        }
                                    }
                                }

                                if (!fileDoesExist)
                                {
                                    // we should write a (new) file header
                                    using (StreamWriter writer = new StreamWriter(_logFilename, false))
                                    {
                                        writer.WriteLine(_openDTUData.CsvFileHeader());
                                    }
                                    csvHeaderChecked = true;
                                }
                            }

                            // write a line of data
                            using (StreamWriter writer = new StreamWriter(_logFilename, true))
                            {
                                writer.WriteLine(_openDTUData.CsvFileData());
                            }

                            nextEvent = DateTime.Now + new TimeSpan(0, 0, 300); // 300 seconds.
                        }
                    }
                }
                else
                {
                    if (this.IsConnected)
                    { // we still need to disconnect!
                        DisconnectFromMQTT();
                        this.IsConnected = false;
                    }
                }
                Thread.Sleep(10);
            }
            workerHasClosed = true;
        }

        private void SetProgressBar(int progress)
        {
            this.form.SetProgressBar(progress);
        }

        private void WriteToLog(string text)
        {
            this.form.WriteToLog(text);
        }

        private void WriteErrorToLog(string text)
        {
            this.WriteToLog("ERROR - " + text);
        }

        private async void ConnectToMQTT(string broker, int port)
        {
            await ConnectAsync(broker, port);
        }

        private async Task ConnectAsync(string broker, int port)
        {
            IMqttClientOptions options = new MqttClientOptionsBuilder()
                .WithClientId("OpenDTU-MQTTLogger")
                .WithTcpServer(broker, port)
                .Build();

            _mqttClient = new MQTTClient(options, this.ConnectedHandler, this.MessageHandler, this.DisconnectedHandler);

            await _mqttClient.KeepConnectedAndSubscribed("solar/#");
        }

        private async void DisconnectFromMQTT()
        {
            await DisconnectAsync();
        }

        private async Task DisconnectAsync()
        {
            await _mqttClient.DisconnectAsync();
        }


        private void ConnectedHandler(MqttClientConnectedEventArgs e)
        {
            return;
        }

        private void MessageHandler(MqttApplicationMessageReceivedEventArgs e)
        {
            string topic = e.ApplicationMessage.Topic;
            string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

            WriteToLog(topic + " : " + payload);
            _openDTUData.Parse(topic, payload);
            return;
        }

        private void DisconnectedHandler(MqttClientDisconnectedEventArgs e)
        {
            return;
        }


    }
}
