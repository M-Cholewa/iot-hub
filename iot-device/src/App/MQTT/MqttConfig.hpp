#pragma once

#include <string>

using namespace std;

class MqttConfig
{
private:
    MqttConfig() {}
    static const string ApiKey; // populate with yout API key. You can get it from the device page in the dashboard

public:
    static const string RpcTopicBase;
    static const string TelemetryTopic;
    static const string LogTopic;

    static const string DeviceIdString;
    static const string MqttLoginString;
    static const string MqttPassString;
    static const string MqttServerString;

    static const char *DeviceId;
    static const char *MqttLogin;
    static const char *MqttPass;
    static const char *MqttServer;

    static string GetRpcResponseTopicName(char *inQueue)
    {
        return string(inQueue) + "/response";
    }
};

const string MqttConfig::ApiKey = "ClientID=d201ab30-a86e-41af-8a4d-7589109c7fa8;User=NFG^QNpaZ0I9C4c/)6aPhja!a0&5IA;Pass=@DqgVl(9HlayVh&W!%3tMl*QTurxk65R2@aa/c:>$|jX1>mk|v+eBE!_JuO(;Server=192.168.100.110;";

const string MqttConfig::DeviceIdString = MqttConfig::ApiKey.substr(9, 36);
const string MqttConfig::MqttLoginString = MqttConfig::ApiKey.substr(51, 30);
const string MqttConfig::MqttPassString = MqttConfig::ApiKey.substr(87, 60);
const string MqttConfig::MqttServerString = MqttConfig::ApiKey.substr(155, 15);

const char *MqttConfig::DeviceId = MqttConfig::DeviceIdString.c_str();
const char *MqttConfig::MqttLogin = MqttConfig::MqttLoginString.c_str();
const char *MqttConfig::MqttPass = MqttConfig::MqttPassString.c_str();
const char *MqttConfig::MqttServer = MqttConfig::MqttServerString.c_str();

const string MqttConfig::TelemetryTopic = "telemetry_queue/" + string(MqttConfig::DeviceId) + "/telemetry";
const string MqttConfig::LogTopic = "telemetry_queue/" + string(MqttConfig::DeviceId) + "/log";
const string MqttConfig::RpcTopicBase = "MQTTnet.RPC/+/" + string(MqttConfig::DeviceId) + ".";
