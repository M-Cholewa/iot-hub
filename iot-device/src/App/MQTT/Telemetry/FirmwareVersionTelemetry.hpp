#pragma once

#include "App/MQTT/Telemetry/BaseTelemetry.hpp"

class FirmwareVersionTelemetry : public BaseTelemetry<string>
{
public:
    FirmwareVersionTelemetry(string value) : BaseTelemetry(value, "FirmwareVersion", "") {}
};