import * as React from 'react';
import { drawerItems } from './drawerConstants';
import {
    Box, Drawer, List, ListItem, ListItemButton, ListItemIcon, ListItemText, Divider, Toolbar, Stack, Typography
} from '@mui/material';
import LogoutIcon from '@mui/icons-material/Logout';
import { Link, useLocation } from 'react-router-dom';
import { useNavigate } from "react-router-dom";
import { useUserAuth } from '../hooks/useUserAuth'

const drawerWidth = 240;

export const PageDrawer = ({ drawerOpen, handleDrawerToggle }) => {
    const location = useLocation();
    const { getUserRoles, logout } = useUserAuth();
    const navigate = useNavigate();
    const userRoles = getUserRoles();
    const isAdmin = userRoles.some(role => role.key === 'ADMIN');

    const handleLogout = () => {
        logout();
        navigate('/login');
    };

    const drawer = (
        <div>
            <Toolbar />
            <List>
                {drawerItems.base.map((item) => {

                    if (item.path.includes('/admin') && !isAdmin) {
                        return (null);
                    }

                    return (
                        <Link to={item.path} style={{ textDecoration: 'none', color: '#FFF' }} key={item.path}>
                            <ListItem disablePadding>
                                <ListItemButton selected={location.pathname === item.path}>
                                    <ListItemIcon>
                                        <item.icon color='primary' />
                                    </ListItemIcon>
                                    <ListItemText primary={item.label} />
                                </ListItemButton>
                            </ListItem>
                        </Link>
                    )
                })}
            </List>
            <Divider />
            <List>
                {drawerItems.account.map((item) => {
                    if (item.path.includes('/admin') && !isAdmin) {
                        return (null);
                    }

                    return (
                        <Link to={item.path} style={{ textDecoration: 'none', color: '#FFF' }} key={item.path}>
                            <ListItem disablePadding>
                                <ListItemButton selected={location.pathname === item.path} >
                                    <ListItemIcon>
                                        <item.icon color='primary' />
                                    </ListItemIcon>
                                    <ListItemText primary={item.label} />
                                </ListItemButton>
                            </ListItem>
                        </Link>
                    )
                })}

                <ListItem disablePadding>
                    <ListItemButton onClick={handleLogout} >
                        <ListItemIcon>
                            <LogoutIcon color='primary' />
                        </ListItemIcon>
                        <ListItemText primary="Logout" />
                    </ListItemButton>
                </ListItem>
            </List>
        </div>
    );

    return (
        <Box
            component="nav"
            sx={{ width: { sm: drawerWidth }, flexShrink: { sm: 0 } }}
            aria-label="mailbox folders"
        >
            {/* The implementation can be swapped with js to avoid SEO duplication of links. */}
            <Drawer
                variant="temporary"
                open={drawerOpen}
                onClose={handleDrawerToggle}
                ModalProps={{
                    keepMounted: true, // Better open performance on mobile.
                }}
                sx={{
                    display: { xs: 'block', sm: 'none' },
                    '& .MuiDrawer-paper': { boxSizing: 'border-box', width: drawerWidth },
                }}
            >
                <Stack
                    direction="column"
                    sx={{ height: "100%" }}
                    justifyContent="space-between">
                    {drawer}
                    <Typography variant="body2" color="text.secondary" m={1}>
                        2.5.1-beta, 12.01.2024
                    </Typography>
                </Stack>
            </Drawer>
            <Drawer
                variant="permanent"
                sx={{
                    display: { xs: 'none', sm: 'block' },
                    width: drawerWidth,
                    flexShrink: 0,
                    [`& .MuiDrawer-paper`]: { width: drawerWidth, boxSizing: 'border-box' },
                }}
                open
            >
                <Stack
                    direction="column"
                    sx={{ height: "100%" }}
                    justifyContent="space-between">
                    {drawer}
                    <Typography variant="body2" color="text.secondary" m={1}>
                        2.5.1-beta, 12.01.2024
                    </Typography>
                </Stack>
            </Drawer>

        </Box>
    );
}