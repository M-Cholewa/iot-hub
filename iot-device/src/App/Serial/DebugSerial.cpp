#include "DebugSerial.hpp"

void DebugSerial::Setup(long baud)
{
    Serial.begin(baud);
    PCSerialInit = INITIALIZED;
}

HardwareSerial *DebugSerial::Get()
{
    if (PCSerialInit == NOT_INITIALIZED)
    {
        Setup();
    }

    return &Serial;
}

Initialization DebugSerial::PCSerialInit = NOT_INITIALIZED;
