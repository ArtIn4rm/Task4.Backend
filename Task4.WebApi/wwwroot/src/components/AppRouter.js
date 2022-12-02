import React, {useContext} from 'react';
import {Switch, Route, Redirect} from 'react-router-dom';
import {publicRoutes, userRoutes} from '../routes';
import {Context} from '../index'
import {USER_ROUTE, LOGIN_ROUTE} from "../utils/consts"

const AppRouter = () => {
    const {user} = useContext(Context)
    return (
        <Switch>
            {user.isAuth && userRoutes.map(({path, Component}) =>
                <Route key={path} path={path} component={Component} exact/>
            )}
            {publicRoutes.map(({path, Component}) =>
                <Route key={path} path={path} component={Component} exact/>
            )}
            <Redirect to={(user.isAuth) ? USER_ROUTE : LOGIN_ROUTE}/>
        </Switch>
    );
};

export default AppRouter;