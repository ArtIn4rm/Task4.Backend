import {makeAutoObservable} from 'mobx'

export default class UserState{
    constructor(){
        this._isUser = false
        this._user = {}
        this._userList = [
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