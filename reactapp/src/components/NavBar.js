import React, {useContext} from 'react';
import {Context} from '../index'
import {Navbar, Nav, Button, Container, Image} from 'react-bootstrap'
import {observer} from 'mobx-react-lite'
import logo from '../assets/logo.png'
import {useHistory} from 'react-router-dom'

const NavBar = observer(() => {
    const {user} = useContext(Context)
    let history = useHistory()

    const logout = () => {
        user.setUser({})
        localStorage.removeItem('token')
        history.push('/api/login')
        document.location.reload();
    }

    return (
        <Navbar bg="light" variant="light">
            <Container>
            <Image height={30} src={logo} />
            <Nav className="me-auto">
                    {user.isAuth &&
                        <Button variant={"outline-dark"} style={{paddingTop: "2px", paddingBottom: "2px"}}
                        onClick={logout}>Log out</Button>
                    }
                </Nav>
            </Container>
        </Navbar>
    );
});

export default NavBar;