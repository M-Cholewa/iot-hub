#pragma once

#include <string>

using namespace std;

class MqttConfig
{
private:
    MqttConfig() {}

public:
    static const char *DeviceId;
    static const char *MqttServer;
    static const char *MqttLogin;
    static const char *MqttPass;
    static const string RpcTopicBase;
    static const char *TelemetryTopic;

    static string GetRpcResponseTopicName(char *inQueue)
    {
        return string(inQueue) + "/response";
    }
};

const char *MqttConfig::DeviceId = "esp-32-device";
const char *MqttConfig::MqttServer = "192.168.100.110";
const char *MqttConfig::MqttLogin = "mqtt-test";
const char *MqttConfig::MqttPass = "mqtt-test";
const char *MqttConfig::TelemetryTopic = "telemetry_queue";
const string MqttConfig::RpcTopicBase = "MQTTnet.RPC/+/" + string(MqttConfig::DeviceId) + ".";
