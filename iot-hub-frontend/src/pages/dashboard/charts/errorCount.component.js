import React from 'react';
import { Bar } from 'react-chartjs-2';
import faker from 'faker';
import { getLabels } from '../../../core/utility/mocks';

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
            label: 'Alarms registered',
            data: labels.map(() => faker.datatype.number({ min: 0, max: 5 })),
            backgroundColor: 'rgba(255, 50, 50, 0.8)',
        },

    ],
};

export function ErrorCount() {
    return <Bar options={options} data={data} />;
}
