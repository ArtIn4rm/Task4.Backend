import {makeAutoObservable} from 'mobx'

export default class UserState{
    constructor(){
        this._isUser = false
        this._user = {}
        this._userList = [
            {
                "id": "b464a743-93fa-4cba-bac9-219f44d620af",
                "name": "Artem",
                "email": "kuf227@gmail.com",
                "registrationDate": "2022-11-30T12:04:16.2893772",
                "lastAuthorizationDate": "2022-11-30T12:04:16.2894319",
                "status": 0
            },
            {
                "id": "da2b58f2-f168-427a-8f78-425b7dcefad6",
                "name": "Maxim",
                "email": "kuf228@gmail.com",
                "registrationDate": "2022-11-30T12:04:26.3488197",
                "lastAuthorizationDate": "2022-12-01T15:30:50.3856714",
                "status": 0
            },
            {
                "id": "b2854c6d-ff11-4dc4-bbe2-fef201acd33b",
                "name": "Nick",
                "email": "kuf226@gmail.com",
                "registrationDate": "2022-11-30T12:25:46.9548446",
                "lastAuthorizationDate": "2022-11-30T12:25:46.954899",
                "status": 0
            }
        ]
        makeAutoObservable(this)
    }

    setIsAuth(bool){
        this._isUser = bool;
    }

    setUserList(userList){
        this._userList = userList;
    }

    setUser(user){
        this._user = user;
    }

    get isAuth(){
        return this._isUser
    }

    get user(){
        return this._user
    }

    get userList(){
        return this._userList;
    }

}