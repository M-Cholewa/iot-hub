import { Typography, Button, Stack, Card, CardContent } from "@mui/material";
import { useNavigate } from "react-router-dom";
import { useCookies } from 'react-cookie';

export const LastOpenDevice = () => {
    const [cookies, setCookie] = useCookies(['lastViewed']);

    const navigate = useNavigate();

    const onLastViewedDeviceClick = (deviceId) => {
        navigate(`/device/${deviceId}`);
    };

    return (
        <Card>
            {cookies.lastViewed && cookies.lastViewed.length > 0 && <CardContent>
                <Typography variant="body1" mb={2}>Last viewed</Typography>
                <Typography variant="h4">{cookies.lastViewed[0].name}</Typography>
                <Stack direction="row" justifyContent="flex-end">
                    <Button onClick={() => onLastViewedDeviceClick(cookies.lastViewed[0].id)}>
                        Show
                    </Button>
                </Stack>
            </CardContent>}

        </Card>);
};