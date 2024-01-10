import * as React from 'react';
import { PageDrawer } from './pageDrawer.component';
import { AppBar, Box, IconButton, Toolbar, Typography, Container } from '@mui/material';
import MenuIcon from '@mui/icons-material/Menu';
import LogoutIcon from '@mui/icons-material/Logout';
import { useUserAuth } from '../hooks/useUserAuth';
import { useNavigate } from "react-router-dom";

export const Layout = ({ children }) => {

    const [drawerOpen, setDrawerOpen] = React.useState(false);
    const { logout } = useUserAuth();
    const navigate = useNavigate();

    const handleDrawerToggle = () => {
        setDrawerOpen(!drawerOpen);
    };

    const handleLogout = () => {
        logout();
        navigate('/login');
    };

    return (
        <Box sx={{ display: 'flex' }}>
            <AppBar
                position="fixed" sx={{ background: '#101418', zIndex: (theme) => theme.zIndex.drawer + 1 }}
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
            <Box component="main" sx={{ flexGrow: 1, p: 3 }}>
                <Toolbar />
                <Container>
                    {children}
                </Container>
            </Box>
        </Box>
    );
}

