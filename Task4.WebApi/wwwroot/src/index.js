import React, {createContext} from 'react'
import ReactDOM from 'react-dom'
import App from './App'
import UserState from './service/UserState'
//import 'materialize-css'
import './sass/styles.scss'

export const Context = createContext(null)

ReactDOM.render(
  <Context.Provider value={{
    user: new UserState()
  }}>
    <App />
  </Context.Provider>,
  document.getElementById('root')
);