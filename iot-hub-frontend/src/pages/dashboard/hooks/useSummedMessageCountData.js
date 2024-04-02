import axios from 'axios';
import { useEffect, useState } from 'react';
import { serverAddress } from '../../../core/config/server';

export const useSummedMessageCountData = () => {
    const [summedMessageCount, setSummedMessageCount] = useState(0);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        const getNotifications = () => {
            setLoading(true);
            axios.get(`${serverAddress}/User/SummedMessageCount`)
                .then((res) => {
                    res.data
                        ? setSummedMessageCount(res.data)
                        : setSummedMessageCount(0);
                })
                .catch((err) => {
                    setSummedMessageCount(0);
                })
                .finally(() => {
                    setLoading(false);
                });
        }

        getNotifications();
    }, []);

    return { summedMessageCount, loading };

};