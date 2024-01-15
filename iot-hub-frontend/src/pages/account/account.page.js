import { Divider, Typography, Container, Grid, Button, Stack, Card, CardContent } from "@mui/material";
import { Layout } from "../../core/layout/layout.component";
import { useUserAuth } from '../../core/hooks/useUserAuth';

const CardItem = ({ title, value }) => {
    return (
        <Card>
            <CardContent>
                <Typography variant="body1" gutterBottom>
                    {title}
                </Typography>
                <Typography variant="h5" gutterBottom align="center">
                    {value}
                </Typography>
            </CardContent>
        </Card>);
}

export const AccountPage = () => {
    const { getUser } = useUserAuth();

    const user = getUser();
    const roleKeys = user.roles?.map(role => role.key) ?? [];

    return (
        <Layout>
            <Container >
                <Grid container justifyContent="space-around" alignItems="center" mt={2}>
                    <Grid item xs={3}>
                        <CardItem title="ID" value={user.id ?? "?"} />
                    </Grid>
                    <Grid item xs={3}>
                        <CardItem title="Mail" value={user.email ?? "?"} />
                    </Grid>
                    <Grid item xs={3}>
                        <CardItem title="Roles" value={roleKeys.toString()} />
                    </Grid>
                </Grid>
                <Stack direction="column" justifyContent="space-between" spacing={2} mt={2}>
                    <Typography variant="h4" mt={2}>
                        Security
                    </Typography>
                    <Divider flexItem />
                    <Grid container justifyContent="space-between">
                        <Grid item>
                            <Typography>
                                Change mail
                            </Typography>
                        </Grid>
                        <Grid item>
                            <Button variant="outlined">show</Button>
                        </Grid>
                    </Grid>
                    <Grid container justifyContent="space-between">
                        <Grid item>
                            <Typography>
                                Change password
                            </Typography>
                        </Grid>
                        <Grid item>
                            <Button variant="outlined">show</Button>
                        </Grid>
                    </Grid>
                    <Grid container justifyContent="space-between">
                        <Grid item>
                            <Typography>
                                Remove account
                            </Typography>
                        </Grid>
                        <Grid item>
                            <Button variant="outlined">show</Button>
                        </Grid>
                    </Grid>
                </Stack>
            </Container>
        </Layout>
    );
};