#pragma once

#include <string>

using namespace std;

class IRpcHandler
{
public:
    virtual string Handle(string message);
    virtual string Help();
    virtual ~IRpcHandler() {}
};
