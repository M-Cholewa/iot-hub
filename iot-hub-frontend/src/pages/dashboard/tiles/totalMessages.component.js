import { Typography, Card, CardContent, CircularProgress } from "@mui/material";
import { useSummedMessageCountData } from "../hooks/useSummedMessageCountData";

export const TotalMessages = () => {

    const { summedMessageCount, loading } = useSummedMessageCountData();

    return (
        <Card>
            <CardContent>
                <Typography variant="body1" gutterBottom mb={2}>
                    Messages
                </Typography>
                {
                    loading
                        ? <CircularProgress />
                        : <Typography variant="h4" gutterBottom align="center">
                            {summedMessageCount}
                        </Typography>
                }
            </CardContent>
        </Card>);
};