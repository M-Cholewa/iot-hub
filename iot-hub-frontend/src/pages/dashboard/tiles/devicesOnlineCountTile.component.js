import { Typography, Card, CardContent, CircularProgress } from "@mui/material";
import { useDevicesOnlineCountData } from "../hooks/useDevicesOnlineCountData";

export const DevicesOnlineCountTile = () => {
    const { devicesOnlineCount, loading } = useDevicesOnlineCountData();
    return (
        <Card>
            <CardContent>
                <Typography variant="body1" gutterBottom mb={2}>
                    Online
                </Typography>
                {
                    loading
                        ? <CircularProgress />
                        : <Typography variant="h4" gutterBottom align="center">
                            {devicesOnlineCount}
                        </Typography>
                }
            </CardContent>
        </Card>);
};