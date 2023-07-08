using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenDTU_MQTTLogger
{
    internal class OpenDTUData
    {
        public static StringItem IPAddress = new StringItem();
        public static StringItem hostName = new StringItem();
        public static DoubleItem powerAC = new DoubleItem();
        public static DoubleItem eneryTotal = new DoubleItem();
        public static DoubleItem energyDay = new DoubleItem();
        public static DoubleItem powerDC = new DoubleItem();

        public class StringItem
        {
            public string value = "";

            public override string ToString()
            {
                return value;
            }
        }

        public class DoubleItem
        {
            public double value;

            public override string ToString()
            {
                return value.ToString(CultureInfo.InvariantCulture);
            }
        }

        public class ParserBase
        {
            public ParserBase() { }

            public virtual void Parse(string value) { }
        }

        public class ParserVoid : ParserBase // this can be used to just ignore certain topic messages
        {
            public ParserVoid()
            {
            }

            public override void Parse(string value)
            {
            }
        }

        public class ParserString : ParserBase
        {
            private StringItem _var;

            public ParserString(StringItem variable)
            {
                _var = variable;
            }

            public override void Parse(string value)
            {
                _var.value = value;
            }
        }

        public class ParserDouble : ParserBase
        {
            private DoubleItem _var;

            public ParserDouble(DoubleItem variable)
            {
                _var = variable;
            }

            public override void Parse(string value)
            {
                Double.TryParse(value, CultureInfo.InvariantCulture, out _var.value);
            }
        }

        public class InverterChannel
        {
            public int channelNumber;
            public StringItem name = new StringItem();
            public DoubleItem current = new DoubleItem();
            public DoubleItem voltage = new DoubleItem();
            public DoubleItem power = new DoubleItem();
            public DoubleItem energyTotal = new DoubleItem();
            public DoubleItem energyDay = new DoubleItem();

            public InverterChannel(int channelNumber)
            {
                this.channelNumber = channelNumber;
            }
        }

        public class Inverter
        {
            public StringItem name = new StringItem();
            public StringItem serialNumber = new StringItem();

            public DoubleItem currentAC = new DoubleItem();
            public DoubleItem voltageAC = new DoubleItem();
            public DoubleItem frequency = new DoubleItem();
            public DoubleItem powerAC = new DoubleItem();
            public DoubleItem powerDC = new DoubleItem();
            public DoubleItem temperature = new DoubleItem();
            public DoubleItem energyTotal = new DoubleItem();
            public DoubleItem energyDay = new DoubleItem();

            public SortedDictionary<int, InverterChannel> channel = new SortedDictionary<int, InverterChannel>();

            public Inverter(string serialNumber)
            {
                this.serialNumber.value = serialNumber;
            }
        }

        private static Dictionary<string, ParserBase> messages = new Dictionary<string, ParserBase>
        {
            ["solar/dtu/ip"] = new ParserString(IPAddress),
            ["solar/dtu/hostname"] = new ParserString(hostName),
            ["solar/dtu/rssi"] = new ParserVoid(),
            ["solar/dtu/status"] = new ParserVoid(),
            ["solar/dtu/uptime"] = new ParserVoid(),
            ["solar/ac/power"] = new ParserDouble(powerAC),
            ["solar/ac/yieldtotal"] = new ParserDouble(eneryTotal),
            ["solar/ac/yieldday"] = new ParserDouble(energyDay),
            ["solar/ac/is_valid"] = new ParserVoid(),
            ["solar/dc/power"] = new ParserDouble(powerDC),
            ["solar/dc/irradiation"] = new ParserVoid(),
            ["solar/dc/is_valid"] = new ParserVoid()
        };

        public static SortedDictionary<string, Inverter> inverter = new SortedDictionary<string, Inverter>();

        public OpenDTUData()
        {
        }

        private static volatile bool alreadyParsing = false;
        public void Parse(string topic, string value)
        {
            if (!alreadyParsing)
            {
                alreadyParsing = true;

                if (messages.ContainsKey(topic))
                {
                    messages[topic].Parse(value);
                }
                else
                {
                    // see if the sub-topic is a number
                    string[] subTopic = topic.Split('/');
                    if (subTopic[0].Equals("solar") && (subTopic.Length > 1)) // make sure we are listening to the solar topic
                    {
                        System.Int128 serialNumber;
                        if (Int128.TryParse(subTopic[1], out serialNumber))
                        {
                            string inverterSerial = subTopic[1];

                            // yes it is a number! it is about an inverter!
                            // see if we already have seen it before
                            if (!inverter.ContainsKey(inverterSerial))
                            {
                                // no we have not seen it before. We must add a new inverter to our list!
                                inverter.Add(inverterSerial, new Inverter(inverterSerial));

                                // and add the topic messages for this inverter
                                string solarInverterSerial = "solar/" + inverterSerial + "/";
                                messages.Add(solarInverterSerial + "name", new ParserString(inverter[inverterSerial].name));
                                messages.Add(solarInverterSerial + "device/bootloaderversion", new ParserVoid());
                                messages.Add(solarInverterSerial + "device/fwbuildversion", new ParserVoid());
                                messages.Add(solarInverterSerial + "device/fwbuilddatetime", new ParserVoid());
                                messages.Add(solarInverterSerial + "device/hwpartnumber", new ParserVoid());
                                messages.Add(solarInverterSerial + "device/hwversion", new ParserVoid());
                                messages.Add(solarInverterSerial + "status/reachable", new ParserVoid());
                                messages.Add(solarInverterSerial + "status/producing", new ParserVoid());
                                messages.Add(solarInverterSerial + "status/last_update", new ParserVoid());
                                messages.Add(solarInverterSerial + "status/limit_relative", new ParserVoid());
                                messages.Add(solarInverterSerial + "status/limit_absolute", new ParserVoid());

                                messages.Add(solarInverterSerial + "0/current", new ParserDouble(inverter[inverterSerial].currentAC));
                                messages.Add(solarInverterSerial + "0/voltage", new ParserDouble(inverter[inverterSerial].voltageAC));
                                messages.Add(solarInverterSerial + "0/frequency", new ParserDouble(inverter[inverterSerial].frequency));
                                messages.Add(solarInverterSerial + "0/power", new ParserDouble(inverter[inverterSerial].powerAC));
                                messages.Add(solarInverterSerial + "0/powerdc", new ParserDouble(inverter[inverterSerial].powerDC));
                                messages.Add(solarInverterSerial + "0/temperature", new ParserDouble(inverter[inverterSerial].temperature));
                                messages.Add(solarInverterSerial + "0/yieldtotal", new ParserDouble(inverter[inverterSerial].energyTotal));
                                messages.Add(solarInverterSerial + "0/yieldday", new ParserDouble(inverter[inverterSerial].energyDay));
                                messages.Add(solarInverterSerial + "0/efficiency", new ParserVoid());
                                messages.Add(solarInverterSerial + "0/powerfactor", new ParserVoid());
                                messages.Add(solarInverterSerial + "0/reactivepower", new ParserVoid());
                            }

                            if (subTopic.Length > 2)
                            {
                                int channelNumber;
                                if (Int32.TryParse(subTopic[2], out channelNumber))
                                {
                                    // yes it is a channel
                                    if ((channelNumber > 0) && (channelNumber <= 4))
                                    {
                                        // yes it is an actual channel, not the 0 channel.
                                        // Check if we need to add it to the list!
                                        if (!inverter[inverterSerial].channel.ContainsKey(channelNumber))
                                        {
                                            // the channel is not know yet!
                                            // Let's add it to the list!
                                            inverter[inverterSerial].channel.Add(channelNumber, new InverterChannel(channelNumber));

                                            string solarInverterSerialChannel = "solar/" + inverterSerial + "/" + subTopic[2] + "/";
                                            messages.Add(solarInverterSerialChannel + "name", new ParserString(inverter[inverterSerial].channel[channelNumber].name));
                                            messages.Add(solarInverterSerialChannel + "power", new ParserDouble(inverter[inverterSerial].channel[channelNumber].power));
                                            messages.Add(solarInverterSerialChannel + "voltage", new ParserDouble(inverter[inverterSerial].channel[channelNumber].voltage));
                                            messages.Add(solarInverterSerialChannel + "current", new ParserDouble(inverter[inverterSerial].channel[channelNumber].current));
                                            messages.Add(solarInverterSerialChannel + "yieldtotal", new ParserDouble(inverter[inverterSerial].channel[channelNumber].energyTotal));
                                            messages.Add(solarInverterSerialChannel + "yieldday", new ParserDouble(inverter[inverterSerial].channel[channelNumber].energyDay));
                                            messages.Add(solarInverterSerialChannel + "irradiation", new ParserVoid());
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                alreadyParsing = false;
            }
        }

        public string CsvFileHeader()
        {
            string s = "DateTime, IPAddress, Hostname, PowerAC, EnergyTotal, EnergyDay, PowerDC, ";
            foreach (KeyValuePair<string, Inverter> inverterKvp in inverter)
            {
                s += inverterKvp.Key + ":name, ";
                s += inverterKvp.Key + ":currentAC, ";
                s += inverterKvp.Key + ":voltageAC, ";
                s += inverterKvp.Key + ":frequency, ";
                s += inverterKvp.Key + ":powerAC, ";
                s += inverterKvp.Key + ":powerDC, ";
                s += inverterKvp.Key + ":temperature, ";
                s += inverterKvp.Key + ":energyTotal, ";
                s += inverterKvp.Key + ":energyDay, ";

                foreach (KeyValuePair<int, InverterChannel> channelKvp in inverterKvp.Value.channel)
                {
                    string t = inverterKvp.Key + "[" + channelKvp.Key.ToString() + "]:";
                    s += t + "name, ";
                    s += t + "current, ";
                    s += t + "voltage, ";
                    s += t + "power, ";
                    s += t + "energyTotal, ";
                    s += t + "energyDay, ";
                }
            }
            s = s.Substring(0, s.Length - 2); // remove the last ", ";
            return s;
        }

        public string CsvFileData()
        {
            // "DateTime, IPAddress, Hostname, PowerAC, EnergyTotal, EnergyDay, PowerDC, ";
            string s = DateTime.Now.ToString(CultureInfo.InvariantCulture) + ", ";
            s += IPAddress.ToString() + ", ";
            s += hostName.ToString() + ", ";
            s += powerAC.ToString() + ", ";
            s += eneryTotal.ToString() + ", ";
            s += energyDay.ToString() + ", ";
            s += powerDC.ToString() + ", ";

            foreach (KeyValuePair<string, Inverter> inverterKvp in inverter)
            {
                Inverter inv = inverterKvp.Value;
                s += inv.name.ToString() + ", ";
                s += inv.currentAC.ToString() + ", ";
                s += inv.voltageAC.ToString() + ", ";
                s += inv.frequency.ToString() + ", ";
                s += inv.powerAC.ToString() + ", ";
                s += inv.powerDC.ToString() + ", ";
                s += inv.temperature.ToString() + ", ";
                s += inv.energyTotal.ToString() + ", ";
                s += inv.energyDay.ToString() + ", ";

                foreach (KeyValuePair<int, InverterChannel> channelKvp in inv.channel)
                {
                    InverterChannel ch = channelKvp.Value;
                    s += ch.name.ToString() + ", ";
                    s += ch.current.ToString() + ", ";
                    s += ch.voltage.ToString() + ", ";
                    s += ch.power.ToString() + ", ";
                    s += ch.energyTotal.ToString() + ", ";
                    s += ch.energyDay.ToString() + ", ";
                }
            }
            s = s.Substring(0, s.Length - 2); // remove the last ", ";
            return s;
        }
    }
}
