import { Typography, Card, CardContent } from "@mui/material";

export const TotalMessages = () => {
    return (
        <Card>
            <CardContent>
                <Typography variant="body1" gutterBottom mb={2}>
                    Messages
                </Typography>
                <Typography variant="h4" gutterBottom align="center">
                    1554
                </Typography>
            </CardContent>
        </Card>);
};