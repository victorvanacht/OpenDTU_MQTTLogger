# OpenDTU_MQTTLogger
This Windows application can be used in combination with [OpenDTU](https://github.com/tbnobody/OpenDTU) and an MQTT broker, for example [Eclipse Mosquitto](https://mosquitto.org/).
This application parses messages all of OpenDTU, and writes (relevant) inverter status information to  to a .CSV file at regular intervals.
The generated .CSV logfile can be viewed graphically by [SolarPlot](https://github.com/victorvanacht/SolarPlot) (...once this program has been adapted to also plot OpenDTU data).

## Installation
- After building using Visual Studio 2022, copy the generated binary executables to a suitable location of choise somewhere on your PC.
- Start OpenDTU_MQTTLogger.exe. 
- If an error message appears, install the appropriate dotNet runtime.
- If desired, a shortcut the program can also be placed in the 'auto startup' folder of Windows, so that logging starts immediately after the computer has been restarted.
- To close the program use the menu bar at the top (File-> Exit), because clicking the close button in the top right corner of the window will minimize to the Windows system tray.

## Special thanks
Some of the code is heavily inspired by [this project](https://github.com/s1oen/mqttConsoleClient.git).


