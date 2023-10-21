/*********
  Rui Santos
  Complete project details at https://randomnerdtutorials.com
*********/

#include <WiFi.h>
#include <PubSubClient.h>
#include <Wire.h>
#include "nlohmann/json.hpp"
#include "Domain/RabbitMQ/RPC/RpcRequest.cpp"
#include "Domain/RabbitMQ/RPC/RpcResponse.cpp"

using json = nlohmann::json;
using namespace std;

// Replace the next variables with your SSID/Password combination
const char *ssid = "Bestconnect_184";
const char *password = "456123789";
const char *mqtt_server = "192.168.100.110";
const char *deviceid = "esp-32-device";

WiFiClient espClient;
PubSubClient client(espClient);
long lastMsg = 0;
char msg[50];
int value = 0;

void setup_wifi();
void callback(char *topic, byte *message, unsigned int length);

// LED Pin
const int ledPin = 4;

void setup()
{
  Serial.begin(921600);
  setup_wifi();
  client.setServer(mqtt_server, 1883);
  client.setCallback(callback);

  pinMode(ledPin, OUTPUT);
}

void setup_wifi()
{
  delay(10);
  // We start by connecting to a WiFi network
  Serial.println();
  Serial.print("Connecting to ");
  Serial.println(ssid);

  WiFi.begin(ssid, password);

  while (WiFi.status() != WL_CONNECTED)
  {
    delay(500);
    Serial.print(".");
  }

  Serial.println("");
  Serial.println("WiFi connected");
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());
}

void callback(char *topic, byte *message, unsigned int length)
{
  Serial.print("Message arrived on topic: ");
  Serial.print(topic);
  Serial.print(". Message: ");
  String messageTemp;

  for (int i = 0; i < length; i++)
  {
    messageTemp += (char)message[i];
  }

  try
  {
    auto j = json::parse(messageTemp);

    // conversion: json -> Room
    auto rpcRequest = RpcRequest(j);

    // do weird things to send string to serial
    String reqString = String(string("  >> CorelationId: " + rpcRequest.CorelationId +
                                     ", MethodName: " + rpcRequest.MethodName +
                                     ", ArgumentsJson: " + rpcRequest.ArgumentsJson +
                                     ", ReplyTo: " + rpcRequest.ReplyTo)
                                  .c_str());

    Serial.print(reqString);

    Serial.println();

    auto rpcResponse = RpcResponse(rpcRequest.MethodName, rpcRequest.CorelationId, "{\"Hello\":1}");
    string responseStringJson = rpcResponse.to_json().dump();

    client.publish(rpcRequest.ReplyTo.c_str(), responseStringJson.c_str());
  }
  catch (exception ex)
  {
    Serial.print("EXCEPTON: - ");
    Serial.print(ex.what());
    Serial.println();
  }
}

void reconnect()
{
  // Loop until we're reconnected
  while (!client.connected())
  {
    Serial.print("Attempting MQTT connection...");
    // Attempt to connect
    if (client.connect("ESP8266Client", "mqtt-test", "mqtt-test"))
    {
      Serial.println("connected");
      // Subscribe
      client.subscribe(deviceid);
    }
    else
    {
      Serial.print("failed, rc=");
      Serial.print(client.state());
      Serial.println(" try again in 5 seconds");
      // Wait 5 seconds before retrying
      delay(5000);
    }
  }
}
void loop()
{
  if (!client.connected())
  {
    reconnect();
  }
  client.loop();

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
    Serial.print("Temperature: ");
    Serial.println(tempString);
    client.publish("telemetry_queue", tempString);
  }
}