import {$auth, $host} from './index'
import jwt_decode from 'jwt-decode'
import React, { useContext } from 'react'
import { Context } from '../index'


export const login = async (email, password) => {
    return await $host.put('/api/login', {Email: email, Password: password})
    .then((res) => {
        localStorage.setItem('token', res.data)
        return jwt_decode(res.data)
    }).catch((error) => handle(error))
}

export const register = async (email, password, name) => {
    return await $host.post('/api/login', {Name: name, Email: email, Password: password})
    .then((res) => {
        localStorage.setItem('token', res.data)
        return jwt_decode(res.data)
    }).catch((error) => handle(error))
}

export const check = async () => {
    return await $auth.get('/api/login/auth').then((res) => {
        localStorage.setItem('token', res.data)
        return jwt_decode(res.data)
    }).catch((error) => handle(error))
}

export const remove = async (list) => {
    await $auth.delete('/api/user', {headers: {'Content-Type': 'application/json'}, data: {IdList: [...list]}})
        .catch((error) => handle(error))
}

export const block = async (list) => {
    await $auth.put('/api/user', {IdList: [...list], IsBlocking: true})
        .catch((error) => handle(error))
}

export const unblock = async (list) => {
    await $auth.put('/api/user', {IdList: [...list], IsBlocking: false})
        .catch((error) => handle(error))
}

export const fetchUsers = async () => {
    const {data} = await $auth.get('/api/user')
        .catch((error) => handle(error))
    return data;
}

const handle = (error) => {
    if (error.response) {
        if (error.response.status == 401) {
            localStorage.removeItem('token')
            document.location.reload();
        } else {
            alert(error.response.status + " : " + error.response.data.error)
        }
    } else {
      console.log('Error', error.message);
    }
}
