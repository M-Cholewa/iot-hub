#include "App/Core/WifiTask.hpp"
#include "App/Core/MqttTask.hpp"
#include "App/Serial/DebugSerial.hpp"

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
  wifiTask.Loop();
  mqttTask.Loop();

  long now = millis();
  if (now - lastMsg > 5000)
  {
    lastMsg = now;

    float temperature = 21.37;
    char tempString[8];
    dtostrf(temperature, 1, 2, tempString);

    mqttTask.PushTelemetryMessage(string(tempString));
  }
}