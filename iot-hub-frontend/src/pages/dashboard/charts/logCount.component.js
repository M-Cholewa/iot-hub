import React from 'react';
import { Line } from 'react-chartjs-2';
import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    LineElement,
    Title,
    Tooltip,
    Legend,
    TimeScale,
} from 'chart.js';
import { useLogsChartData } from '../hooks/useLogsChartData';
import { CircularProgress } from '@mui/material';
import 'chartjs-adapter-dayjs-4/dist/chartjs-adapter-dayjs-4.esm';

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

ChartJS.register(
    CategoryScale,
    LinearScale,
    LineElement,
    TimeScale,
    Title,
    Tooltip,
    Legend
);

export function LogCount() {
    const { datasets, loading } = useLogsChartData();
    if (loading) {
        return <CircularProgress />
    }
    return <Line options={options} data={datasets} />;
}
