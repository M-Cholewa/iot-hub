import axios from 'axios';
import { useEffect, useState } from 'react';
import { serverAddress } from '../../../core/config/server';
import dayjs from 'dayjs';
import utc from 'dayjs/plugin/utc';

export const useMessagesLoggedChartData = () => {
    const [dataset, setDataset] = useState({ datasets: [] });
    const [loading, setLoading] = useState(false);

    dayjs.extend(utc);

    const dateSince = dayjs().utc().subtract(24, 'hours');
    const dateTo = dayjs().utc();

    useEffect(() => {
        const getMessagesLogged = () => {
            setLoading(true);
            axios.get(`${serverAddress}/User/MessagesLoggedCountTelemetries`, { params: { sinceUTC: dateSince, toUTC: dateTo } })
                .then((res) => {
                    if (res.data) {
                        setDataset({
                            datasets: [
                                {
                                    label: "Messages Logged",
                                    data: res.data.map(d => { return { x: d.dateUTC, y: d.fieldValue } }),
                                    backgroundColor: "#3f51b5",
                                    borderColor: "#3f51b5",
                                }
                            ]
                        });
                    } else {
                        setDataset({ datasets: [] });
                    }
                })
                .catch((err) => {
                    setDataset({ datasets: [] });
                })
                .finally(() => {
                    setLoading(false);
                });
        }

        getMessagesLogged();
    }, []);

    return { dataset, loading };
};