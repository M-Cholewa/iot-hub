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
    TimeScale
} from 'chart.js';
import { Line } from 'react-chartjs-2';
import { MobileDateTimePicker } from '@mui/x-date-pickers/MobileDateTimePicker';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { useChartsData } from "../../hooks/useChartsData";
import { useContext } from "react";
import { DeviceContext } from "../../context/deviceContext";
import dayjs from 'dayjs';
import utc from 'dayjs/plugin/utc';
import 'chartjs-adapter-dayjs-4/dist/chartjs-adapter-dayjs-4.esm';

const options = {
    responsive: true,
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
    plugins: {
        legend: {
            position: 'top',
        },
    },
};


ChartJS.register(
    CategoryScale,
    LinearScale,
    PointElement,
    LineElement,
    TimeScale,
    Title,
    Tooltip,
    Legend
);

export const DetailsChart = () => {

    dayjs.extend(utc);

    const deviceFromContext = useContext(DeviceContext);
    const { fieldNames, selectedFieldNames, setSelectedFieldNames, datasets, loading,
        refreshDatasets, dateSince, setDateSince, dateTo, setDateTo } = useChartsData(deviceFromContext.id);


    const handleChange = (event) => {
        const {
            target: { value },
        } = event;
        setSelectedFieldNames(
            // On autofill we get a stringified value.
            typeof value === 'string' ? value.split(',') : value,
        );
    };

    const handleShow = () => {
        refreshDatasets(dateSince.utc(), dateTo.utc());
    };

    return (
        <Grid container spacing={2}>
            <Grid item xs={4}>
                <Stack direction="column" spacing={2}>
                    <LocalizationProvider dateAdapter={AdapterDayjs}>
                        <MobileDateTimePicker ampm={false} value={dateSince} onChange={setDateSince} label="Since" />
                        <MobileDateTimePicker ampm={false} value={dateTo} onChange={setDateTo} label="To" />
                    </LocalizationProvider>
                    <FormControl>
                        <InputLabel id="chartParameter">Params</InputLabel>
                        <Select
                            labelId="chartParameter"
                            multiple
                            value={selectedFieldNames}
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
                            {fieldNames.map((parameter) => (
                                <MenuItem
                                    key={parameter}
                                    value={parameter}
                                >
                                    {parameter}
                                </MenuItem>
                            ))}
                        </Select>
                    </FormControl>
                    <Button variant="outlined" onClick={handleShow}>show</Button>
                </Stack>
            </Grid>
            <Grid item xs={8}>
                <Line options={options} data={datasets} />
            </Grid>
        </Grid>
    );
};