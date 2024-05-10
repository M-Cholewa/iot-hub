#pragma once

#include "App/MQTT/Telemetry/BaseTelemetry.hpp"

class HumidityTelemetry : public BaseTelemetry<float>
{
public:
    HumidityTelemetry(float value) : BaseTelemetry(value, "Humidity", "% rH") {}
};