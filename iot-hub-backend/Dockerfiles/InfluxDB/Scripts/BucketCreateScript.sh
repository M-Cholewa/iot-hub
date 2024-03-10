#!/bin/bash

echo "=============== Running bucket create script ==============="


# Tworzenie bucketów
influx bucket create -n "logs-bucket"
influx bucket create -n "telemetry-bucket"
influx bucket create -n "console-record-bucket"

echo "Bucket-y zostały utworzone."
