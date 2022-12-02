import React, {useContext, useState, useEffect} from 'react'
import {Context} from '../index'
import {ButtonGroup, Row, CheckBox, Button, Container, Card, Table} from 'react-bootstrap'
import Form from 'react-bootstrap/Form'
import {useLocation, useHistory} from 'react-router-dom'
import {observer} from 'mobx-react-lite'
import personLock from '../assets/svg/person-lock.svg'
import trash from '../assets/svg/trash.svg'
import unlock from '../assets/svg/unlock.svg'
import {parseDate} from '../utils/dataFormat'
import {remove, block, unblock, fetchUsers} from '../api/userApi'

const Users = observer(() =>{
    const {user} = useContext(Context)

    useEffect(()=>{
        fetchUsers().then((data) => {
            user.setUserList(data.registeredUsers)
        })
    }, [])

    const pickAll = (checked) => {
        user.userList.map((value, index) => {
            document.getElementById(index+1).checked = checked
        })
    }

    const getIdList = () => {
        let list = []
        user.userList.map((value, index) => {
            if(document.getElementById(index+1).checked){
                list.push(value.id)
            }
        })
        return list
    }

    const blockChecked = () => {
        block(getIdList())
        pickAll(false)
        document.location.reload();
    }

    const unblockChecked = () => {
        unblock(getIdList())
        pickAll(false)
        document.location.reload();
    }

    const deleteChecked = () => {
        remove(getIdList())
        pickAll(false)
        document.location.reload();
    }

    return (
        <Container
        className="d-flex justify-content-center con" style={{height: window.innerHeight-54}}>
            <Card className="d-flex p-5 crd" style={{backgroundColor: "#f8f9fb"}}>
                <ButtonGroup style={{width: "400px"}} className="mr-auto ml-auto mb-3">
                    <Button variant="outline-dark" className="p-0">
                        <div onClick={(e)=>blockChecked()} className="svg_div"><img src={personLock} className="svg"/></div>
                    </Button>
                    <Button variant="outline-dark" className="p-0">
                        <div  onClick={(e)=>unblockChecked()} className="svg_div"><img src={unlock} className="svg"/></div>
                    </Button>
                    <Button variant="outline-dark" className="p-0">
                        <div  onClick={(e)=>deleteChecked()} className="svg_div"><img src={trash} className="svg"/></div>
                    </Button>
                </ButtonGroup>
                <Table striped bordered hover
                    className="ml-auto mr-aut align-items-center">
                    <thead>
                        <tr>
                            <th className="align_th">
                                <Row className="ml-1 mr-1">Pick All<Form.Check onClick={(e)=>pickAll(e.target.checked)} className="pick_all"/></Row>
                            </th>
                            <th className="align_th id_th">Id</th>
                            <th className="align_th">Email</th>
                            <th className="align_th">Name</th>
                            <th className="align_th">Registration Date</th>
                            <th className="align_th">Last Login Date</th>
                            <th className="align_th">Status</th>
                        </tr>
                    </thead>
                    <tbody>
                         {user.userList.map((value, index) => 
                            <tr>
                                <td className="align_th"><Form.Check id={index+1} className="check_th"/></td>
                                <td className="align_th">{value.id}</td>
                                <td className="align_th">{value.email}</td>
                                <td className="align_th">{value.name}</td>
                                <td className="align_th">{parseDate(value.registrationDate)}</td>
                                <td className="align_th">{parseDate(value.lastAuthorizationDate)}</td>
                                <td className="align_th">{(value.status == 0) ? "Active" : "Blocked"}</td>
                            </tr>
                        )}
                    </tbody>
                </Table>
            </Card>
        </Container>
    )
})

export default Users;