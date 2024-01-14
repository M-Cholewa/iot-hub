import { useRef } from "react";
import { Box, Typography } from "@mui/material";

export const Console = () => {
    const scrollableBoxRef = useRef(null);
    return (
        <Box
            sx={{
                bgcolor: 'black',
                border: '1px solid rgba(255,255,255,0.7)',
                borderRadius: 2,
                overflowY: "scroll",
                maxHeight: "15vh",
                height: "15vh",
                flexDirection: "column-reverse"
            }}
            p={2}
            ref={scrollableBoxRef}
        >
            <Typography variant="body2">
                <span>14.01.2024 20:11:10: &nbsp;</span>
                <span>Received application message from 19a1805b-9d19-4f46-a34a-d055dde12560.</span>
            </Typography>
            <Typography variant="body2">
                <span>14.01.2024 20:12:10: &nbsp;</span>
                <span>Received application message from 19a1805b-9d19-4f46-a34a-d055dde12560.</span>
            </Typography>
        </Box>
    );
}