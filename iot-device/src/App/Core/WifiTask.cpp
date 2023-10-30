#include "App/WIFI/WifiConfig.hpp"
#include "App/Serial/DebugSerial.hpp"
#include "App/Core/WifiTask.hpp"

WifiTask::~WifiTask()
{
    WiFi.disconnect();
    WiFi.~WiFiClass();
}

void WifiTask::Loop()
{
    if (WiFi.status() != WL_CONNECTED)
    {
        connect();
    }
}

void WifiTask::Setup() {}

WiFiClass WifiTask::GetWifi(/* args */)
{
    return WiFi;
}

WiFiClient &WifiTask::GetWifiClient(/* args */)
{
    return espClient;
}

void WifiTask::connect()
{
    DebugSerial::Get()->println();
    DebugSerial::Get()->print("Connecting to ");
    DebugSerial::Get()->println(WifiConfig::SSID);

    WiFi.begin(WifiConfig::SSID, WifiConfig::Password);
    while (WiFi.status() != WL_CONNECTED)
    {
        delay(500);
        DebugSerial::Get()->print(".");
    }

    DebugSerial::Get()->println("");
    DebugSerial::Get()->println("WiFi connected");
    DebugSerial::Get()->println("IP address: ");
    DebugSerial::Get()->println(WiFi.localIP());
}