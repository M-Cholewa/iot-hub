import axios from 'axios';
import { useEffect, useState } from 'react';
import { serverAddress } from '../../../core/config/server';
import dayjs from 'dayjs';
import { textToRgb } from '../../../core/utility/color';

export const useChartsData = (deviceId) => {
    const [fieldNames, setFieldNames] = useState([]);
    const [selectedFieldNames, setSelectedFieldNames] = useState([]);
    const [datasets, setDatasets] = useState({ datasets: [] });
    const [loading, setLoading] = useState(false);
    const [dateSince, setDateSince] = useState(dayjs().subtract(3, 'hours'));
    const [dateTo, setDateTo] = useState(dayjs());

    useEffect(() => {
        const refreshFieldNames = () => {
            axios.get(`${serverAddress}/Telemetry/FieldNames`, { params: { deviceId: deviceId } })
                .then((res) => {
                    res.data
                        ? setFieldNames(res.data)
                        : setFieldNames([]);
                })
                .catch((err) => {
                    setFieldNames([]);
                });
        };

        refreshFieldNames();
    }, [deviceId]);

    useEffect(() => {
        if (fieldNames.length > 0) {
            setSelectedFieldNames([fieldNames[0]]);
            refreshDatasets(dateSince, dateTo);
        }
    }, [fieldNames]);

    const getDataset = (fieldName) => {
        return axios.get(`${serverAddress}/Telemetry/All`, { params: { deviceId: deviceId, fieldName: fieldName, sinceUTC: dateSince, toUTC: dateTo } })
    };

    const refreshDatasets = () => {
        setLoading(true);
        Promise.all(selectedFieldNames.map(fieldName => getDataset(fieldName)))
            .then((promisesRes) => {

                setDatasets({
                    datasets: promisesRes.map((promiseRes, i) => {
                        return {
                            label: selectedFieldNames[i],
                            data: promiseRes.data.map(d => { return { x: d.dateUTC, y: d.fieldValue } }),
                            borderColor: textToRgb(selectedFieldNames[i]),
                            backgroundColor: textToRgb(selectedFieldNames[i]),
                        }
                    })
                });
            })
            .catch((err) => {
                setDatasets([]);
            })
            .finally(() => {
                setLoading(false);
            });
    };

    return { fieldNames, selectedFieldNames, setSelectedFieldNames, datasets, loading, refreshDatasets, dateSince, setDateSince, dateTo, setDateTo };

};