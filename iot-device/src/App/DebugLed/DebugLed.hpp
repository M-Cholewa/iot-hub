#pragma once

#define DEBUG_LED 2

class DebugLed
{
protected:
    DebugLed();
    static DebugLed *_instance;

public:
    static DebugLed *GetInstance();
    void On();
    void Off();
};