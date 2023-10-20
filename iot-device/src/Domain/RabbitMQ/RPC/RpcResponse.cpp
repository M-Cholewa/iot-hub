#include <string>
using namespace std;

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
};