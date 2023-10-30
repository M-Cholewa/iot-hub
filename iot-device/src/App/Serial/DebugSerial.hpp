#pragma once

#include <Arduino.h>
#include "Utils/PeripheralUtils.hpp"

class DebugSerial
{
private:
    static Initialization PCSerialInit;
    DebugSerial(){};

public:
    static void Setup(long baud = 921600);
    static HardwareSerial *Get();
};
