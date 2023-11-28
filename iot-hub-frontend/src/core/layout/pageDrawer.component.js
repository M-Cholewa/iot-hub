import * as React from 'react';
import { drawerItemsAdmin } from './drawerConstants';
import {
    Box, Drawer, List, ListItem, ListItemButton, ListItemIcon, ListItemText, Divider, Toolbar
} from '@mui/material';
import { Link, useLocation } from 'react-router-dom';

const drawerWidth = 240;

export const PageDrawer = ({ drawerOpen, handleDrawerToggle }) => {
    const location = useLocation();

    const drawer = (
        <div>
            <Toolbar />
            <List>
                {drawerItemsAdmin.base.map((item) => (
                    <Link to={item.path} style={{ textDecoration: 'none' }} key={item.path}>
                        <ListItem disablePadding>
                            <ListItemButton>
                                <ListItemIcon>
                                    <item.icon color='primary' />
                                </ListItemIcon>
                                <ListItemText primary={item.label} />
                            </ListItemButton>
                        </ListItem>
                    </Link>
                ))}
            </List>
            <Divider />
            <List>
                {drawerItemsAdmin.account.map((item) => (
                    <Link to={item.path} style={{ textDecoration: 'none' }} key={item.path}>
                        <ListItem disablePadding>
                            <ListItemButton selected={location.pathname === item.path}>
                                <ListItemIcon>
                                    <item.icon color='primary' />
                                </ListItemIcon>
                                <ListItemText primary={item.label} />
                            </ListItemButton>
                        </ListItem>
                    </Link>
                ))}
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
                {drawer}
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
                {drawer}
            </Drawer>
        </Box>
    );
}