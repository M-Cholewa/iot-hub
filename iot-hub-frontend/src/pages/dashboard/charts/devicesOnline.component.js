import React from 'react';
import faker from 'faker';
import { getLabels } from '../../../core/utility/mocks';
import { Bar } from 'react-chartjs-2';
import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend,
    TimeScale
} from 'chart.js';
import { useDevicesOnlineChartData } from '../hooks/useDevicesOnlineChartData';
import { CircularProgress } from '@mui/material';

ChartJS.register(
    CategoryScale,
    LinearScale,
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

export function DevicesOnline() {
    const { dataset, loading } = useDevicesOnlineChartData();

    if (loading) {
        return <CircularProgress />
    }

    return <Bar options={options} data={dataset} />;
}

