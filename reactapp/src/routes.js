import {USER_ROUTE, LOGIN_ROUTE} from '../src/utils/consts'
import Login from '../src/pages/Login'
import Users from '../src/pages/Users'

export const publicRoutes = [
    {
        Path: LOGIN_ROUTE,
        Component: Login
    }
]

export const userRoutes = [
    {
        Path: USER_ROUTE,
        Component: Users
    }
]