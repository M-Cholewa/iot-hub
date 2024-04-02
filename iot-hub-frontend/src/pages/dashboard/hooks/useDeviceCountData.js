import axios from 'axios';
import { useEffect, useState } from 'react';
import { serverAddress } from '../../../core/config/server';

export const useDeviceCountData = () => {
    const [deviceCount, setDeviceCount] = useState(0);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        const getNotifications = () => {
            setLoading(true);
            axios.get(`${serverAddress}/User/UserDeviceCount`)
                .then((res) => {
                    res.data
                        ? setDeviceCount(res.data)
                        : setDeviceCount(0);
                })
                .catch((err) => {
                    setDeviceCount(0);
                })
                .finally(() => {
                    setLoading(false);
                });
        }

        getNotifications();
    }, []);

    return { deviceCount, loading };

};