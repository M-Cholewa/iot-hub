import axios from 'axios';
import { useEffect, useState } from 'react';
import { serverAddress } from '../../../core/config/server';

export const useDevicesData = () => {
    const [devices, setDevices] = useState([]);

    useEffect(() => {
        refreshDevices();
    }, []);

    const addDevice = async (deviceName) => {

        const res = await axios.put(`${serverAddress}/Device`, null, { params: { deviceName: deviceName } })
            .then((res) => {
                refreshDevices();

                return res.data;
            }).catch((error) => {
                // Obsługa błędu
                return { isSuccess: false, message: error.response.data };
            });

        return res;
    };

    const refreshDevices = () => {
        axios.get(`${serverAddress}/Device/ThisUser`)
            .then((res) => {
                res.data
                    ? setDevices(res.data)
                    : setDevices([]);
            });
    };

    const deleteDevice = (deviceId) => {
        axios.delete(`${serverAddress}/Device`, { data: { id: deviceId } })
            .then(() => {
                refreshDevices();
            });
    };

    return { devices, addDevice, refreshDevices, deleteDevice };
};