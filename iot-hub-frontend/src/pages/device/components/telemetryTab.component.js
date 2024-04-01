import { Grid, Typography, Card, CardContent } from "@mui/material";
import { DataGrid } from '@mui/x-data-grid';
import { DetailsChart } from "./telemetryTab/detailsChart.component";
import { Console } from "./telemetryTab/console.component";
import { useDeviceTelemetryData } from "../hooks/useDeviceTelemetryData";
import { useDeviceData } from "../hooks/useDeviceData";
import { useContext } from "react";
import { DeviceContext } from "../context/deviceContext.js";

const columns = [
    {
        field: 'fieldName',
        headerName: 'Parameter',
        flex: 180,
    },
    {
        field: 'fieldValue',
        headerName: 'Value',
        flex: 150,
    },
    {
        field: 'fieldUnit',
        headerName: 'Unit',
        flex: 50,
    },
    {
        field: 'dateUTC',
        headerName: 'Updated at',
        flex: 200,
        valueGetter: (params) => {
            const date = new Date(params.row.dateUTC);
            return date.toLocaleString();
        },
    },
];


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
    const deviceFromContext = useContext(DeviceContext);
    const { device } = useDeviceData(deviceFromContext.id);
    const { telemetryMap, loading: telemetryMapLoading, getTelemetry } = useDeviceTelemetryData(deviceFromContext.id);

    const getRowId = (row) => row._mIdx;

    return (<div>
        <Grid container spacing={2}>
            <Grid item xs={2}>
                <CardItem title="Status" value={getTelemetry("Status")?.fieldValue ?? "Unknown"} />
            </Grid>
            <Grid item xs={2}>
                <CardItem title="Firmware" value={getTelemetry("FirmwareVersion")?.fieldValue ?? "Unknown"} />
            </Grid>
            <Grid item xs={3}>
                <CardItem title="Last activity" value={new Date(device?.telemetry?.lastActivityUTC).toLocaleString() ?? "Unknown"} />
            </Grid>
            <Grid item xs={1}>
                <CardItem title="Uptime [s]" value={getTelemetry("UptimeS")?.fieldValue ?? "Unknown"} />
            </Grid>
            <Grid item xs={2}>
                <CardItem title="RSSI" value={getTelemetry("RSSI")?.fieldValue ?? "Unknown"} />
            </Grid>
            <Grid item xs={2}>
                <CardItem title="Battery [%]" value={getTelemetry("Battery")?.fieldValue ?? "Unknown"} />
            </Grid>
        </Grid>

        <Grid container mt={2} spacing={2}>
            <Grid item xs={8}>
                <DetailsChart />
            </Grid>
            <Grid item xs={4}>
                <DataGrid
                    loading={telemetryMapLoading}
                    rows={telemetryMap}
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
                    getRowId={getRowId}
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