#pragma once

#include "App/MQTT/Telemetry/BaseTelemetry.hpp"

class ECO2Telemetry : public BaseTelemetry<uint16_t>
{
public:
    ECO2Telemetry(uint16_t value) : BaseTelemetry(value, "ECO2", "ppm") {}
};