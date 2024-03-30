import { Input, InputAdornment, Grid, Box, Typography, Container, Backdrop, CircularProgress } from '@mui/material';
import { useContext, useRef, useEffect, useState } from "react";
import { DeviceContext } from "../context/deviceContext.js";
import SendIcon from '@mui/icons-material/Send';
import { executeDirectMethod } from "../services/executeDirectMethod.js";
import LoadingButton from '@mui/lab/LoadingButton';
import { useCliData } from "../hooks/useCliData.js";

export const CliTab = () => {
    const [method, setMethod] = useState("");
    const [payload, setPayload] = useState("{}");
    const scrollableBoxRef = useRef(null);
    const device = useContext(DeviceContext);
    const { consoleLoading, consoleRecords, refreshConsoleRecords } = useCliData(device.id);
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(false);

    const scrollToBottom = () => {
        if (scrollableBoxRef?.current) {
            scrollableBoxRef.current.scrollTop = scrollableBoxRef.current.scrollHeight;
        }
    };

    useEffect(() => {
        scrollToBottom();
    }, [consoleRecords]);


    const handleSend = () => {

        setError(null);

        if (method === "") {
            setError("Method name cannot be empty");
            return;
        }

        // check if payload is valid JSON
        try {
            JSON.parse(payload);
        } catch (e) {
            setError("Payload is not valid JSON");
            return;
        }

        setLoading(true);

        executeDirectMethod(device.id, method, payload)
            .then((res) => {

                refreshConsoleRecords();

            })
            .catch((err) => {
                setError(err);
            })
            .finally(() => {
                setLoading(false);
            });
    };

    return (<Container>

        <Grid container spacing={2} justifyContent="left" alignContent="center" sx={{ position: "relative" }}>
            <Backdrop
                sx={{ color: '#fff', zIndex: (theme) => theme.zIndex.drawer + 1, position: "absolute" }}
                open={loading || consoleLoading}
            >
                <CircularProgress color="inherit" />
            </Backdrop>

            <Grid item xs={12}>
                <Box
                    sx={{
                        bgcolor: 'black',
                        border: '1px solid rgba(255,255,255,0.7)',
                        borderRadius: 2,
                        overflowY: "scroll",
                        maxHeight: "60vh",
                        height: "60vh",
                        flexDirection: "column-reverse"
                    }}
                    p={2}
                    ref={scrollableBoxRef}
                >
                    {consoleRecords.map((item, index) =>
                        <div key={index}>
                            <Typography variant="body2" gutterBottom me={3}>
                                <span style={{ color: "#32afff" }}>
                                    {item.dateUTC} &nbsp;
                                </span>
                                <span style={{ color: "#4AF626" }}>
                                    {device.name}: &nbsp;
                                </span>
                                {item.method}: {item.payload}
                                &nbsp;-&gt;&nbsp;
                                {(item.rpcResult === "SUCCESS")
                                    ? <span>{item.rpcResult}</span>
                                    : <span style={{ color: "#ef2929" }}>
                                        {item.rpcResult}
                                    </span>}

                            </Typography>
                            <Typography variant="body2" gutterBottom>
                                {(item.rpcResult === "SUCCESS") &&
                                    <span style={{ whiteSpace: "pre-wrap" }}>
                                        &nbsp;
                                        {item.responseDataJson}
                                    </span>}

                            </Typography>
                        </div>
                    )}
                </Box>
            </Grid>
            <Grid item xs={3}>
                <Input
                    value={method}
                    onChange={(e) => setMethod(e.target.value)}
                    startAdornment={<InputAdornment position="start">Method</InputAdornment>}
                />
            </Grid>
            <Grid item xs={8}>
                <Input
                    value={payload}
                    onChange={(e) => setPayload(e.target.value)}
                    sx={{ width: "100%" }}
                    startAdornment={<InputAdornment position="start">Payload</InputAdornment>}
                />
            </Grid>
            <Grid item xs={1} alignContent="end" justifyContent="end">
                <LoadingButton
                    loading={loading || consoleLoading}
                    loadingPosition="end"
                    endIcon={<SendIcon />}
                    onClick={handleSend}>
                    Send
                </LoadingButton>
            </Grid>
            <Grid item xs={12}>
                {error && <Typography variant="body2" color="error">{error}</Typography>}
            </Grid>
        </Grid>



    </Container>);
};