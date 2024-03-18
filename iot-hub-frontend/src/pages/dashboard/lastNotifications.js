import { Alert, Stack, Typography } from '@mui/material';

export const LastNotifications = () => {
    return (
        <Stack spacing={1}>
            <Alert variant="outlined" severity="info"
                action={
                    <Typography variant='span'>18 Mar 2024 19:40:21</Typography>
                }>
                ESP32-1: Device connected.
            </Alert>
            <Alert variant="outlined" severity="warning"
                action={
                    <Typography variant='span'>18 Mar 2024 19:40:21</Typography>
                }>
                ESP32-2: Low battery level (5%).
            </Alert>
            <Alert variant="outlined" severity="error"
                action={
                    <Typography variant='span'>18 Mar 2024 19:40:21</Typography>
                }>
                ESP32-2: No data from temperature sensor.
            </Alert>
        </Stack>
    );
};