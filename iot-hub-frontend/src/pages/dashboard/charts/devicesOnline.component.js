import React from 'react';
import faker from 'faker';
import { getLabels } from '../../../core/utility/mocks';
import { Line } from 'react-chartjs-2';
import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    BarElement,
    LineElement,
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
    LineElement,
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

const labels = getLabels(60);

const data = {
    labels,
    datasets: [
        {
            label: 'Devices online',
            data: labels.map(() => faker.datatype.number({ min: 3, max: 5 })),
            backgroundColor: 'rgba(53, 162, 235, 0.8)',
        },

    ],
};

export function DevicesOnline() {
    const { dataset, loading } = useDevicesOnlineChartData();

    if (loading) {
        return <CircularProgress />
    }


    return <Line options={options} data={dataset} />;
}

