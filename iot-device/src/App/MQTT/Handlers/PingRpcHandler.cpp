#include "IRpcHandler.hpp"
#include "App/Serial/DebugSerial.hpp"

class PingRpcHandler : public IRpcHandler
{
private:
    /* data */
public:
    string Handle(string message) override
    {
        DebugSerial::Get()->println("Executing PingRpcHandler");
        return "pong";
    }

    string Help() override
    {
        return "ping: (no arguments) \n\t\treturns pong (used for testing connection to device.";
    }
};
