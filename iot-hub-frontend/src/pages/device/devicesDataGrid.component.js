import * as React from 'react';
import { DataGrid, GridActionsCellItem } from '@mui/x-data-grid';
import { useDevicesData } from './hooks/useDevicesData.js';
import { Button } from '@mui/material';
import { useNavigate } from "react-router-dom";

import { AppBar, Toolbar, Grid, Tooltip, IconButton } from '@mui/material';
import RefreshIcon from '@mui/icons-material/Refresh';
import DeleteIcon from '@mui/icons-material/Delete';

const columns = [
    {
        field: 'id',
        headerName: 'ID',
        width: 250
    },
    {
        field: 'name',
        headerName: 'Name',
        width: 150,
    },
    {
        field: 'deviceTwin.fwVersion',
        headerName: 'Firmware version',
        width: 150,
        valueGetter: (params) =>
            params.row.deviceTwin?.fwVersion ?? `Unknown`
    },
    {
        field: 'deviceTwin.updateTime',
        headerName: 'Last activity',
        width: 110,
        valueGetter: (params) =>
            params.row.deviceTwin?.updateTime ?? `Unknown`
    },
    {
        field: 'deviceTwin.upTime',
        headerName: 'Up time',
        width: 110,
        valueGetter: (params) =>
            params.row.deviceTwin?.upTime ?? `Unknown`
    },
    {
        field: 'status',
        headerName: 'Status',
        width: 110,
        valueGetter: (params) => {
            if (!params.row.deviceTwin?.updateTime) {
                return "Unknown";
            }
            let updateDate = new Date(params.row.deviceTwin.updateTime);
            let currentDate = new Date();
            let diff = currentDate.getTime() - updateDate.getTime();

            if (diff < 2 * 60) { // 2 minutes
                return "Online";
            }

            return "Offline";
        }
    },
    {
        field: 'actions',
        type: 'actions',
        width: 80,
        getActions: (params) => [
            <GridActionsCellItem
                icon={<DeleteIcon />}
                label="Delete"
                showInMenu
            />,
        ],
    },
    {
        field: 'actionShow',
        type: 'actions',
        width: 80,
        getActions: (params) => [
            <ShowDeviceButton deviceId={params.row.id} />
        ],
    },
];

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

    const { devices } = useDevicesData();

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
                                <Button variant="contained" >
                                    Add device
                                </Button>
                            </Tooltip>
                        </Grid>
                        <Grid item>
                            <Tooltip title="Reload">
                                <IconButton>
                                    <RefreshIcon color="inherit" sx={{ display: 'block' }} />
                                </IconButton>
                            </Tooltip>
                        </Grid>
                    </Grid>
                </Toolbar>
            </AppBar>

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
                pageSizeOptions={[5]}
                checkboxSelection
                disableRowSelectionOnClick
            />
        </>
    );
}