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


        public bool loggingEnabled { set { this._loggingEnabled = value; } }
        public string brokerAddress { set { this._brokerAddress = value; } }
        public int brokerPort { set { this._brokerPort = value; } }
        /*
        public string broadcastAddress { set { this._broadcastAddress = value; } }

        /*
        public string PVOutputSystemID { set { this._PVOutputSystemID = value; } }
        public string PVOutputAPIKey { set { this._PVOutputAPIKey = value; } }
        public string PVOutputRequestURL { set { this._PVOutputRequestURL = value; } }
        public string logFilename { set { this._logFileName = value; } }
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
                    }

                    /*

                    if (this.discoveryComplete == false)
                    {
                        if (this._hostAddress.Equals(""))
                        {
                            WriteToLog("Discovering inverter...");
                            this.hostIPaddress = DiscoverInverters().Result;
                            if (this.hostIPaddress.Equals("")) WriteErrorToLog("No Goodwe inverters found");
                        }
                        else
                        {
                            this.hostIPaddress = this._hostAddress; //@@@ TODO: we may want to find the IP adress via DNS-lookup or something.
                        }
                        this.discoveryComplete = true;
                    }

                    double secondsSindsLastLogEntry = (DateTime.Now - previousLogEntry).TotalSeconds;
                    Console.WriteLine(secondsSindsLastLogEntry.ToString());

                    if (secondsSindsLastLogEntry > _logInterval)
                    {
                        previousLogEntry = DateTime.Now;

                        InverterTelemetry telemetry = null;
                        if (!this.hostIPaddress.Equals(""))
                        {
                            telemetry = ReadTelemetry().Result;
                        }

                        if (telemetry != null)
                        {
                            // write to screen
                            string? serialized = JsonSerializer.Serialize(telemetry, new JsonSerializerOptions { WriteIndented = true });
                            WriteToLog(serialized);

                            //write to PvOutput
                            if ((!this._PVOutputSystemID.Equals("")) &&
                                (!this._PVOutputAPIKey.Equals("")) &&
                                (!this._PVOutputRequestURL.Equals("")))
                            {
                                string responseString = PostToPvOutput(telemetry).Result;
                                WriteToLog(responseString);
                            }
                        }
                        else // if we do not have a response, make an empty reponse with the current time stamp, so that we can log it anyway to file.
                        {
                            telemetry = new InverterTelemetry
                            {
                                Timestamp = DateTime.Now,
                                ResponseIp = this.hostIPaddress,
                                Status = InverterTelemetry.InverterStatus.Off
                            };
                        }

                        if (this._logFileName != null)
                            GoodweLib.FileLogger.WriteToFile(this._logFileName, telemetry);
                    }

                    if ((secondsSindsLastLogEntry >= 0) && (secondsSindsLastLogEntry < (_logInterval * 2))) // very first LogEntry the value for the previous time is rubbish
                    {
                        int percentage = Convert.ToInt32((100 * secondsSindsLastLogEntry) / _logInterval);
                        if (percentage < 0) percentage = 0; // due to small rounding issues the percentage may be <0 or >100
                        if (percentage > 100) percentage = 100;
                        SetProgressBar(percentage);
                    }
                    */

                    Thread.Sleep(1);
                }
                else
                {
                    //this.discoveryComplete = false;
                }
                Thread.Sleep(1);
            }
            workerHasClosed = true;
        }

        /*
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

        */

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

        private void ConnectedHandler(MqttClientConnectedEventArgs e)
        {
            return;
        }

        private void MessageHandler(MqttApplicationMessageReceivedEventArgs e)
        {
            string topic = e.ApplicationMessage.Topic;
            string payload = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);

            this.form.WriteToLog(topic + " : " + payload);
            _openDTUData.Parse(topic, payload);
            return;
        }

        private void DisconnectedHandler(MqttClientDisconnectedEventArgs e)
        {
            return;
        }


    }
}
