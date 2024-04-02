import React from 'react';
import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    PointElement,
    BarElement,
    Title,
    Tooltip,
    Legend,
    TimeScale
} from 'chart.js';
import { Bar } from 'react-chartjs-2';
import { useMessagesLoggedChartData } from '../hooks/useMessagesLoggedChartData';
import { CircularProgress } from '@mui/material';
import 'chartjs-adapter-dayjs-4/dist/chartjs-adapter-dayjs-4.esm';


ChartJS.register(
    CategoryScale,
    LinearScale,
    PointElement,
    BarElement,
    TimeScale,
    Title,
    Tooltip,
    Legend
);

const options = {
    responsive: true,
    plugins: {
        legend: {
            position: 'top',
        },
    },
    interaction: {
        mode: 'index',
        intersect: false,
    },
    scales: {
        x: {
            type: 'time',
            time: {
                unit: 'hour',
                round: 'hour',
                displayFormats: {
                    millisecond: 'HH:mm:ss.SSS',
                    second: 'HH:mm:ss',
                    minute: 'HH:mm',
                    hour: 'HH',
                    day: 'MMM DD',
                    week: 'll',
                    month: 'MMM YYYY',
                    quarter: '[Q]Q - YYYY',
                    year: 'YYYY',
                },
                tooltipFormat: 'll HH:mm',
            }
        },
    },
};

export function MessagesLogged() {
    const { dataset, loading } = useMessagesLoggedChartData();
    if (loading) {
        return <CircularProgress />
    }
    return <Bar options={options} data={dataset} />;
}
