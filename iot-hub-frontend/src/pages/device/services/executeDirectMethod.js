import axios from 'axios';
import { serverAddress } from '../../../core/config/server';

export const executeDirectMethod = async (deviceId, methodName, payload) => {

    return await axios.post(`${serverAddress}/Device/ExecuteDirectMethod`, {
        deviceId: deviceId,
        methodName: methodName,
        payload: payload
    })

};