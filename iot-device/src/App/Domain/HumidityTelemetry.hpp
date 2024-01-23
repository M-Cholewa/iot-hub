#pragma once

#include "App/Domain/BaseTelemetry.hpp"

class HumidityTelemetry : public BaseTelemetry<float>
{
public:
    HumidityTelemetry(float value) : BaseTelemetry(value, "Humidity", "%") {}
};