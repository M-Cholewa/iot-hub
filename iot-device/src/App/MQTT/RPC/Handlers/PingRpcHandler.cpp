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
};
