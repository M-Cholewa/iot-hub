
#include "App/MQTT/MqttConfig.hpp"
#include "App/MQTT/RPC/RpcRequest.cpp"
#include "App/MQTT/RPC/RpcResponse.cpp"
#include "nlohmann/json.hpp"
#include "App/MQTT/RPC/Handlers/PingRpcHandler.cpp"
#include "App/MQTT/RPC/Handlers/DebugLedRpcHandler.cpp"
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

    string messageTemp;

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

        bool _found = false;

        String helpString = String("/") + MqttConfig::DeviceId + String(".help");

        // check if topic ends with help

        if (String(topic).endsWith(helpString))
        {

            _found = true;
            string helpString = string("Help: Available RPC functions: \n\n");

            for (auto const &x : topicHandlerMap)
            {
                helpString += string("====> " + x.second->Help() + "\n\n");
            }

            RpcResponse rpcResponse = RpcResponse(helpString);

            string responseStringJson = rpcResponse
                                            .to_json()
                                            .dump();

            const char *resTopicCharArray = MqttConfig::GetRpcResponseTopicName(topic).c_str();
            const char *resStringJsonCharArray = responseStringJson.c_str();

            client.publish(resTopicCharArray, resStringJsonCharArray);
        }

        // check if topic ends with response

        if (!_found)
        {
            for (auto const &x : topicHandlerMap)
            {

                String topicString = String(topic);
                String topisStringEnd = String("/") + MqttConfig::DeviceId + String(".") + x.first.c_str();

                if (!topicString.endsWith(topisStringEnd))
                {
                    continue;
                }

                _found = true;

                auto handlerResult = x.second->Handle(rpcRequest.ArgumentsJson);
                RpcResponse rpcResponse = RpcResponse(handlerResult);

                string responseStringJson = rpcResponse
                                                .to_json()
                                                .dump();

                const char *resTopicCharArray = MqttConfig::GetRpcResponseTopicName(topic).c_str();
                const char *resStringJsonCharArray = responseStringJson.c_str();

                client.publish(resTopicCharArray, resStringJsonCharArray);
            }
        }

        // if no handler found

        if (!_found)
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
                string topic = string(MqttConfig::RpcTopicBase) + x.first; // first -> key

                DebugSerial::Get()->print("Subscribing to topic: ");
                DebugSerial::Get()->println(topic.c_str());

                client.subscribe(topic.c_str());
            }

            string topic = string(MqttConfig::RpcTopicBase) + "help"; // first -> key

            DebugSerial::Get()->print("Subscribing to topic: ");
            DebugSerial::Get()->println(topic.c_str());
            client.subscribe(topic.c_str());
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

void MqttTask::Loop()
{
    if (!client.connected())
    {
        reconnect();
    }

    if (!telemetryMessages.empty())
    {
        string _dbg = string("TelemetryMessage: " + telemetryMessages.dump());
        DebugSerial::Get()->println(_dbg.c_str());

        client.publish(MqttConfig::TelemetryTopic.c_str(), telemetryMessages.dump().c_str());
        telemetryMessages.clear();
    }

    if (!logMessages.empty())
    {
        string _dbg = string("TelemetryMessage: " + logMessages.dump());
        DebugSerial::Get()->println(_dbg.c_str());

        client.publish(MqttConfig::LogTopic.c_str(), logMessages.dump().c_str());
        logMessages.clear();
    }

    client.loop();
}

// maps incomming message on RPC topic to specific handler
const std::map<string, std::shared_ptr<IRpcHandler>> MqttTask::topicHandlerMap = {
    {"ping", std::make_shared<PingRpcHandler>()},
    {"debugLed", std::make_shared<DebugLedRpcHandler>()}};
