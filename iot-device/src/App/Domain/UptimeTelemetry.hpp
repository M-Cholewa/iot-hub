#pragma once

#include "App/Domain/BaseTelemetry.hpp"

class UptimeTelemetry : public BaseTelemetry<unsigned long>
{
public:
    UptimeTelemetry(unsigned long value) : BaseTelemetry(value, "UptimeSTelemetry", "s") {}
};