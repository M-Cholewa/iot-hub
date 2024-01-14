import { Grid, Typography, Card, CardContent } from "@mui/material";
import { DataGrid } from '@mui/x-data-grid';
import { DetailsChart } from "./telemetryTab/detailsChart.component";
import { Console } from "./telemetryTab/console.component";
const columns = [
    {
        field: 'parameter',
        headerName: 'Parameter',
        width: 180,
    },
    {
        field: 'value',
        headerName: 'Value',
        width: 150,
    },
    {
        field: 'timestampUTC',
        headerName: 'Updated at',
        width: 200,
    },
];

const rows = [
    { id: 1, parameter: 'Temperature', value: '25', timestampUTC: '2021-10-10 12:00:00' },
    { id: 2, parameter: 'Humidity', value: '50', timestampUTC: '2021-10-10 12:00:00' },
    { id: 3, parameter: 'Battery', value: '80', timestampUTC: '2021-10-10 12:00:00' },
    { id: 4, parameter: 'RSSI', value: '-20', timestampUTC: '2021-10-10 12:00:00' },
    { id: 5, parameter: 'Status', value: 'Online', timestampUTC: '2021-10-10 12:00:00' },
    { id: 6, parameter: 'Last activity', value: '13.01.24 16:45:20', timestampUTC: '2021-10-10 12:00:00' },
    { id: 7, parameter: 'Uptime', value: '25d 15h 10m 11s', timestampUTC: '2021-10-10 12:00:00' },
    { id: 8, parameter: 'Power consumption [W]', value: '0.3', timestampUTC: '2021-10-10 12:00:00' },
    { id: 9, parameter: 'LED 0 state', value: '0', timestampUTC: '2021-10-10 12:00:00' },
    { id: 10, parameter: 'LED 1 state', value: '0', timestampUTC: '2021-10-10 12:00:00' },
    { id: 11, parameter: 'LED 2 state', value: '0', timestampUTC: '2021-10-10 12:00:00' },
    { id: 12, parameter: 'Light switch state', value: '0', timestampUTC: '2021-10-10 12:00:00' },

]

const CardItem = ({ title, value }) => {
    return (
        <Card>
            <CardContent>
                <Typography variant="body1" gutterBottom>
                    {title}
                </Typography>
                <Typography variant="h5" gutterBottom align="center">
                    {value}
                </Typography>
            </CardContent>
        </Card>);
}


export const TelemetryTab = () => {
    return (<div>
        <Grid container spacing={2}>
            <Grid item xs={2}>
                <CardItem title="Status" value="Online" />
            </Grid>
            <Grid item xs={2}>
                <CardItem title="Firmware" value="1.0.0_alpha" />
            </Grid>
            <Grid item xs={2}>
                <CardItem title="Last activity" value="13.01.24 16:45:20" />
            </Grid>
            <Grid item xs={2}>
                <CardItem title="Uptime" value="25d 15h 10m 11s" />
            </Grid>
            <Grid item xs={2}>
                <CardItem title="RSSI" value="-20" />
            </Grid>
            <Grid item xs={2}>
                <CardItem title="Battery" value="100%" />
            </Grid>
        </Grid>

        <Grid container mt={2} spacing={2}>
            <Grid item xs={8}>
                <DetailsChart />
            </Grid>
            <Grid item xs={4}>
                <DataGrid
                    rows={rows}
                    columns={columns}
                    initialState={{
                        pagination: {
                            paginationModel: {
                                pageSize: 5,
                            },
                        },
                    }}
                    pageSizeOptions={[5]}
                    disableRowSelectionOnClick
                />
            </Grid>
        </Grid>

        <Grid container mt={1} spacing={2}>
            <Grid item xs={12}>
                <Console />
            </Grid>
        </Grid>
    </div>);
};