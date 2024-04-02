import axios from 'axios';
import { useEffect, useState } from 'react';
import { serverAddress } from '../../../core/config/server';

export const useDevicesOnlineCountData = () => {
    const [devicesOnlineCount, setDevicesOnlineCount] = useState(0);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        const getNotifications = () => {
            setLoading(true);
            axios.get(`${serverAddress}/Device/UserDevicesOnlineCount`)
                .then((res) => {
                    res.data
                        ? setDevicesOnlineCount(res.data)
                        : setDevicesOnlineCount(0);
                })
                .catch((err) => {
                    setDevicesOnlineCount(0);
                })
                .finally(() => {
                    setLoading(false);
                });
        }

        getNotifications();
    }, []);

    return { devicesOnlineCount, loading };

};