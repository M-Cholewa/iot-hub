#pragma once

#include "App/MQTT/Telemetry/BaseTelemetry.hpp"

class AQITelemetry : public BaseTelemetry<uint8_t>
{
public:
    AQITelemetry(uint8_t value) : BaseTelemetry(value, "AQI", "") {}
};