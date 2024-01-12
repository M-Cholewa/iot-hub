import { Typography, Button, Stack, Card, CardContent } from "@mui/material";
import { useNavigate } from "react-router-dom";

export const LastOpenDevice = () => {

    const navigate = useNavigate();

    const onLastViewedDeviceClick = () => {
        navigate('/device/6220654d-cfdd-4ab6-9a62-60c31ed307ab');
    };

    return (
        <Card>
            <CardContent>
                <Typography variant="body1" mb={2}>Last viewed</Typography>
                <Typography variant="h4">Room-ESP32</Typography>
                <Typography variant="h5">Online</Typography>
                <Stack direction="row" justifyContent="flex-end">
                    <Button onClick={onLastViewedDeviceClick}>
                        Show
                    </Button>
                </Stack>
            </CardContent>
        </Card>);
};