import { Typography, Card, CardContent } from "@mui/material";

export const DevicesCountTile = () => {
    return (
        <Card>
            <CardContent>
                <Typography variant="body1" gutterBottom mb={2}>
                    Devices
                </Typography>
                <Typography variant="h4" gutterBottom align="center">
                    5
                </Typography>
            </CardContent>
        </Card>);
};