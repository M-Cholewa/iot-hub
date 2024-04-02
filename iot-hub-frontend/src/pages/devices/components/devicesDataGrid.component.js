import { useState } from 'react';
import { DataGrid, GridActionsCellItem } from '@mui/x-data-grid';
import { useDevicesData } from '../hooks/useDevicesData.js';
import { Button, Stack } from '@mui/material';
import { useNavigate } from "react-router-dom";

import { AppBar, Toolbar, Grid, Tooltip, IconButton, TextField } from '@mui/material';
import { Dialog, DialogTitle, DialogContent, DialogContentText, DialogActions } from '@mui/material';
import { CopyToClipboardButton } from '../../../core/components/copyToClipboardButton.js';

import RefreshIcon from '@mui/icons-material/Refresh';
import DeleteIcon from '@mui/icons-material/Delete';

const ShowDeviceButton = ({ deviceId }) => {
    const navigate = useNavigate();


    const onShowDeviceClick = () => {
        navigate(`/device/${deviceId}`);
    }

    return (
        <Button onClick={onShowDeviceClick}>
            Show
        </Button>
    );
}

export const DevicesDataGrid = () => {

    const [deviceCreateOpen, setDeviceCreateOpen] = useState(false);
    const [deviceCreateResultOpen, setDeviceCreateResultOpen] = useState(false);

    const [deviceName, setDeviceName] = useState('');

    const [deviceCreateResult, setDeviceCreateResult] = useState({ isSuccess: false, message: "" });

    const { devices, addDevice, refreshDevices, deleteDevice } = useDevicesData();

    const handleDeviceCreateOpen = () => {
        setDeviceCreateOpen(true);
    };

    const handleDeviceCreateClose = () => {
        setDeviceCreateOpen(false);
    };

    const handleDeviceCreateResultClose = () => {
        setDeviceCreateResultOpen(false);
    };

    const handleAdd = async () => {
        let _deviceCreateResult = await addDevice(deviceName);

        setDeviceCreateResult(_deviceCreateResult);
        setDeviceCreateOpen(false);
        setDeviceCreateResultOpen(true);
    }

    const handleDelete = (deviceId) => {
        deleteDevice(deviceId);
    };

    const handleRefresh = () => {
        refreshDevices();
    };

    const columns = [
        {
            field: 'device.id',
            headerName: 'ID',
            flex: 0.5,
            valueGetter: (params) =>
                params.row.device?.id ?? `Unknown`
        },
        {
            field: 'device.name',
            headerName: 'Name',
            flex: 1,
            valueGetter: (params) =>
                params.row.device?.name ?? `Unknown`
        },
        {
            field: 'deviceTelemetry.firmwareVersion',
            headerName: 'Firmware version',
            flex: 1,
            valueGetter: (params) =>
                params.row.deviceTelemetry?.firmwareVersion ?? `Unknown`
        },
        {
            field: 'deviceTelemetry.lastActivityUTC',
            headerName: 'Last activity',
            flex: 1.5,
            valueGetter: (params) => {
                const date = new Date(params.row.deviceTelemetry?.lastActivityUTC ?? `Unknown`);
                return date.toLocaleString();
            }
        },
        {
            field: 'deviceTelemetry.uptimeS',
            headerName: 'Up time (s)',
            flex: 1,
            valueGetter: (params) =>
                params.row.deviceTelemetry?.uptimeS ?? `Unknown`
        },
        {
            field: 'status',
            headerName: 'Status',
            flex: 0.7,
            valueGetter: (params) =>
                params.row.deviceTelemetry?.status ?? `Unknown`
        },
        {
            field: 'actions',
            type: 'actions',
            width: 80,
            getActions: (params) => [
                <GridActionsCellItem
                    icon={<DeleteIcon />}
                    label="Delete"
                    onClick={() => handleDelete(params.row.device.id)}
                    showInMenu
                />,
            ],
        },
        {
            field: 'actionShow',
            type: 'actions',
            width: 80,
            getActions: (params) => [
                <ShowDeviceButton deviceId={params.row.device.id} />
            ],
        },
    ];

    function getRowId(row) {
        return row.device.id;
    }

    return (
        <>
            <AppBar
                position="static"
                sx={{ background: '#101418' }}
                color="default"
                elevation={0}
            >
                <Toolbar>
                    <Grid container spacing={2} alignItems="center" justifyContent="flex-end">
                        <Grid item>
                            <Tooltip title="Add device">
                                <Button variant="contained" onClick={handleDeviceCreateOpen}>
                                    Add device
                                </Button>
                            </Tooltip>
                        </Grid>
                        <Grid item>
                            <Tooltip title="Reload">
                                <IconButton onClick={handleRefresh}>
                                    <RefreshIcon color="inherit" sx={{ display: 'block' }} />
                                </IconButton>
                            </Tooltip>
                        </Grid>
                    </Grid>
                </Toolbar>
            </AppBar>

            <Dialog
                open={deviceCreateOpen}
                onClose={handleDeviceCreateClose}
            >
                <DialogTitle>Add new device</DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        To add a new device, please enter the device name.
                    </DialogContentText>

                    <TextField
                        autoFocus
                        required
                        margin="dense"
                        id="deviceName"
                        name="deviceName"
                        label="Device Name"
                        type="text"
                        fullWidth
                        variant="standard"
                        value={deviceName}
                        onChange={(event) => {
                            setDeviceName(event.target.value);
                        }}
                    />
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleDeviceCreateClose}>Cancel</Button>
                    <Button type="submit" onClick={handleAdd}>Add</Button>
                </DialogActions>
            </Dialog>

            <Dialog
                open={deviceCreateResultOpen}
            >
                <DialogTitle>
                    {deviceCreateResult.isSuccess ? <>Device created successfully</> : <>Device creation failed</>}
                </DialogTitle>
                <DialogContent>
                    <DialogContentText >

                        {deviceCreateResult.isSuccess

                            ? <>Copy this Api key, it will be needed to connect the device to the server. It will not be displayed again.</>
                            : <> Error message: {deviceCreateResult.message}</>}


                    </DialogContentText>

                    {deviceCreateResult.isSuccess &&
                        <Stack
                            direction="row"
                            alignItems="center"
                            style={{ wordBreak: "break-all" }}
                            mt={2}
                        >
                            {deviceCreateResult.mqttApiKey}
                            <CopyToClipboardButton copyText={deviceCreateResult.mqttApiKey} />
                        </Stack>}

                </DialogContent>
                <DialogActions>
                    <Button onClick={handleDeviceCreateResultClose}>Close</Button>
                </DialogActions>
            </Dialog>

            <DataGrid
                rows={devices}
                columns={columns}
                initialState={{
                    pagination: {
                        paginationModel: {
                            pageSize: 5,
                        },
                    },
                }}
                getRowId={getRowId}
                pageSizeOptions={[5]}
                checkboxSelection
                disableRowSelectionOnClick
            />
        </>
    );
}