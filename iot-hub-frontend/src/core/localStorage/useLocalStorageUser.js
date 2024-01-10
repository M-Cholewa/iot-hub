export const useLocalStorageUser = () => {
    const saveUserToLocalStorage = (user) => {
        localStorage.setItem('user', JSON.stringify(user));
    };

    const removeUserFromStorage = () => {
        localStorage.removeItem('user');
    };

    const getUserObject = () => {
        const user = localStorage.getItem('user');
        if (user) {
            return JSON.parse(user);
        }
    }

    return { getUserObject, saveUserToLocalStorage, removeUserFromStorage };
};