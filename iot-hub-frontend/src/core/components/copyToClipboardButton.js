import { useState } from "react";
import { IconButton, Snackbar } from "@mui/material";
import ContentCopyIcon from '@mui/icons-material/ContentCopy';

export const CopyToClipboardButton = () => {
    const [open, setOpen] = useState(false);

    const handleClick = () => {
        setOpen(true);
        navigator.clipboard.writeText(window.location.toString());
    };

    return (
        <>
            <IconButton onClick={handleClick} color="primary">
                <ContentCopyIcon />
            </IconButton>
            <Snackbar
                message="Copied to clibboard"
                anchorOrigin={{ vertical: "bottom", horizontal: "right" }}
                autoHideDuration={2000}
                onClose={() => setOpen(false)}
                open={open}
            />
        </>
    );
};