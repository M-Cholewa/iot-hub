import axios from 'axios';
import { useEffect, useState } from 'react';
import { serverAddress } from '../../../core/config/server';

export const useDeviceData = (deviceId) => {
    const [device, setDevice] = useState(null);

    useEffect(() => {
        axios.get(`${serverAddress}/GetDevice`, { params: { id: deviceId } })
            .then((res) => {
                res.data
                    ? setDevice(res.data)
                    : setDevice(null);
            })
            .catch((err) => {
                setDevice(null);
            });
    }, []);

    return { device };
};