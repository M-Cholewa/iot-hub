#pragma once

#include <string>
#include "nlohmann/json.hpp"

using json = nlohmann::json;
using namespace std;

class LogTelemetry
{
private:
    string message;
    string severity;

public:
    LogTelemetry(string message, string severity)
    {
        this->message = message;
        this->severity = severity;
    }

    json GetTelemetryJson()
    {
        return json{
            {"Message", message},
            {"Severity", severity},
        };
    }
};