import { useRef, useContext, useEffect } from "react";
import { Box, Typography, Backdrop, CircularProgress } from "@mui/material";
import { DeviceContext } from "../../context/deviceContext";
import { useDeviceGeneralLogsData } from "../../hooks/useDeviceGeneralLogsData.js";

export const Console = () => {
    const scrollableBoxRef = useRef(null);
    const device = useContext(DeviceContext);
    const { logs, loading: logsLoading } = useDeviceGeneralLogsData(device.id);

    const scrollToBottom = () => {
        if (scrollableBoxRef?.current) {
            scrollableBoxRef.current.scrollTop = scrollableBoxRef.current.scrollHeight;
        }
    };

    useEffect(() => {
        scrollToBottom();
    }, [logs]);

    return (
        <Box
            sx={{
                bgcolor: 'black',
                border: '1px solid rgba(255,255,255,0.7)',
                borderRadius: 2,
                overflowY: "scroll",
                maxHeight: "15vh",
                height: "15vh",
                flexDirection: "column-reverse",
                position: "relative"
            }}
            p={2}
            ref={scrollableBoxRef}
        >

            <Backdrop
                sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1, position: "absolute" }}
                open={logsLoading}
            >
                <CircularProgress color="inherit" />
            </Backdrop>

            {logs.map((log, index) => (
                <Typography key={index} variant="body2">
                    <span>{new Date(log.dateUTC).toLocaleString()}: &nbsp;</span>
                    <span>{log.message}</span>
                </Typography>

            ))}
        </Box>
    );
}