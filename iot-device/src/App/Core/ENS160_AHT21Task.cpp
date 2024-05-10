#include "ENS160_AHT21Task.hpp"
#include <Wire.h>
#include <Adafruit_AHTX0.h>
#include "ScioSense_ENS160.h" // ENS160 library
#include "App/Serial/DebugSerial.hpp"

Adafruit_AHTX0 aht;
ScioSense_ENS160 ens160(ENS160_I2CADDR_1); // 0x53..ENS160+AHT21

/*--------------------------------------------------------------------------

SETUP function

initiate sensor

--------------------------------------------------------------------------*/

void ENS160_AHT21Task::Setup()
{
    DebugSerial::Get()->println("------------------------------------------------------------");

    DebugSerial::Get()->println("ENS160 - Digital air quality sensor");

    DebugSerial::Get()->println();

    DebugSerial::Get()->println("Sensor readout in standard mode");

    DebugSerial::Get()->println();

    DebugSerial::Get()->println("------------------------------------------------------------");

    delay(1000);

    DebugSerial::Get()->print("ENS160...");

    ens160.begin();

    DebugSerial::Get()->println(ens160.available() ? "done." : "failed!");

    if (ens160.available())
    {

        // Print ENS160 versions

        DebugSerial::Get()->print("\tENS160 Rev: ");
        DebugSerial::Get()->print(ens160.getMajorRev());

        DebugSerial::Get()->print(".");
        DebugSerial::Get()->print(ens160.getMinorRev());

        DebugSerial::Get()->print(".");
        DebugSerial::Get()->println(ens160.getBuild());

        DebugSerial::Get()->print("\tStandard mode ");

        DebugSerial::Get()->println(ens160.setMode(ENS160_OPMODE_STD) ? "done." : "failed!");
    }

    DebugSerial::Get()->print("AHT21...");

    if (!aht.begin())
    {

        DebugSerial::Get()->println("Could not find AHT? Check wiring");

        while (1)
            delay(10);
    }

    DebugSerial::Get()->println("AHT10 or AHT20 found");

    // AHT20 end

} // end void setup

/*--------------------------------------------------------------------------

MAIN LOOP FUNCTION

Cylce every 1000ms and perform measurement

--------------------------------------------------------------------------*/

void ENS160_AHT21Task::Loop()
{

    ///// AHT20 start

    sensors_event_t humidity1, temp; // Tim had to change to humidity1

    aht.getEvent(&humidity1, &temp); // populate temp and humidity objects with fresh data

    float tempC = (temp.temperature);
    float humidity = (humidity1.relative_humidity);

    ENS160_AHT21Task::temperatureTelemetry = TemperatureTelemetry(tempC);
    ENS160_AHT21Task::humidityTelemetry = HumidityTelemetry(humidity);

    delay(1000);

    ///// AHT20 end

    if (ens160.available())
    {

        // Give values to Air Quality Sensor.

        ens160.set_envdata(tempC, humidity);

        ens160.measure(true);

        ens160.measureRaw(true);

        uint8_t _aqi = ens160.getAQI();
        uint16_t _tvoc = ens160.getTVOC();
        uint16_t _eco2 = ens160.geteCO2();

        ENS160_AHT21Task::aqiTelemetry = AQITelemetry(_aqi);
        ENS160_AHT21Task::tvocTelemetry = TVOCTelemetry(_tvoc);
        ENS160_AHT21Task::eco2Telemetry = ECO2Telemetry(_eco2);
    }

    delay(1000);

} // end void loop



