import axios from 'axios';
import { useEffect, useState } from 'react';
import { serverAddress } from '../../../core/config/server';

export const useDeviceData = (deviceId) => {
    const [device, setDevice] = useState(null);

    useEffect(() => {
        axios.get(`${serverAddress}/Device`, { params: { id: deviceId } })
            .then(async (res) => {

                if (res.data == null) {
                    setDevice(null);
                    return;
                }

                const telemetry = await axios.get(`${serverAddress}/Device/UserDeviceTelemetry`, { params: { deviceId: deviceId } });
                setDevice({ ...res.data, telemetry: telemetry?.data ?? {} });

            })
            .catch((err) => {
                setDevice(null);
            });
    }, [deviceId]);

    return { device };
};