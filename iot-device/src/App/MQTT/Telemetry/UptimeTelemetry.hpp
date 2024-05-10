#pragma once

#include "App/MQTT/Telemetry/BaseTelemetry.hpp"

class UptimeTelemetry : public BaseTelemetry<unsigned long>
{
public:
    UptimeTelemetry(unsigned long value) : BaseTelemetry(value, "UptimeS", "s") {}
};