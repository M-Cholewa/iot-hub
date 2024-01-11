import React from 'react';
import { Bar } from 'react-chartjs-2';
import faker from 'faker';
import { getLabels } from '../../../core/utility/mocks';
import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend,
} from 'chart.js';

ChartJS.register(
    CategoryScale,
    LinearScale,
    BarElement,
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
    return <Bar options={options} data={data} />;
}

