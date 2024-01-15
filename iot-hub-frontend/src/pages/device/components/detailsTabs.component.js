import { useState } from 'react';
import { Box, Tab } from '@mui/material';
import { TabContext, TabList, TabPanel } from '@mui/lab';
import { CliTab, LogsTab, TelemetryTab } from '../components';


export const DetailsTabs = () => {
    const [value, setValue] = useState('1');

    const handleChange = (event, newValue) => {
        setValue(newValue);
    };

    return (
        <Box sx={{ width: '100%', typography: 'body1' }}>
            <TabContext value={value}>
                <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                    <TabList onChange={handleChange} aria-label="lab API tabs example">
                        <Tab label="Charts and telemetry" value="1" />
                        <Tab label="Command line" value="2" />
                        <Tab label="Logs" value="3" />
                    </TabList>
                </Box>
                <TabPanel value="1"><TelemetryTab /></TabPanel>
                <TabPanel value="2"><CliTab /></TabPanel>
                <TabPanel value="3"><LogsTab /></TabPanel>
            </TabContext>
        </Box>
    );
}