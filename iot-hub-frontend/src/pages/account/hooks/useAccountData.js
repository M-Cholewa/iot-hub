import axios from 'axios';
import { useState } from 'react';
import { serverAddress } from '../../../core/config/server';

export const useAccountData = (userId) => {

    const [loading, setLoading] = useState(false);

    const updateEmail = async (newEmail) => {
        setLoading(true);
        const response = await axios.patch(`${serverAddress}/user/email`, {
            userId: userId,
            email: newEmail
        }).then((res) => {
            return res.data;
        }).catch((error) => {
            return { isSuccess: false, message: error.response.data };
        }).finally(() => {
            setLoading(false);
        });

        return response;
    };

    const updatePassword = async (oldPassword, newPassword) => {
        setLoading(true);
        const response = await axios.patch(`${serverAddress}/user/password`, {
            userId: userId,
            oldPassword: oldPassword,
            newPassword: newPassword,
        }).then((res) => {
            return res.data;
        }).catch((error) => {
            return { isSuccess: false, message: error.response.data };
        }).finally(() => {
            setLoading(false);
        });

        return response;
    };

    const deleteAccount = async () => {
        setLoading(true);
        const response = await axios.delete(`${serverAddress}/user`, {
            data: { userId: userId }
        }).then((res) => {
            return res.data;
        }).catch((error) => {
            return { isSuccess: false, message: error.response.data };
        }).finally(() => {
            setLoading(false);
        });

        return response;
    };


    return { loading, updateEmail, updatePassword, deleteAccount };
};