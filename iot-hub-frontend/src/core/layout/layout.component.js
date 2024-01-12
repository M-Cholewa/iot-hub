import * as React from 'react';
import { PageDrawer } from './pageDrawer.component';
import { AppBar, Box, IconButton, Toolbar, Typography, Stack, Chip } from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import LogoutIcon from '@mui/icons-material/Logout';
import { useUserAuth } from '../hooks/useUserAuth';
import { useNavigate } from "react-router-dom";
import Link from '@mui/material/Link';

function Copyright(props) {
    return (
        <Typography variant="body2" color="text.secondary" align="center" mt={3} {...props}>
            {'Copyright © '}
            <Link color="inherit" href="#">
                IoT Hub
            </Link>{' '}
            {new Date().getFullYear()}
            {'.'}
        </Typography>
    );
}

export const Layout = ({ children }) => {

    const [drawerOpen, setDrawerOpen] = React.useState(false);
    const { logout, getUser } = useUserAuth();
    const navigate = useNavigate();

    const handleDrawerToggle = () => {
        setDrawerOpen(!drawerOpen);
    };

    const handleLogout = () => {
        logout();
        navigate('/login');
    };

    const user = getUser();

    console.log(user)

    return (
        <Box sx={{ display: 'flex' }}>

            <AppBar sx={{ background: '#101418', zIndex: (theme) => theme.zIndex.drawer + 1 }}
            >
                <Toolbar>
                    <IconButton
                        color='primary'
                        aria-label="open drawer"
                        edge="start"
                        onClick={handleDrawerToggle}
                        sx={{ mr: 2, display: { sm: 'none' } }}
                    >
                        <MenuIcon />
                    </IconButton>
                    <Typography variant="h6" color='primary' noWrap component="div">
                        IoT Hub
                    </Typography>
                    <Box sx={{ flexGrow: 1 }} />
                    <Box sx={{ display: { xs: 'none', md: 'flex' } }}>
                        <Stack direction="row" spacing={1} alignItems="center">
                            <Chip label={`Hello ${user.email}`} color="primary" />
                            <Chip label="MQTT connected" color="success" />
                        </Stack>
                        <IconButton
                            size="large"
                            aria-label="Logout"
                            color="primary"
                            onClick={handleLogout}
                        >
                            <LogoutIcon />
                        </IconButton>
                    </Box>
                </Toolbar>
            </AppBar>

            <PageDrawer
                drawerOpen={drawerOpen}
                handleDrawerToggle={handleDrawerToggle}
            />
            <Box component="main">
                <Toolbar />
                <div>
                    {children}
                </div>
                <Copyright />
            </Box>
        </Box>
    );
}

