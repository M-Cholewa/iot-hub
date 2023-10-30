#pragma once
#include <WiFi.h>
#include <PubSubClient.h>
#include <map>
#include <vector>
#include "App/MQTT/RPC/Handlers/IRpcHandler.hpp"

using namespace std;

class MqttTask
{
private:
    PubSubClient client;
    vector<string> telemetryMessages;
    void callback(char *topic, byte *message, unsigned int length);
    void reconnect();
    static const std::map<string, std::shared_ptr<IRpcHandler>> topicHandlerMap;

public:
    MqttTask(WiFiClient &wifiClient);
    void PushTelemetryMessage(string jsonMessage);
    void Setup();
    void Loop();
};