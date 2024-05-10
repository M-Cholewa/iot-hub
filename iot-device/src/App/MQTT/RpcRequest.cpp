#include <iostream>
#include "nlohmann/json.hpp"
using json = nlohmann::json;
using namespace std;

#define Nameof(x) #x

class RpcRequest
{
public:
    string ArgumentsJson;

    RpcRequest(string argumentsJson)
    {
        ArgumentsJson = argumentsJson;
    }

    RpcRequest(const json &j)
    {
        j.at("ArgumentsJson").get_to(ArgumentsJson);
    }

    json to_json()
    {
        return json{{Nameof(ArgumentsJson), ArgumentsJson}};
    }
};
