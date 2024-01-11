import { Typography, Button, Stack, Card, CardContent } from "@mui/material";
export const LastOpenDevice = () => {
    return (
        <Card>
            <CardContent>
                <Typography variant="body1" mb={2}>Last viewed</Typography>
                <Typography variant="h4">Room-ESP32</Typography>
                <Typography variant="h5">Online</Typography>
                <Stack direction="row" justifyContent="flex-end">
                    <Button>
                        Show
                    </Button>
                </Stack>
            </CardContent>
        </Card>);
};