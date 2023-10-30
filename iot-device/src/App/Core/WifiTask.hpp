#pragma once
#include <WiFi.h>

class WifiTask
{
private:
    WiFiClient espClient;
    void connect();

public:
    ~WifiTask();
    WiFiClass GetWifi();
    WiFiClient &GetWifiClient();
    void Setup();
    void Loop();
};