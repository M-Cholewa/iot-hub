#pragma once

#include "App/MQTT/Telemetry/BaseTelemetry.hpp"

class TVOCTelemetry : public BaseTelemetry<uint16_t>
{
public:
    TVOCTelemetry(uint16_t value) : BaseTelemetry(value, "TVOC", "ppb") {}
};