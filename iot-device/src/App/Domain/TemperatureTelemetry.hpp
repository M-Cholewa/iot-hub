#pragma once

#include "App/Domain/BaseTelemetry.hpp"

class TemperatureTelemetry : public BaseTelemetry<float>
{
public:
    TemperatureTelemetry(float value) : BaseTelemetry(value, "Temperature", "°C") {}
};