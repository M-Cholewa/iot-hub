import { Typography, Card, CardContent, CircularProgress } from "@mui/material";
import { useDeviceCountData } from "../hooks/useDeviceCountData";

export const DevicesCountTile = () => {
    const { deviceCount, loading } = useDeviceCountData();
    return (
        <Card>
            <CardContent>
                <Typography variant="body1" gutterBottom mb={2}>
                    Devices
                </Typography>
                {
                    loading
                        ? <CircularProgress />
                        : <Typography variant="h4" gutterBottom align="center">
                            {deviceCount}
                        </Typography>
                }
            </CardContent>
        </Card>);
};