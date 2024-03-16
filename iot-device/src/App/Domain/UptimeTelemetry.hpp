#pragma once

#include "App/Domain/BaseTelemetry.hpp"

class UptimeTelemetry : public BaseTelemetry<uint32_t>
{
public:
    UptimeTelemetry(uint32_t value) : BaseTelemetry(value, "UptimeSTelemetry", "") {}
};