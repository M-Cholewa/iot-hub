import { Alert, Stack } from '@mui/material';

export const LastNotifications = () => {
    return (
        <Stack spacing={1}>
            <Alert variant="outlined" severity="info">
                ESP32-1: Device connected.
            </Alert>
            <Alert variant="outlined" severity="warning">
                ESP32-2: Low battery level (5%).
            </Alert>
            <Alert variant="outlined" severity="error">
                ESP32-2: No data from temperature sensor.
            </Alert>
        </Stack>
    );
};