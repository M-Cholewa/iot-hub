import { Typography, Button, Stack, Card, CardContent, Grid } from "@mui/material";
import { useNavigate } from "react-router-dom";

export const LastOpenedDevices = () => {

    const navigate = useNavigate();

    const onLastViewedDeviceClick = () => {
        navigate('/device/6220654d-cfdd-4ab6-9a62-60c31ed307ab');
    };

    return (
        <Grid container alignItems="center" direction="row" my={3} >
            <Grid item xs={2}>
                <Typography variant="h5" mb={2}>Last viewed</Typography>
            </Grid>
            <Grid item xs={10}>
                <Grid container alignItems="center" justifyContent="space-between" direction="row">
                    <Grid item xs={3}>
                        <Card>
                            <CardContent>
                                <Typography variant="h6">Room-ESP32</Typography>
                                <Stack direction="row" justifyContent="flex-end">
                                    <Button onClick={onLastViewedDeviceClick}>
                                        Show
                                    </Button>
                                </Stack>
                            </CardContent>
                        </Card>
                    </Grid>
                    <Grid item xs={3}>
                        <Card>
                            <CardContent>
                                <Typography variant="h6">Room-ESP32</Typography>
                                <Stack direction="row" justifyContent="flex-end">
                                    <Button onClick={onLastViewedDeviceClick}>
                                        Show
                                    </Button>
                                </Stack>
                            </CardContent>
                        </Card>
                    </Grid>
                    <Grid item xs={3}>
                        <Card>
                            <CardContent>
                                <Typography variant="h6">Room-ESP32</Typography>
                                <Stack direction="row" justifyContent="flex-end">
                                    <Button onClick={onLastViewedDeviceClick}>
                                        Show
                                    </Button>
                                </Stack>
                            </CardContent>
                        </Card>
                    </Grid>
                </Grid>
            </Grid>
        </Grid>
    );
};