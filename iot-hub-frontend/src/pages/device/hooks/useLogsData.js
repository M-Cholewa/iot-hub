import axios from 'axios';
import { useEffect, useState } from 'react';
import { serverAddress } from '../../../core/config/server';

export const useLogsData = (deviceId) => {
    const [logs, setLogs] = useState([]);
    const [loading, setLoading] = useState(false);

    useEffect(() => {

        const refreshLogs = () => {
            setLoading(true);
            axios.get(`${serverAddress}/Logs/All`, { params: { deviceId: deviceId } })
                .then((res) => {
                    res.data
                        ? setLogs(res.data.map((obj, idx) => ({ ...obj, _mIdx: idx })))
                        : setLogs([]);
                })
                .catch((err) => {
                    setLogs([]);
                })
                .finally(() => {
                    setLoading(false);
                });
        }

        refreshLogs();
    }, []);



    return { logs, loading };

};