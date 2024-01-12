import axios from 'axios';
import { useEffect, useState } from 'react';
import { serverAddress } from '../../../core/config/server';

export const useDevicesData = () => {
    const [devices, setDevices] = useState([]);

    useEffect(() => {
        axios.get(`${serverAddress}/GetDevices`).then((res) => {
            res.data
                ? setDevices(res.data)
                : setDevices([]);
        });
    }, []);

    return { devices };
};