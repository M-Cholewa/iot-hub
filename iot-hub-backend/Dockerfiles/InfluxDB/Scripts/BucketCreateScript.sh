#!/bin/bash

echo "=============== Running bucket create script ==============="


# Tworzenie bucketów
influx bucket create -n "log-bucket"
influx bucket create -n "telemetry-bucket"
influx bucket create -n "console-record-bucket"
influx bucket create -n "general-telemetry-bucket"
influx bucket create -n "general-log-bucket"

echo "Bucket-y zostały utworzone."
