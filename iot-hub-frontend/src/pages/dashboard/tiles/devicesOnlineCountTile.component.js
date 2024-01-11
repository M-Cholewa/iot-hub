import { Typography, Card, CardContent } from "@mui/material";

export const DevicesOnlineCountTile = () => {
    return (
        <Card>
            <CardContent>
                <Typography variant="body1" gutterBottom mb={2}>
                    Online
                </Typography>
                <Typography variant="h4" gutterBottom align="center">
                    3
                </Typography>
            </CardContent>
        </Card>);
};