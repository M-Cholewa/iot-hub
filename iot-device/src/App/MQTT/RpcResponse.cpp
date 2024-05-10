#include <string>
#include "nlohmann/json.hpp"
using json = nlohmann::json;
using namespace std;

#define Nameof(x) #x

class RpcResponse
{
public:
    string ResponseDataJson;

    RpcResponse(string responseDataJson)
    {
        ResponseDataJson = responseDataJson;
    }

    RpcResponse(const json &j)
    {
        j.at("ResponseDataJson").get_to(ResponseDataJson);
    }

    json to_json()
    {
        return json{
            {Nameof(ResponseDataJson), ResponseDataJson},
        };
    }
};