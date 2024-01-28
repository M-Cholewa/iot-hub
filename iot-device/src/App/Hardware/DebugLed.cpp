#include "App/Hardware/DebugLed.hpp"
#include "Arduino.h"

DebugLed *DebugLed::_instance = nullptr;

DebugLed::DebugLed()
{
    pinMode(DEBUG_LED, OUTPUT);
};

DebugLed *DebugLed::GetInstance()
{
    if (DebugLed::_instance == nullptr)
    {
        DebugLed::_instance = new DebugLed();
    }

    return DebugLed::_instance;
}

void DebugLed::On()
{
    digitalWrite(DEBUG_LED, HIGH);
}

void DebugLed::Off()
{
    digitalWrite(DEBUG_LED, LOW);
}
