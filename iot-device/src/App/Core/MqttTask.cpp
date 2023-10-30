
#include "App/MQTT/MqttConfig.hpp"
#include "App/MQTT/RPC/RpcRequest.cpp"
#include "App/MQTT/RPC/RpcResponse.cpp"
#include "nlohmann/json.hpp"
#include "App/MQTT/RPC/Handlers/PingRpcHandler.cpp"
#include "App/Serial/DebugSerial.hpp"
#include "App/Core/MqttTask.hpp"

using json = nlohmann::json;
using namespace std;

MqttTask::MqttTask(WiFiClient &wifiClient)
{
    client = PubSubClient(wifiClient);
}

void MqttTask::Setup()
{
    client.setServer(MqttConfig::MqttServer, 1883);
    client.setCallback([this](char *topic, byte *message, unsigned int length)
                       { this->callback(topic, message, length); });
}

void MqttTask::callback(char *topic, byte *message, unsigned int length)
{
    DebugSerial::Get()->print("Message arrived on topic: ");
    DebugSerial::Get()->print(topic);
    DebugSerial::Get()->print(". Message: ");

    String messageTemp;

    for (int i = 0; i < length; i++)
    {
        messageTemp += (char)message[i];
    }

    try
    {
        auto j = json::parse(messageTemp);

        // conversion: json -> RpcRequest
        auto rpcRequest = RpcRequest(j);

        // do weird things to send string to serial
        string reqString = string("ArgumentsJson: " + rpcRequest.ArgumentsJson);

        DebugSerial::Get()->print(String(reqString.c_str()));
        DebugSerial::Get()->println();

        auto it = topicHandlerMap.find(topic);

        if (it != topicHandlerMap.end())
        {
            auto handlerResult = it->second->Handle(rpcRequest.ArgumentsJson);
            RpcResponse rpcResponse = RpcResponse(handlerResult);

            string responseStringJson = rpcResponse
                                            .to_json()
                                            .dump();

            const char *resTopicCharArray = MqttConfig::GetRpcResponseTopicName(topic).c_str();
            const char *resStringJsonCharArray = responseStringJson.c_str();

            client.publish(resTopicCharArray, resStringJsonCharArray);
        }
        else
        {
            DebugSerial::Get()->print("No handler found for RPC function: ");
            DebugSerial::Get()->print(topic);
            DebugSerial::Get()->print(". Exiting callback");
            DebugSerial::Get()->println();
        }
    }
    catch (exception ex)
    {
        DebugSerial::Get()->print("EXCEPTON: - ");
        DebugSerial::Get()->print(ex.what());
        DebugSerial::Get()->println();
    }
}

void MqttTask::reconnect()
{
    // Loop until we're reconnected
    while (!client.connected())
    {
        DebugSerial::Get()->print("Attempting MQTT connection...");
        // Attempt to connect
        if (client.connect(MqttConfig::DeviceId, MqttConfig::MqttLogin, MqttConfig::MqttPass))
        {
            DebugSerial::Get()->println("connected");
            // Subscribe
            for (auto const &x : topicHandlerMap)
            {
                client.subscribe(x.first.c_str()); // first -> key
            }
        }
        else
        {
            DebugSerial::Get()->print("failed, rc=");
            DebugSerial::Get()->print(client.state());
            DebugSerial::Get()->println(" try again in 5 seconds");
            // Wait 5 seconds before retrying
            delay(5000);
        }
    }
}

void MqttTask::PushTelemetryMessage(string jsonMessage)
{
    telemetryMessages.push_back(jsonMessage);
}

void MqttTask::Loop()
{
    if (!client.connected())
    {
        reconnect();
    }

    for (string telemetryMessage : telemetryMessages)
    {
        string _dbg = string("TelemetryMessage: " + telemetryMessage);
        DebugSerial::Get()->println(_dbg.c_str());
        client.publish(MqttConfig::TelemetryTopic, telemetryMessage.c_str());
    }

    telemetryMessages.clear();

    client.loop();
}

// maps incomming message on RPC topic to specific handler
const std::map<string, std::shared_ptr<IRpcHandler>> MqttTask::topicHandlerMap = {
    {MqttConfig::RpcTopicBase + "ping", std::make_shared<PingRpcHandler>()}};
