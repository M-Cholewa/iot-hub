#pragma once

#include "App/Domain/BaseTelemetry.hpp"

class FirmwareVersionTelemetry : public BaseTelemetry<string>
{
public:
    FirmwareVersionTelemetry(string value) : BaseTelemetry(value, "FirmwareVersion", "") {}
};