#pragma once
#include <WiFi.h>
#include <PubSubClient.h>
#include <map>
#include <vector>
#include "App/MQTT/RPC/Handlers/IRpcHandler.hpp"
#include "App/Domain/BaseTelemetry.hpp"
#include "App/Domain/LogTelemetry.hpp"

using namespace std;

class MqttTask
{
private:
    PubSubClient client;
    json telemetryMessages;
    json logMessages;

    void callback(char *topic, byte *message, unsigned int length);
    void reconnect();
    static const std::map<string, std::shared_ptr<IRpcHandler>> topicHandlerMap;

public:
    MqttTask(WiFiClient &wifiClient);

    template <typename T>
    void PushTelemetryMessage(BaseTelemetry<T> *telemetry)
    {
        telemetryMessages.push_back(telemetry->GetTelemetryJson());
    }

    void PushLogMessage(LogTelemetry *telemetry)
    {
        logMessages.push_back(telemetry->GetTelemetryJson());
    }

    void Setup();
    void Loop();
};