#include "App/Core/WifiTask.hpp"
#include "App/Core/MqttTask.hpp"
#include "App/Core/ENS160_AHT21Task.hpp"

#include "App/Serial/DebugSerial.hpp"

#include "App/MQTT/Telemetry/LogTelemetry.hpp"
#include "App/MQTT/Telemetry/FirmwareVersionTelemetry.hpp"
#include "App/MQTT/Telemetry/UptimeTelemetry.hpp"

#include <uptime.h>

#include <SPI.h>
#include <Wire.h>
#include <Adafruit_AHTX0.h>
#include "ScioSense_ENS160.h"

using namespace std;

long lastMsg = 0;

WifiTask wifiTask = WifiTask();
MqttTask mqttTask = MqttTask(wifiTask.GetWifiClient());
ENS160_AHT21Task ens160AHT21Task = ENS160_AHT21Task();

void setup()
{
  // setup debug serial
  DebugSerial::Setup(921600);

  // setup wifi
  wifiTask.Setup();

  // setup mqtt
  mqttTask.Setup();

  // setup ENS160_AHT21Task
  ens160AHT21Task.Setup();

  DebugSerial::Get()->print("Setup complete");
}

void loop()
{
  uptime::calculateUptime();
  wifiTask.Loop();
  mqttTask.Loop();
  ens160AHT21Task.Loop();

  long now = millis();
  if (now - lastMsg > 25000)
  {
    lastMsg = now;

    FirmwareVersionTelemetry firmwareVersionTelemetry = FirmwareVersionTelemetry("1.0.0");
    UptimeTelemetry uptimeTelemetry = UptimeTelemetry(uptime::getSeconds());

    LogTelemetry logTelemetry = LogTelemetry("test", "Warning");

    mqttTask.PushTelemetryMessage(&ens160AHT21Task.aqiTelemetry);
    mqttTask.PushTelemetryMessage(&ens160AHT21Task.eco2Telemetry);
    mqttTask.PushTelemetryMessage(&ens160AHT21Task.tvocTelemetry);
    mqttTask.PushTelemetryMessage(&ens160AHT21Task.humidityTelemetry);
    mqttTask.PushTelemetryMessage(&ens160AHT21Task.temperatureTelemetry);
    mqttTask.PushTelemetryMessage(&firmwareVersionTelemetry);
    mqttTask.PushTelemetryMessage(&uptimeTelemetry);

    mqttTask.PushLogMessage(&logTelemetry);
  }
}