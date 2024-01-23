#pragma once

#include <string>
#include "nlohmann/json.hpp"

using json = nlohmann::json;
using namespace std;

template <typename T>
class BaseTelemetry
{

public:
    string fieldName;
    string unit;
    T value;

    virtual json GetTelemetryJson()
    {
        return json{
            {"FieldName", fieldName},
            {"Value", value},
            {"Unit", unit},
        };
    }

    BaseTelemetry(T value, string fieldName, string unit)
    {
        this->value = value;
        this->fieldName = fieldName;
        this->unit = unit;
    }
};