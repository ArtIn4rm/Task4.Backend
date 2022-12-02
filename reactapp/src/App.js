import React, {useContext, useEffect, useState} from 'react';
import {BrowserRouter} from 'react-router-dom';
import {Spinner} from 'react-bootstrap'
import AppRouter from './components/AppRouter'
import {Context} from './index'
import NavBar from '../src/components/NavBar'
import {observer} from 'mobx-react-lite'
import {check} from './api/userApi';

const App = observer(() => {
  const {user} = useContext(Context)
  const [loading, setLoading] = useState(true)

  useEffect(() => {
    if(localStorage.getItem('token') !== null){
      check().then(data => {
        user.setUser(data)
        user.setIsAuth(true)
      }).finally(() => setLoading(false))
    } else {
      setLoading(false)
    }
  }, [])

  if(loading){
    return <Spinner className="d-flex justify-content-center align-items-center" animation="border" />
  }

  return (
    <BrowserRouter>
      <NavBar/>
      <AppRouter/>
    </BrowserRouter>
  );
})

export default App;
