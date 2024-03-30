import axios from 'axios';
import { useEffect, useState } from 'react';
import { serverAddress } from '../../../core/config/server';

export const useCliData = (deviceId) => {
    const [consoleRecords, setConsoleRecords] = useState([]);
    const [consoleLoading, setConsoleLoading] = useState(false);

    useEffect(() => {
        refreshConsoleRecords();
    }, []);


    const refreshConsoleRecords = () => {
        setConsoleLoading(true);
        axios.get(`${serverAddress}/Device/AllConsoleRecords`, { params: { deviceId: deviceId } })
            .then((res) => {
                res.data
                    ? setConsoleRecords(res.data)
                    : setConsoleRecords([]);
            })
            .catch((err) => {
                setConsoleRecords([]);
            })
            .finally(() => {
                setConsoleLoading(false);
            });
    };


    return { consoleLoading, consoleRecords };
};