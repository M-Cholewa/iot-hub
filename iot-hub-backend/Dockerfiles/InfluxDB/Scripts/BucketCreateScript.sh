#!/bin/bash

echo "=============== Running bucket create script ==============="


# Tworzenie bucketów
influx bucket create -n "logs-bucket"
influx bucket create -n "telemetry-bucket"

echo "Bucket-y zostały utworzone."
