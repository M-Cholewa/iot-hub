import React from 'react';
import { Bar } from 'react-chartjs-2';
import faker from 'faker';
import { getLabels } from '../../../core/utility/mocks';
import { useTheme } from '@mui/material';

const options = {
    responsive: true,
    plugins: {
        legend: {
            position: 'top',
        },
    },
};

const labels = getLabels(120);



const data = {
    labels,
    datasets: [
        {
            label: 'Infos',
            data: labels.map(() => faker.datatype.number({ min: 0, max: 3 })),
        },
        {
            label: 'Warnings',
            data: labels.map(() => faker.datatype.number({ min: 0, max: 2 })),
        },
        {
            label: 'Alarms',
            data: labels.map(() => faker.datatype.number({ min: 0, max: 1 })),
        },

    ],
};

export function LogCount() {
    const theme = useTheme();

    // Kolory zgodne z MUI
    const infoColor = theme.palette.info.main;
    const warningColor = theme.palette.warning.main;
    const errorColor = theme.palette.error.main;

    data.datasets[0].backgroundColor = infoColor;
    data.datasets[1].backgroundColor = warningColor;
    data.datasets[2].backgroundColor = errorColor;

    return <Bar options={options} data={data} />;
}
