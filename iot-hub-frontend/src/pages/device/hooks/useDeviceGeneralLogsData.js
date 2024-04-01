import axios from 'axios';
import { useEffect, useState } from 'react';
import { serverAddress } from '../../../core/config/server';

export const useDeviceGeneralLogsData = (deviceId) => {
    const [logs, setLogs] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        setLoading(true);
        axios.get(`${serverAddress}/Logs/AllGeneralLogs`, { params: { deviceId: deviceId } })
            .then((res) => {
                setLogs(res.data);
            })
            .catch((err) => {
                setLogs([]);
            })
            .finally(() => {
                setLoading(false);
            });
    }, [deviceId]);

    return { logs, loading };
};