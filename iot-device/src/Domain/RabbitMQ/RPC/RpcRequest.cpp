#include <iostream>
#include "nlohmann/json.hpp"
using json = nlohmann::json;
using namespace std;

#define Nameof(x) #x

class RpcRequest
{
public:
    string ReplyTo;
    string Message;
    string CorelationId;

    RpcRequest(string replyTo, string message, string corelationId)
    {
        ReplyTo = replyTo;
        Message = message;
        CorelationId = corelationId;
    }

    RpcRequest(const json &j)
    {
        j.at("ReplyTo").get_to(ReplyTo);
        j.at("Message").get_to(Message);
        j.at("CorelationId").get_to(CorelationId);
    }

    json to_json()
    {
        return json{{Nameof(ReplyTo), ReplyTo}, {Nameof(Message), Message}, {Nameof(CorelationId), CorelationId}};
    }
};
