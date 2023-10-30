/*********
  Rui Santos
  Complete project details at https://randomnerdtutorials.com
*********/

// #include <WiFi.h>
// #include <PubSubClient.h>
// #include <Wire.h>
// #include "string.h"
// #include "nlohmann/json.hpp"
// #include "App/MQTT/RPC/RpcRequest.cpp"
// #include "App/MQTT/RPC/RpcResponse.cpp"
// #include "App/MQTT/MqttConfig.hpp"
// #include "App/WIFI/WifiConfig.hpp"

#include "App/Core/WifiTask.hpp"
#include "App/Core/MqttTask.hpp"
#include "App/Serial/DebugSerial.hpp"

// using json = nlohmann::json;
using namespace std;

// WiFiClient espClient;
// PubSubClient client(espClient);
long lastMsg = 0;

// void setup_wifi();
// void callback(char *topic, byte *message, unsigned int length);

// LED Pin
const int ledPin = 4;

WifiTask wifiTask = WifiTask();
MqttTask mqttTask = MqttTask(wifiTask.GetWifiClient());

void setup()
{
  // Serial.begin(921600);
  // setup_wifi();
  // client.setServer(MqttConfig::MqttServer, 1883);
  // client.setCallback(callback);

  // setup debug serial
  DebugSerial::Setup(921600);

  // setup wifi
  wifiTask.Setup();

  // setup mqtt
  mqttTask.Setup();

  pinMode(ledPin, OUTPUT);

  DebugSerial::Get()->print("Setup complete");
}

// void setup_wifi()
// {
//   delay(10);
//   // We start by connecting to a WiFi network
//   Serial.println();
//   Serial.print("Connecting to ");
//   Serial.println(WifiConfig::SSID);

//   WiFi.begin(WifiConfig::SSID, WifiConfig::Password);

//   while (WiFi.status() != WL_CONNECTED)
//   {
//     delay(500);
//     Serial.print(".");
//   }

//   Serial.println("");
//   Serial.println("WiFi connected");
//   Serial.println("IP address: ");
//   Serial.println(WiFi.localIP());
// }

// void callback(char *topic, byte *message, unsigned int length)
// {
//   Serial.print("Message arrived on topic: ");
//   Serial.print(topic);
//   Serial.print(". Message: ");
//   String messageTemp;

//   for (int i = 0; i < length; i++)
//   {
//     messageTemp += (char)message[i];
//   }

//   try
//   {
//     auto j = json::parse(messageTemp);

//     // conversion: json -> RpcRequest
//     auto rpcRequest = RpcRequest(j);

//     // do weird things to send string to serial
//     string reqString = string("ArgumentsJson: " + rpcRequest.ArgumentsJson);

//     Serial.print(String(reqString.c_str()));

//     Serial.println();

//     auto it = MqttConfig::TopicHandlerMap.find(topic);

//     if (it != MqttConfig::TopicHandlerMap.end())
//     {
//       auto handlerResult = it->second->Handle(rpcRequest.ArgumentsJson);
//       RpcResponse rpcResponse = RpcResponse(handlerResult);

//       string responseStringJson = rpcResponse
//                                       .to_json()
//                                       .dump();

//       const char *resTopicCharArray = MqttConfig::GetRpcResponseTopicName(topic).c_str();
//       const char *resStringJsonCharArray = responseStringJson.c_str();

//       client.publish(resTopicCharArray, resStringJsonCharArray);
//     }
//     else
//     {
//       Serial.print("No handler found for RPC function: ");
//       Serial.print(topic);
//       Serial.print(". Exiting callback");
//       Serial.println();
//     }
//   }
//   catch (exception ex)
//   {
//     Serial.print("EXCEPTON: - ");
//     Serial.print(ex.what());
//     Serial.println();
//   }
// }

// void reconnect()
// {
//   // Loop until we're reconnected
//   while (!client.connected())
//   {
//     Serial.print("Attempting MQTT connection...");
//     // Attempt to connect
//     if (client.connect(MqttConfig::DeviceId, MqttConfig::MqttLogin, MqttConfig::MqttPass))
//     {
//       Serial.println("connected");
//       // Subscribe
//       for (auto const &x : MqttConfig::TopicHandlerMap)
//       {
//         client.subscribe(x.first.c_str()); // first -> key
//       }
//     }
//     else
//     {
//       Serial.print("failed, rc=");
//       Serial.print(client.state());
//       Serial.println(" try again in 5 seconds");
//       // Wait 5 seconds before retrying
//       delay(5000);
//     }
//   }
// }

void loop()
{
  // if (!client.connected())
  // {
  //   reconnect();
  // }
  // client.loop();

  wifiTask.Loop();
  mqttTask.Loop();

  long now = millis();
  if (now - lastMsg > 5000)
  {
    lastMsg = now;

    // Temperature in Celsius
    // Uncomment the next line to set temperature in Fahrenheit
    // (and comment the previous temperature line)
    // temperature = 1.8 * bme.readTemperature() + 32; // Temperature in Fahrenheit

    // Convert the value to a char array

    float temperature = 21.37;
    char tempString[8];
    dtostrf(temperature, 1, 2, tempString);

    mqttTask.PushTelemetryMessage(string(tempString));
    // Serial.print("Temperature: ");
    // Serial.println(tempString);

    // client.publish(MqttConfig::TelemetryTopic, tempString);
  }
}