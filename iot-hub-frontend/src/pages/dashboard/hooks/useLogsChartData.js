import axios from 'axios';
import { useEffect, useState } from 'react';
import { serverAddress } from '../../../core/config/server';
import dayjs from 'dayjs';
import utc from 'dayjs/plugin/utc';
import { useTheme } from '@mui/material';

export const useLogsChartData = () => {
    const [datasets, setDatasets] = useState({ datasets: [] });
    const [loading, setLoading] = useState(false);
    const theme = useTheme();

    dayjs.extend(utc);

    const dateSince = dayjs().utc().subtract(6, 'hours');
    const dateTo = dayjs().utc();

    useEffect(() => {
        setLoading(true);
        const getLogsCountTelemetries = () => {
            axios.get(`${serverAddress}/User/LogsCountTelemetries`, { params: { sinceUTC: dateSince, toUTC: dateTo } })
                .then((res) => {
                    if (res.data) {

                        const infoColor = theme.palette.info.main;
                        const warningColor = theme.palette.warning.main;
                        const errorColor = theme.palette.error.main;

                        setDatasets({
                            datasets: [
                                {
                                    label: 'Info',
                                    data: res.data.logsInfo.map(d => { return { x: d.dateUTC, y: d.fieldValue } }),
                                    backgroundColor: infoColor,
                                    borderColor: infoColor,
                                    // barThickness: 4,
                                },
                                {
                                    label: 'Warning',
                                    data: res.data.logsWarning.map(d => { return { x: d.dateUTC, y: d.fieldValue } }),
                                    backgroundColor: warningColor,
                                    borderColor: warningColor,
                                    // barThickness: 4,
                                },
                                {
                                    label: 'Error',
                                    data: res.data.logsError.map(d => { return { x: d.dateUTC, y: d.fieldValue } }),
                                    backgroundColor: errorColor,
                                    borderColor: errorColor,
                                    // barThickness: 4,
                                }
                            ]
                        })

                        console.log(res.data);


                    } else {
                        setDatasets({ datasets: [] });
                    }
                })
                .catch((err) => {
                    setDatasets({ datasets: [] });
                })
                .finally(() => {
                    setLoading(false);
                });
        };

        getLogsCountTelemetries();
    }, []);

    useEffect(() => {
        console.log(datasets);
    }, [datasets])

    return { datasets, loading };
}