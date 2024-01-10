import { useLocalStorageUser } from '../../core/localStorage/useLocalStorageUser';
import axios from 'axios';

export const useUserAuth = () => {
    const { getUserObject, saveUserToLocalStorage, removeUserFromStorage } = useLocalStorageUser();

    const login = (_user) => {
        saveUserToLocalStorage(_user);
        axios.defaults.headers.common['Authorization'] = `Bearer ${_user.token}`;
    }

    const logout = () => {
        removeUserFromStorage();
        delete axios.defaults.headers.common['Authorization'];
    }

    const getUser = () => {
        const userObject = getUserObject();

        if (userObject) {
            return userObject.user;
        }
    }

    const getUserToken = () => {
        const userObject = getUserObject();

        if (userObject) {
            return userObject.token;
        }
    }

    const getUserRoles = () => {
        const user = getUser();

        if (user) {
            return user.roles;
        }

        return [];
    }

    return { getUser, getUserRoles, getUserToken, login, logout };
}