#include <string>
#include "nlohmann/json.hpp"
using json = nlohmann::json;
using namespace std;

#define Nameof(x) #x

class RpcResponse
{
public:
    string Message;
    string CorelationId;
    string ResponseDataJson;

    RpcResponse(string message, string correlationId, string responseDataJson)
    {
        Message = message;
        CorelationId = correlationId;
        ResponseDataJson = responseDataJson;
    }

    RpcResponse(const json &j)
    {
        j.at("Message").get_to(Message);
        j.at("CorelationId").get_to(CorelationId);
        j.at("ResponseDataJson").get_to(ResponseDataJson);
    }

    json to_json()
    {
        return json{
            {Nameof(Message), Message},
            {Nameof(CorelationId), CorelationId},
            {Nameof(ResponseDataJson), ResponseDataJson},
        };
    }
};