import { Box, Chip, FormControl, InputLabel, MenuItem, OutlinedInput, Select, Grid, Stack, Button } from "@mui/material";
import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend,
} from 'chart.js';
import { Line } from 'react-chartjs-2';
import faker from 'faker';
import { getLabels } from '../../../../core/utility/mocks';
import { useState } from "react";
import { MobileDateTimePicker } from '@mui/x-date-pickers/MobileDateTimePicker';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import dayjs from 'dayjs';

const parameters = [
    'Temperature',
    'Humidity',
    'Battery',
    'RSSI',
    'Status',
    'Last activity',
    'Uptime',
    'Power consumption [W]',
    'LED 0 state',
    'LED 1 state',
    'LED 2 state',
    'Light switch state',
];

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
            label: 'Temperature',
            data: labels.map(() => faker.datatype.number({ min: 12, max: 15 })),
            borderColor: 'rgb(53, 162, 235)',
            backgroundColor: 'rgba(53, 162, 235, 0.5)',
        },
    ],
};

ChartJS.register(
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    Title,
    Tooltip,
    Legend
);

export const DetailsChart = () => {

    const [selectedParameters, setSelectedParameters] = useState([parameters[0], parameters[1], parameters[2], parameters[3]]);

    const handleChange = (event) => {
        const {
            target: { value },
        } = event;
        setSelectedParameters(
            // On autofill we get a stringified value.
            typeof value === 'string' ? value.split(',') : value,
        );
    };

    return (
        <Grid container spacing={2}>
            <Grid item xs={4}>
                <Stack direction="column" spacing={2}>
                    <LocalizationProvider dateAdapter={AdapterDayjs}>
                        <MobileDateTimePicker label="Since" defaultValue={dayjs('2022-04-17T15:30')} />
                        <MobileDateTimePicker label="To" defaultValue={dayjs('2022-04-18T15:30')} />
                    </LocalizationProvider>
                    <FormControl>
                        <InputLabel id="chartParameter">Params</InputLabel>
                        <Select
                            labelId="chartParameter"
                            multiple
                            value={selectedParameters}
                            onChange={handleChange}
                            input={<OutlinedInput label="Params" />}
                            renderValue={(selected) => (
                                <Box sx={{ display: 'flex', flexWrap: 'wrap', gap: 0.5 }}>
                                    {selected.map((value) => (
                                        <Chip key={value} label={value} />
                                    ))}
                                </Box>
                            )}
                        >
                            {parameters.map((parameter) => (
                                <MenuItem
                                    key={parameter}
                                    value={parameter}
                                >
                                    {parameter}
                                </MenuItem>
                            ))}
                        </Select>
                    </FormControl>
                    <Button variant="outlined">show</Button>
                </Stack>
            </Grid>
            <Grid item xs={8}>
                <Line options={options} data={data} />
            </Grid>
        </Grid>
    );
};