#pragma once

#include "App/MQTT/Telemetry/BaseTelemetry.hpp"

class TemperatureTelemetry : public BaseTelemetry<float>
{
public:
    TemperatureTelemetry(float value) : BaseTelemetry(value, "Temperature", "°C") {}
};