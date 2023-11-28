import DashboardIcon from '@mui/icons-material/Dashboard';
import DeviceThermostatIcon from '@mui/icons-material/DeviceThermostat';
import PersonIcon from '@mui/icons-material/Person';
import LogoutIcon from '@mui/icons-material/Logout';
import AdminPanelSettingsIcon from '@mui/icons-material/AdminPanelSettings';

export const drawerItemsUser = {
    base: [
        {
            label: "Dashboard",
            icon: DashboardIcon,
            path: "/",
        },
        {
            label: "Devices",
            icon: DeviceThermostatIcon,
            path: "/devices",
        },
    ],
    account: [
        {
            label: "Account",
            icon: PersonIcon,
            path: "/account",
        },
        {
            label: "Logout",
            icon: LogoutIcon,
            path: "/logout",
        },
    ],
};

export const drawerItemsAdmin = {
    base: [
        {
            label: "Dashboard",
            icon: DashboardIcon,
            path: "/",
        },
        {
            label: "Devices",
            icon: DeviceThermostatIcon,
            path: "/devices",
        },
        {
            label: "Admin",
            icon: AdminPanelSettingsIcon,
            path: "/admin",
        },
    ],
    account: [
        {
            label: "Account",
            icon: PersonIcon,
            path: "/account",
        },
        {
            label: "Logout",
            icon: LogoutIcon,
            path: "/logout",
        },
    ],
};