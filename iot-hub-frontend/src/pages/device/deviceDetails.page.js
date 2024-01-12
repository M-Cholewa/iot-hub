import { Layout } from "../../core/layout/layout.component.js";
import { Breadcrumbs, Link, Typography } from '@mui/material';
import { useParams } from 'react-router-dom';
import { useDeviceData } from "./hooks/useDeviceData.js";
import { DetailsTabs } from "./components/detailsTabs.component.js";

const NullDevice = () => {
    return (
        <Layout>
            <Breadcrumbs aria-label="breadcrumb">
                <Link
                    underline="hover"
                    color="inherit"
                    href="/devices"
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

    if (device === null) {
        return <NullDevice />
    }

    return (
        <Layout>
            <Breadcrumbs aria-label="breadcrumb">
                <Link
                    underline="hover"
                    color="inherit"
                    href="/devices"
                >
                    Devices
                </Link>
                <Typography color="text.primary">{device.name}</Typography>
            </Breadcrumbs>

            <DetailsTabs />
        </Layout>
    );
}