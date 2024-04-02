import { Alert, Stack, Typography, CircularProgress } from '@mui/material';
import { useLastLogsData } from './hooks/useLastLogsData';

function compare(a, b) {

    let dateA = new Date(a.log.dateUTC);
    let dateB = new Date(b.log.dateUTC);

    if (dateA < dateB) {
        return 1;
    }
    if (dateA > dateB) {
        return -1;
    }
    return 0;

}

export const LastNotifications = () => {

    const { lastLogs, loading } = useLastLogsData();


    if (loading) {
        return <CircularProgress />
    }

    return (<Stack spacing={1}>
        {
            lastLogs.sort(compare).map((lastLog, index) => {
                let severity = lastLog.log.severity.toLowerCase();
                let date = new Date(lastLog.log.dateUTC).toLocaleString();
                return (
                    <Alert key={index} variant="outlined" severity={severity}
                        action={
                            <Typography variant='span'>{date}</Typography>
                        }>
                        {lastLog.deviceName} : {lastLog.log.message}
                    </Alert>
                );
            })
        }
    </Stack>
    );
};