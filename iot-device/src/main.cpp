#include "App/Core/WifiTask.hpp"
#include "App/Core/MqttTask.hpp"
#include "App/Serial/DebugSerial.hpp"

#include "App/Domain/CO2Telemetry.hpp"
#include "App/Domain/HumidityTelemetry.hpp"
#include "App/Domain/TemperatureTelemetry.hpp"
#include "App/Domain/LogTelemetry.hpp"
#include "App/Domain/FirmwareVersionTelemetry.hpp"
#include "App/Domain/UptimeTelemetry.hpp"

#include <uptime.h>

using namespace std;

long lastMsg = 0;

WifiTask wifiTask = WifiTask();
MqttTask mqttTask = MqttTask(wifiTask.GetWifiClient());

void setup()
{
  // setup debug serial
  DebugSerial::Setup(921600);

  // setup wifi
  wifiTask.Setup();

  // setup mqtt
  mqttTask.Setup();

  DebugSerial::Get()->print("Setup complete");
}

void loop()
{
  uptime::calculateUptime();
  wifiTask.Loop();
  mqttTask.Loop();

  long now = millis();
  if (now - lastMsg > 25000)
  {
    lastMsg = now;

    float temperature = 21.37;
    char tempString[8];
    dtostrf(temperature, 1, 2, tempString);

    CO2Telemetry co2Telemetry = CO2Telemetry(temperature);
    HumidityTelemetry humidityTelemetry = HumidityTelemetry(1);
    TemperatureTelemetry temperatureTelemetry = TemperatureTelemetry(2);
    FirmwareVersionTelemetry firmwareVersionTelemetry = FirmwareVersionTelemetry("1.0.0");
    UptimeTelemetry uptimeTelemetry = UptimeTelemetry(uptime::getSeconds());

    LogTelemetry logTelemetry = LogTelemetry("test", "Warning");

    mqttTask.PushTelemetryMessage(&co2Telemetry);
    mqttTask.PushTelemetryMessage(&humidityTelemetry);
    mqttTask.PushTelemetryMessage(&temperatureTelemetry);
    mqttTask.PushTelemetryMessage(&firmwareVersionTelemetry);
    mqttTask.PushTelemetryMessage(&uptimeTelemetry);

    mqttTask.PushLogMessage(&logTelemetry);
  }
}