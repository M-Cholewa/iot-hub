import axios from 'axios';
import { useEffect, useState } from 'react';
import { serverAddress } from '../../../core/config/server';

export const useLastLogsData = () => {
    const [lastLogs, setLastLogs] = useState([]);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        const getNotifications = () => {
            setLoading(true);
            axios.get(`${serverAddress}/Logs/UserDeviceLogsLastN`, { params: { limit: 3 } })
                .then((res) => {
                    res.data
                        ? setLastLogs(res.data)
                        : setLastLogs([]);
                    console.log(res.data);
                })
                .catch((err) => {
                    setLastLogs([]);
                })
                .finally(() => {
                    setLoading(false);
                });
        }

        getNotifications();
    }, []);

    return { lastLogs, loading };

};