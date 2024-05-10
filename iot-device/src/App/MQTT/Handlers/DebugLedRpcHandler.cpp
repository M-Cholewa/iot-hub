#include "IRpcHandler.hpp"
#include "App/Serial/DebugSerial.hpp"
#include "App/DebugLed/DebugLed.hpp"
#include "nlohmann/json.hpp"

using json = nlohmann::json;
#define Nameof(x) #x

class DebugLedMessageRequest
{
public:
    DebugLedMessageRequest(const json &j)
    {
        j.at("On").get_to(On);
    };

    bool On;
};

class DebugLedMessageResponse
{
private:
    string Message;

public:
    DebugLedMessageResponse(string message)
    {
        Message = message;
    };
    json to_json()
    {
        return json{
            {Nameof(Message), Message},
        };
    }
};

class DebugLedRpcHandler : public IRpcHandler
{
public:
    string Handle(string message) override
    {
        DebugSerial::Get()->println("Executing DebugLedRpcHandler");

        auto j = json::parse(message);
        DebugLedMessageRequest request(j);

        if (request.On)
        {
            DebugLed::GetInstance()->On();
            DebugLedMessageResponse response("DebugLed set to on");

            return response.to_json().dump();
        }
        else
        {
            DebugLed::GetInstance()->Off();
            DebugLedMessageResponse response("DebugLed set to off");

            return response.to_json().dump();
        }
    }

    string Help() override
    {
        return "debugLed: (On: bool) \n\tTurns debug led on or off. \n\tExample: {\"On\": true} or {\"On\": false}";
    }
};
