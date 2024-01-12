import { Layout } from "../../core/layout/layout.component.js";
import { DevicesDataGrid } from "./devicesDataGrid.component.js";
import { LastOpenedDevices } from "./components/lastOpenedDevices.component.js";
import { Container } from '@mui/material';

export const DeviceListPage = () => {
    return (
        <Layout>
            <Container >
                <LastOpenedDevices />
                <DevicesDataGrid />
            </Container>
        </Layout>
    );
}