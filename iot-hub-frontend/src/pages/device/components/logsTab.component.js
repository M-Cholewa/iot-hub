import { Container, Grid, Alert, Card, CardContent, Typography } from '@mui/material';
import { DataGrid } from '@mui/x-data-grid';
import { useLogsData } from '../hooks/useLogsData';
import { useContext } from 'react';
import { DeviceContext } from "../context/deviceContext.js";

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

const columns = [

    {
        field: 'severity',
        headerName: 'Severity',
        flex: 200,
        renderCell: ({ value }) => {
            if (value === 'Info') {
                return (<Alert sx={{ width: "100%" }} severity="info">Info</Alert>);
            }

            if (value === 'Warning') {
                return (<Alert sx={{ width: "100%" }} severity="warning">Warning</Alert>);
            }

            if (value === 'Error') {
                return (<Alert sx={{ width: "100%" }} severity="error">Error</Alert>);
            }
        },
    },
    {
        field: 'message',
        headerName: 'Message',
        flex: 400,
    },
    {
        field: 'dateUTC',
        headerName: 'Received at',
        flex: 200,
        valueGetter: (params) => {
            const date = new Date(params.row.dateUTC);
            return date.toLocaleString();
        },
    },
];


export const LogsTab = () => {
    const device = useContext(DeviceContext);

    const { logs, loading } = useLogsData(device.id);

    function getRowId(row) {
        return row._mIdx;
    }


    return (<Container>
        <Grid container justifyContent="space-around" mb={3}>
            <Grid item xs={3}>
                <CardItem title="Info" value={logs.filter(x => x.severity === "Info").length} />
            </Grid>
            <Grid item xs={3}>
                <CardItem title="Warning" value={logs.filter(x => x.severity === "Warning").length} />
            </Grid>
            <Grid item xs={3}>
                <CardItem title="Error" value={logs.filter(x => x.severity === "Error").length} />
            </Grid>
        </Grid>

        <DataGrid
            loading={loading}
            rows={logs}
            columns={columns}
            initialState={{
                pagination: {
                    paginationModel: {
                        pageSize: 5,
                    },
                },
            }}
            getRowId={getRowId}
            pageSizeOptions={[5]}
            disableRowSelectionOnClick
        />
    </Container>);
};