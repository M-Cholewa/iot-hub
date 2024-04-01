import { Typography, Button, Stack, Card, CardContent, Grid } from "@mui/material";
import { useNavigate } from "react-router-dom";
import { useCookies } from 'react-cookie';

export const LastOpenedDevices = () => {
    const [cookies, setCookie] = useCookies(['lastViewed']);

    const navigate = useNavigate();

    const onLastViewedDeviceClick = (deviceId) => {
        navigate(`/device/${deviceId}`);
    };

    if (cookies.lastViewed == null) {
        return <></>
    }

    return (
        <Grid container alignItems="center" direction="row" my={3} >
            <Grid item xs={2}>
                <Typography variant="h5" mb={2}>Last viewed</Typography>
            </Grid>
            <Grid item xs={10}>
                <Grid container alignItems="center" justifyContent="space-between" direction="row">
                    <Grid item xs={3}>
                        {cookies.lastViewed && cookies.lastViewed.length > 0 &&
                            <Card>
                                <CardContent>
                                    <Typography variant="h6">Room-ESP32</Typography>
                                    <Stack direction="row" justifyContent="flex-end">
                                        <Button onClick={() => onLastViewedDeviceClick()}>
                                            Show
                                        </Button>
                                    </Stack>
                                </CardContent>
                            </Card>
                        }
                    </Grid>
                    <Grid item xs={3}>
                        {cookies.lastViewed && cookies.lastViewed.length > 1 &&
                            <Card>
                                <CardContent>
                                    <Typography variant="h6">Room-ESP32</Typography>
                                    <Stack direction="row" justifyContent="flex-end">
                                        <Button onClick={() => onLastViewedDeviceClick()}>
                                            Show
                                        </Button>
                                    </Stack>
                                </CardContent>
                            </Card>
                        }
                    </Grid>
                    <Grid item xs={3}>
                        {cookies.lastViewed && cookies.lastViewed.length > 2 &&
                            <Card>
                                <CardContent>
                                    <Typography variant="h6">Room-ESP32</Typography>
                                    <Stack direction="row" justifyContent="flex-end">
                                        <Button onClick={() => onLastViewedDeviceClick()}>
                                            Show
                                        </Button>
                                    </Stack>
                                </CardContent>
                            </Card>
                        }
                    </Grid>
                </Grid>
            </Grid>
        </Grid>
    );
};