import { Divider, Typography, Container, Grid, Button, Stack, Card, CardContent } from "@mui/material";
import { Dialog, DialogActions, DialogContent, DialogContentText, DialogTitle, TextField } from "@mui/material";
import { Layout } from "../../core/layout/layout.component";
import { useUserAuth } from '../../core/hooks/useUserAuth';
import { useState } from "react";
import { useAccountData } from "./hooks/useAccountData";
import LoadingButton from '@mui/lab/LoadingButton';
import { red } from "@mui/material/colors";
import { useNavigate } from "react-router-dom";

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
    const { getUser, logout } = useUserAuth();
    const navigate = useNavigate();

    const user = getUser();
    const roleKeys = user.roles?.map(role => role.key) ?? [];

    const { loading, updateEmail, updatePassword, deleteAccount } = useAccountData(user.id);

    const [newEmail, setNewEmail] = useState("");
    const [emailChangeOpen, setEmailChangeOpen] = useState(false);

    const [oldPassword, setOldPassword] = useState("");
    const [newPassword, setNewPassword] = useState("");
    const [passwordChangeOpen, setPasswordChangeOpen] = useState(false);

    const [deleteOpen, setDeleteOpen] = useState(false);

    const [emailChangeResult, setEmailChangeResult] = useState({ isSuccess: false, message: "" });
    const [passwordChangeResult, setPasswordChangeResult] = useState({ isSuccess: false, message: "" });
    const [accountDeleteResult, setAccountDeleteResult] = useState({ isSuccess: false, message: "" });


    const handleEmailChangeClose = () => {
        if (loading || emailChangeResult.isSuccess)
            return;
        setEmailChangeOpen(false);
    };

    const handleEmailChangeOpen = () => {
        setEmailChangeOpen(true);
    };

    const handlePasswordChangeClose = () => {
        if (loading || passwordChangeResult.isSuccess)
            return;
        setPasswordChangeOpen(false);
    };

    const handlePasswordChangeOpen = () => {
        setPasswordChangeOpen(true);
    };

    const handleDeleteClose = () => {
        if (loading || accountDeleteResult.isSuccess)
            return;
        setDeleteOpen(false);
    };

    const handleDeleteOpen = () => {
        setDeleteOpen(true);
    };

    const handleEmailChange = async () => {
        let response = await updateEmail(newEmail);
        setEmailChangeResult(response);
    };

    const handlePassChange = async () => {
        let response = await updatePassword(oldPassword, newPassword);
        setPasswordChangeResult(response);
    };

    const handleDelete = async () => {
        let response = await deleteAccount();
        setAccountDeleteResult(response);
    };

    const handleLogout = () => {
        logout();
        navigate('/login');
    };

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
                            <LoadingButton
                                variant="outlined"
                                loading={loading}
                                onClick={handleEmailChangeOpen}>
                                show
                            </LoadingButton>
                        </Grid>
                    </Grid>
                    <Grid container justifyContent="space-between">
                        <Grid item>
                            <Typography>
                                Change password
                            </Typography>
                        </Grid>
                        <Grid item>
                            <LoadingButton
                                variant="outlined"
                                loading={loading}
                                onClick={handlePasswordChangeOpen}>
                                show
                            </LoadingButton>
                        </Grid>
                    </Grid>
                    <Grid container justifyContent="space-between">
                        <Grid item>
                            <Typography>
                                Remove account
                            </Typography>
                        </Grid>
                        <Grid item>
                            <LoadingButton
                                variant="outlined"
                                loading={loading}
                                onClick={handleDeleteOpen}>
                                show
                            </LoadingButton>
                        </Grid>
                    </Grid>
                </Stack>


                <Dialog
                    open={emailChangeOpen}
                    onClose={handleEmailChangeClose}
                >
                    <DialogTitle>Change email address</DialogTitle>
                    {!emailChangeResult.isSuccess &&
                        <DialogContent>
                            <DialogContentText>
                                To change your email address, please enter your new email address here.
                            </DialogContentText>

                            <TextField
                                autoFocus
                                required
                                margin="dense"
                                id="newEmail"
                                name="newEmail"
                                label="New email"
                                type="text"
                                fullWidth
                                variant="standard"
                                value={newEmail}
                                onChange={(event) => {
                                    setNewEmail(event.target.value);
                                }}
                            />

                            <Typography color="error">{passwordChangeResult.message}</Typography>

                        </DialogContent>}
                    {emailChangeResult.isSuccess &&
                        <DialogContent>
                            <DialogContentText>
                                Your email address has been changed.
                            </DialogContentText>
                        </DialogContent>}
                    {!emailChangeResult.isSuccess &&
                        <DialogActions>
                            <Button onClick={handleEmailChangeClose}>Close</Button>
                            <Button type="submit" onClick={handleEmailChange}>Change</Button>
                        </DialogActions>}
                    {emailChangeResult.isSuccess &&
                        <DialogActions>
                            <Button onClick={handleLogout}>Logout</Button>
                        </DialogActions>}
                </Dialog>


                <Dialog
                    open={passwordChangeOpen}
                    onClose={handlePasswordChangeClose}
                >
                    <DialogTitle>Change password</DialogTitle>
                    {!passwordChangeResult.isSuccess &&
                        <DialogContent>
                            <DialogContentText>
                                To change your password, please enter your old and new password here.
                            </DialogContentText>
                            <TextField
                                autoFocus
                                required
                                margin="dense"
                                id="oldPassword"
                                name="oldPassword"
                                label="Old password"
                                type="text"
                                fullWidth
                                variant="standard"
                                value={oldPassword}
                                onChange={(event) => {
                                    setOldPassword(event.target.value);
                                }}
                            />

                            <TextField
                                autoFocus
                                required
                                margin="dense"
                                id="newPassword"
                                name="newPassword"
                                label="New password"
                                type="text"
                                fullWidth
                                variant="standard"
                                value={newPassword}
                                onChange={(event) => {
                                    setNewPassword(event.target.value);
                                }}
                            />
                            <Typography color="error">{passwordChangeResult.message}</Typography>
                        </DialogContent>
                    }
                    {passwordChangeResult.isSuccess &&
                        <DialogContent>
                            <DialogContentText>
                                Your password has been changed.
                            </DialogContentText>
                        </DialogContent>}
                    {!passwordChangeResult.isSuccess &&
                        <DialogActions>
                            <Button onClick={handlePasswordChangeClose}>Close</Button>
                            <Button type="submit" onClick={handlePassChange}>Change</Button>
                        </DialogActions>}
                    {passwordChangeResult.isSuccess &&
                        <DialogActions>
                            <Button onClick={handleLogout}>Logout</Button>
                        </DialogActions>}

                </Dialog>



                <Dialog
                    open={deleteOpen}
                    onClose={handleDeleteClose}
                >
                    <DialogTitle>Delete Account</DialogTitle>
                    {!accountDeleteResult.isSuccess &&
                        <DialogContent>
                            <DialogContentText>
                                Are you sure you want to delete your account?
                            </DialogContentText>
                            <Typography color="error">{accountDeleteResult.message}</Typography>
                        </DialogContent>
                    }
                    {accountDeleteResult.isSuccess &&
                        <DialogContent>
                            <DialogContentText>
                                Your account has been deleted.
                            </DialogContentText>
                        </DialogContent>}

                    {!accountDeleteResult.isSuccess &&
                        <DialogActions>
                            <Button onClick={handleDeleteClose}>Close</Button>
                            <Button type="submit" onClick={handleDelete}>Delete</Button>
                        </DialogActions>}
                    {accountDeleteResult.isSuccess &&
                        <DialogActions>
                            <Button onClick={handleLogout}>Logout</Button>
                        </DialogActions>}
                </Dialog>


            </Container>
        </Layout>
    );
};