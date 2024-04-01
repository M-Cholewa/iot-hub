import axios from 'axios';
import { useEffect, useState } from 'react';
import { serverAddress } from '../../../core/config/server';

export const useDeviceTelemetryData = (deviceId) => {
    const [telemetryMap, setTelemetryMap] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const refreshTelemetryMap = () => {
            setLoading(true);
            axios.get(`${serverAddress}/Device/TelemetryMap`, { params: { deviceId: deviceId } })
                .then((res) => {
                    setTelemetryMap(res.data.map((item, index) => ({ ...item, _mIdx: index })));
                })
                .catch((err) => {
                    setTelemetryMap([]);
                })
                .finally(() => {
                    setLoading(false);
                });
        }

        refreshTelemetryMap();
    }, [deviceId]);

    const getTelemetry = (fieldName) => {
        return telemetryMap.find((item) => item.fieldName === fieldName);
    };

    return { telemetryMap, loading, getTelemetry };
};