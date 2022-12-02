import React, {useContext, useState} from 'react'
import {Context} from '../index'
import {Form, Row, NavLink, Button, Container, Card} from 'react-bootstrap'
import {useLocation, useHistory} from 'react-router-dom'
import {observer} from 'mobx-react-lite'
import {login, register} from '../api/userApi'
import {validate} from '../utils/validateParams'

const Login = observer(() => {
    const {user} = useContext(Context)
    const isLogin = useLocation().pathname === '/api/login'
    const isReg = useLocation().pathname === '/api/login/register'
    let history = useHistory()
    const [email, setEmail] = useState('')
    const [password, setPassword] = useState('')
    const [name, setName] = useState('')

    if(!isLogin && !isReg){
        history.push('/api/login')
    }

    const sign = async () => {
        try{
            validate(isLogin, {email: email, password: password, name: (isLogin) ? "" : name})
            let data
            if(isLogin){
                data = await login(email, password)
            } else {
                data = await register(email, password, name)
            }
            if(data !== undefined){
                user.setUser(data)
                user.setIsAuth(true)
                history.push('/api/user')
            }
            document.location.reload();
        } catch(e){
            alert(e)
        }
    }

    return (
        <Container
        className='d-flex justify-content-center align-items-center' style={{height: window.innerHeight-54}}>
            <Card className='p-5 mt-auto mb-auto' style={{width: 400, backgroundColor: "#f8f9fb"}}>
                <h2 style={{color: "#4d4d4d"}} className="m-auto">{isLogin ? 'Authentication' : isReg&&'Registration'}</h2>
                <Form className="d-flex flex-column">
                    <Form.Control
                    className="mt-3"
                    placeholder="Enter email"
                    value={email}
                    variant={"outline-dark"}
                    onChange={(e) => setEmail(e.target.value)}
                    />
                </Form>
                <Form className="d-flex flex-column">
                    <Form.Control
                    type="password"
                    className="mt-3"
                    placeholder="Enter password"
                    value={password}
                    onChange={(e) => setPassword(e.target.value)}
                    />
                </Form>
                {isReg&&!isLogin &&
                <Form className="d-flex flex-column">
                    <Form.Control
                    className="mt-3"
                    placeholder="Enter name"
                    value={name}
                    onChange={(e) => setName(e.target.value)}
                    />
                </Form>
                }
                <Row className="d-flex justify-content-between pr-5 pl-5">
                    {!isLogin ? 
                        <NavLink className="ml-auto pt-4" style={{color: "#ff834f"}} href={'/api/login'}>
                            Enter
                        </NavLink>
                        :
                        <NavLink className="ml-auto pt-4" style={{color: "#ff834f"}} href={'/api/login/register'}>
                            Register
                        </NavLink>
                    }
                    <Button 
                    className="mt-3 ml-auto mr-auto pr-4 pl-4" 
                    variant={"outline-dark"}
                    onClick={() => sign()}
                    >
                        {isLogin ? 'Enter' : 'Register'}</Button>
                </Row>
            </Card>
        </Container>
    );
})
export default Login;