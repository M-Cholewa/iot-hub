import { Layout } from "../../core/layout/layout.component.js";
import { Grid } from '@mui/material';
import { DevicesOnline, ErrorCount, MessagesLogged } from "./charts/index.js";
import { LastOpenDevice, DevicesCountTile, DevicesOnlineCountTile, TotalMessages } from "./tiles/index.js";
import { LastNotifications } from "./lastNotifications.js";

export const DashboardPage = () => {

    return (
        <Layout>
            <Grid
                container
                rowSpacing={2}
                direction="row"
                justifyContent="space-around"
                alignItems="center"
                mt={0.5}>
                <Grid item xs={4}>
                    <DevicesOnline />
                </Grid>
                <Grid item xs={2}>
                    <DevicesCountTile />
                </Grid>
                <Grid item xs={2}>
                    <DevicesOnlineCountTile />
                </Grid>
                <Grid item xs={2}>
                    <TotalMessages />
                </Grid>
                <Grid item xs={4}>
                    <MessagesLogged />
                </Grid>
                <Grid item xs={4}>
                    <ErrorCount />
                </Grid>


                <Grid item>
                    <LastOpenDevice />
                </Grid>
                <Grid item xs={6}>
                    <LastNotifications />
                </Grid>
            </Grid>


        </Layout>
    );
}