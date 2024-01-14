import { Layout } from "../../core/layout/layout.component.js";
import { Breadcrumbs, Link, Typography } from '@mui/material';
import { useParams } from 'react-router-dom';
import { useDeviceData } from "./hooks/useDeviceData.js";
import { DetailsTabs } from "./components/detailsTabs.component.js";
import { useNavigate } from "react-router-dom";
import { DeviceContext } from "./context/deviceContext.js";

const NullDevice = () => {
    const navigate = useNavigate();

    const handleClick = () => {
        navigate('/devices');
    }

    return (
        <Layout>
            <Breadcrumbs aria-label="breadcrumb">
                <Link
                    underline="hover"
                    color="inherit"
                    href="#"
                    onClick={handleClick}
                >
                    Devices
                </Link>
                <Typography color="text.primary">?</Typography>
            </Breadcrumbs>
            <Typography variant="h5" gutterBottom>
                Device not found
            </Typography>
        </Layout>
    );
}

export const DeviceDetailsPage = () => {
    const { id } = useParams();
    const { device } = useDeviceData(id);

    const navigate = useNavigate();

    const handleClick = () => {
        navigate('/devices');
    }

    if (device === null) {
        return <NullDevice />
    }

    return (
        <Layout>
            <Breadcrumbs aria-label="breadcrumb">
                <Link
                    underline="hover"
                    color="inherit"
                    href="#"
                    onClick={handleClick}
                >
                    Devices
                </Link>
                <Typography color="text.primary">{device.name}</Typography>
            </Breadcrumbs>
            <DeviceContext.Provider value={device}>
                <DetailsTabs />
            </DeviceContext.Provider>
        </Layout>
    );
}