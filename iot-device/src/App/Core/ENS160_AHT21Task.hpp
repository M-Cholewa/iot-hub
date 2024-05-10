#pragma once

#include "App/MQTT/Telemetry/BaseTelemetry.hpp"
#include "App/MQTT/Telemetry/ENS160/AQITelemetry.hpp"
#include "App/MQTT/Telemetry/ENS160/ECO2Telemetry.hpp"
#include "App/MQTT/Telemetry/ENS160/TVOCTelemetry.hpp"
#include "App/MQTT/Telemetry/AHT21/HumidityTelemetry.hpp"
#include "App/MQTT/Telemetry/AHT21/TemperatureTelemetry.hpp"

class ENS160_AHT21Task
{
public:
    void Setup();
    void Loop();

    AQITelemetry aqiTelemetry = AQITelemetry(0);
    ECO2Telemetry eco2Telemetry = ECO2Telemetry(0);
    TVOCTelemetry tvocTelemetry = TVOCTelemetry(0);
    HumidityTelemetry humidityTelemetry = HumidityTelemetry(0);
    TemperatureTelemetry temperatureTelemetry = TemperatureTelemetry(0);
};