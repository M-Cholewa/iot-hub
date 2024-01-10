import { Outlet, Navigate } from 'react-router-dom'
import { useUserAuth } from '../hooks/useUserAuth'

export const ProtectedRoutes = ({ expectedRoles }) => {

    const { getUser } = useUserAuth();

    const user = getUser();

    if (user == null) {
        return (
            <Navigate to="/login" />
        )
    }

    let userRoles = user.roles;

    if (userRoles == null) {
        return (
            <Navigate to="/login" />
        )
    }

    let auth = false;

    expectedRoles.some(role => {
        var found = userRoles.find((userRole) => userRole.key === role);
        if (!found) {
            auth = false;
            return true;
        }

        auth = true;
        return false;
    });

    return (
        auth ? <Outlet /> : <Navigate to="/login" />
    )
}

export default ProtectedRoutes