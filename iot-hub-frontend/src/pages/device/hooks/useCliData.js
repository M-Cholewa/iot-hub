import axios from 'axios';
import { useEffect, useState } from 'react';
import { serverAddress } from '../../../core/config/server';

export const useCliData = (deviceId) => {
    const [consoleRecords, setConsoleRecords] = useState([]);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        refreshConsoleRecords();
    }, []);


    const refreshConsoleRecords = () => {
        axios.get(`${serverAddress}/Device/AllConsoleRecords`, { params: { deviceId: deviceId } })
            .then((res) => {
                console.log(res.data);
                res.data
                    ? setConsoleRecords(res.data)
                    : setConsoleRecords([]);
            })
            .catch((err) => {
                setConsoleRecords([]);
            })
            .finally(() => {
                setLoading(false);
            });
    };





    return { consoleRecords };
};