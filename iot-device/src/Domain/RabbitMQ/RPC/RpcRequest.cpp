#include <iostream>
#include "nlohmann/json.hpp"
using json = nlohmann::json;
using namespace std;

#define Nameof(x) #x

class RpcRequest
{
public:
    string ReplyTo;
    string MethodName;
    string ArgumentsJson;
    string CorelationId;

    RpcRequest(string replyTo, string methodName, string argumentsJson, string corelationId)
    {
        ReplyTo = replyTo;
        MethodName = methodName;
        ArgumentsJson = argumentsJson;
        CorelationId = corelationId;
    }

    RpcRequest(const json &j)
    {
        j.at("ReplyTo").get_to(ReplyTo);
        j.at("MethodName").get_to(MethodName);
        j.at("ArgumentsJson").get_to(ArgumentsJson);
        j.at("CorelationId").get_to(CorelationId);
    }

    json to_json()
    {
        return json{{Nameof(ReplyTo), ReplyTo}, {Nameof(MethodName), MethodName}, {Nameof(ArgumentsJson), ArgumentsJson}, {Nameof(CorelationId), CorelationId}};
    }
};
