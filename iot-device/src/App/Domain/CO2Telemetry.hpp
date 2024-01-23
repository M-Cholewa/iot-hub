#pragma once

#include "App/Domain/BaseTelemetry.hpp"

class CO2Telemetry : public BaseTelemetry<float>
{
public:
    CO2Telemetry(float value) : BaseTelemetry(value, "CO2", "%") {}
};