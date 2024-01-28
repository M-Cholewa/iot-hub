
import LoadingButton from '@mui/lab/LoadingButton';
import axios from 'axios';
import React, { useState } from 'react';
import { serverAddress } from '../../core/config/server';
import { useUserAuth } from '../../core/hooks/useUserAuth';

import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import { Alert, Avatar, Box, CssBaseline, Grid, Link, TextField, Typography } from '@mui/material';
import { useNavigate } from "react-router-dom";

function Copyright(props) {
    return (
        <Typography variant="body2" color="text.secondary" align="center" {...props}>
            {'Copyright © '}
            <Link color="inherit" href="https://mui.com/">
                IoT System
            </Link>{' '}
            {new Date().getFullYear()}
            {'.'}
        </Typography>
    );
}

export const LoginPage = () => {
    const [loading, setLoading] = useState(false);
    const [dispError, setDispError] = useState(false);
    const [errorMsg, setErrorMsg] = useState('');
    const [mail, setMail] = useState('');
    const [pass, setPass] = useState('');
    const { login } = useUserAuth();
    const navigate = useNavigate();

    const handleLogin = () => {
        setLoading(true);
        setDispError(false);

        axios
            .post(
                `${serverAddress}/Auth/login`,
                {
                    email: mail,
                    password: pass,
                },
                { validateStatus: (status) => status < 500 }
            )
            .then((res) => {
                if (res.status === 200) {
                    login(res.data);
                    navigate('/');
                    setLoading(false);
                } else {
                    setErrorMsg(`Błąd logowania: ${res.statusText}`);
                    setDispError(true);
                    setLoading(false);
                }
            })
            .catch((err) => {
                setErrorMsg(`Błąd logowania: ${err.message}`);
                setDispError(true);
                setLoading(false);
            });
    };

    return (
        <Grid container component="main" sx={{ height: '100vh' }}>
            <CssBaseline />
            <Grid
                item
                xs={false}
                sm={4}
                md={7}
                sx={{
                    backgroundImage: `url(/login-bg.jpg)`,
                    backgroundRepeat: 'no-repeat',
                    opacity: 0.6,
                    backgroundColor: (t) =>
                        t.palette.mode === 'light' ? t.palette.grey[50] : t.palette.grey[900],
                    backgroundSize: 'cover',
                    backgroundPosition: 'center',
                }}
            />
            <Grid item xs={12} sm={8} md={5} elevation={6} square="true">
                <Box
                    sx={{
                        my: 8,
                        mx: 4,
                        display: 'flex',
                        flexDirection: 'column',
                        alignItems: 'center'
                    }}
                >
                    <Avatar sx={{ m: 1, bgcolor: 'primary.main' }}>
                        <LockOutlinedIcon />
                    </Avatar>
                    <Typography component="h1" variant="h5">
                        Sign in
                    </Typography>
                    <Box component="form" noValidate sx={{ mt: 1 }}>

                        <TextField
                            margin="normal"
                            required
                            fullWidth
                            id="email"
                            label="Email Address"
                            name="email"
                            autoComplete="email"
                            autoFocus
                            value={mail}
                            onChange={(event) => {
                                setMail(event.target.value);
                            }}
                        />
                        <TextField
                            margin="normal"
                            required
                            fullWidth
                            name="password"
                            label="Password"
                            type="password"
                            id="password"
                            autoComplete="current-password"
                            value={pass}
                            onChange={(event) => {
                                setPass(event.target.value);
                            }}
                        />

                        <LoadingButton
                            size="small"
                            onClick={handleLogin}
                            loading={loading}
                            sx={{ mt: 3, mb: 2 }}
                            fullWidth
                            variant="contained"
                        >
                            Login
                        </LoadingButton>
                        {dispError ? <Alert severity="error">{errorMsg}</Alert> : <></>}

                        <Grid container>
                            <Grid item>
                                <Link href="register" variant="body2">
                                    {"Don't have an account? Sign Up"}
                                </Link>
                            </Grid>
                        </Grid>
                        <Copyright sx={{ mt: 5 }} />
                    </Box>
                </Box>
            </Grid>
        </Grid >
    );
}