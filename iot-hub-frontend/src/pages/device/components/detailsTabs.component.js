import { useState } from 'react';
import { Box, Tab } from '@mui/material';
import { TabContext, TabList, TabPanel } from '@mui/lab';



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
                        <Tab label="Command line" value="1" />
                        <Tab label="Charts and telemetry" value="2" />
                        <Tab label="Logs" value="3" />
                        <Tab label="Twin representation" value="4" />
                    </TabList>
                </Box>
                <TabPanel value="1">Command line</TabPanel>
                <TabPanel value="2">Charts and telemetry</TabPanel>
                <TabPanel value="3">Logs</TabPanel>
                <TabPanel value="4">Twin representation</TabPanel>
            </TabContext>
        </Box>
    );
}