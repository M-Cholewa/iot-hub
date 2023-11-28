import { createTheme } from '@mui/material/styles';
import { blue } from '@mui/material/colors';

export const darkTheme = createTheme({
    palette: {
        mode: 'dark',
        primary: blue,
        background: {
            default: '#101418',
            paper: '#11151a',
        },
    },
});