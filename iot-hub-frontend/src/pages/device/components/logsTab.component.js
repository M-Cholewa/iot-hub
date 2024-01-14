import { Container, Button, AppBar, Grid, Toolbar, Alert, Card, CardContent, Typography } from '@mui/material';
import { DataGrid } from '@mui/x-data-grid';

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
        width: 200,
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
        field: 'label',
        headerName: 'Label',
        width: 400,
    },
    {
        field: 'timestampUTC',
        headerName: 'Received at',
        width: 200,
    },
];

const rows = [
    { id: 1, severity: 'Info', label: 'Device connected', timestampUTC: '2021-10-10 12:00:00' },
    { id: 2, severity: 'Info', label: 'Device disconnected', timestampUTC: '2021-10-10 12:00:00' },
    { id: 3, severity: 'Info', label: 'Device connected', timestampUTC: '2021-10-10 12:00:00' },
    { id: 4, severity: 'Warning', label: 'Low battery level (5%)', timestampUTC: '2021-10-10 12:00:00' },
    { id: 5, severity: 'Warning', label: 'Low battery level (10%)', timestampUTC: '2021-10-10 12:00:00' },
    { id: 6, severity: 'Warning', label: 'Low battery level (20%)', timestampUTC: '2021-10-10 12:00:00' },
    { id: 7, severity: 'Error', label: 'No data from temperature sensor', timestampUTC: '2021-10-10 12:00:00' },
    { id: 8, severity: 'Error', label: 'No data from temperature sensor', timestampUTC: '2021-10-10 12:00:00' },
    { id: 9, severity: 'Error', label: 'No data from temperature sensor', timestampUTC: '2021-10-10 12:00:00' },
    { id: 10, severity: 'Error', label: 'No data from temperature sensor', timestampUTC: '2021-10-10 12:00:00' },
];

export const LogsTab = () => {
    return (<Container>
        <Grid container justifyContent="space-around">
            <Grid item xs={3}>
                <CardItem title="Info" value={rows.filter(x => x.severity === "Info").length} />
            </Grid>
            <Grid item xs={3}>
                <CardItem title="Warning" value={rows.filter(x => x.severity === "Warning").length} />
            </Grid>
            <Grid item xs={3}>
                <CardItem title="Error" value={rows.filter(x => x.severity === "Error").length} />
            </Grid>
        </Grid>
        <AppBar
            position="static"
            sx={{ background: '#101418' }}
            elevation={0}>
            <Toolbar>
                <Grid container justifyContent="flex-end">
                    <Grid item>
                        <Button variant='outlined'>
                            Clear logs
                        </Button>
                    </Grid>
                </Grid>
            </Toolbar>
        </AppBar>
        <DataGrid
            mt={2}
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
    </Container>);
};