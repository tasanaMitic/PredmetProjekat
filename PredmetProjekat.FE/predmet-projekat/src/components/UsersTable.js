import { useEffect, useState } from "react";
import { Button, Container, Table } from "react-bootstrap";
import { assignManager, deleteEmployee, getEmployees } from "../api/methods";
import ModalError from "./modals/ModalError";
import ModalCheck from './modals/ModalCheck'
import ModalManager from "./modals/ModalManager";
import ModalSuccess from "./modals/ModalSuccess";
import PropTypes from 'prop-types';

const UsersTable = ({ admins, users, loggedInUser }) => {
    const [data, setData] = useState(null);
    const [error, setError] = useState(null);
    const [checkModal, setCheckModal] = useState(false);
    const [managerModal, setManagerModal] = useState(false);
    const [successModal, setSuccessModal] = useState(false);    
    const [errorModal, setErrorModal] = useState(false);
    const [successMessage, setSuccessMessage] = useState(null);
    const [managers, setManagers] = useState(null);
    const [userToDelete, setUserToDelete] = useState(null);
    const [userToAssignManager, setUserToAssignManager] = useState(null);
    

    useEffect(() => {
        if (admins) {
            setData(admins);
        }
        else if (users) {
            setData(users);
        }
    }, [admins, users]);

    const handleDelete = (username) => {
        setCheckModal(true);
        setUserToDelete(username);
    }

    const handleAssign = (username) => {
        setUserToAssignManager(username);

        getEmployees().then(res => {
            if (res.status !== 200) {
                throw Error('There was an error with the request!');
            }
            return res.data;
        })
            .then(data => {
                setManagers(data.filter(user => user.username !== username));
                setError(null);
                setManagerModal(true);       
            })
            .catch(err => {
                setError(err);
            })
    }

    const confirmDelete = () => {
        deleteEmployee(userToDelete).then(res => {
            if (res.status !== 200) {
                throw Error('There was an error with the request!');

            }
            return res.data;
        })
            .then(data => setData(data))
            .catch(err => {
                setError(err.response);
                setErrorModal(true);
            })
    }

    const confirmAssign = (selectedManager) => {
        const payload = { managerUsername: selectedManager, employeeUsername: userToAssignManager };
        assignManager(payload).then(res => {
            if (res.status !== 200) {
                throw Error('There was an error with the request!');
            }
            return res.data;
        })
            .then(data => {
                setData(data);
                setSuccessModal(true);
                setSuccessMessage('You have successfully assigned ' + selectedManager + ' as a manager to ' + userToAssignManager);
            })
            .catch(err => {
                setError(err.response);
                setErrorModal(true);
            })
    }

    const highlightColor = (username) => {
        if (username === loggedInUser.username) {
            return "#FFE4E1";
        }
    }

    const clearData = () => {
        setSuccessModal(false);
        setUserToAssignManager(null);
    }

    return (
        <Container>
            {error && <ModalError setShow={setErrorModal} show={errorModal} error={error} setError={setError}/>}
            {data && <Table striped hover>
                <ModalCheck setShow={setCheckModal} show={checkModal} confirm={confirmDelete} />
                <ModalManager setShow={setManagerModal} show={managerModal} managers={managers} confirm={confirmAssign} loggedInUser={loggedInUser}/>
                <ModalSuccess setShow={setSuccessModal} show={successModal} clearData={clearData} message={successMessage} />
                <thead>
                    <tr>
                        <th>First name</th>
                        <th>Last name</th>
                        <th>Username</th>
                        <th>Email</th>
                        {users && <th>Manager</th>}
                        {users && <th></th>}
                        {users && <th></th>}
                    </tr>
                </thead>
                <tbody>
                    {data.map((user) => (
                        <tr key={user.username}>
                            <td style={{ backgroundColor: highlightColor(user.username) }}>{user.firstName}</td>
                            <td style={{ backgroundColor: highlightColor(user.username) }}>{user.lastName}</td>
                            <td style={{ backgroundColor: highlightColor(user.username) }}>{user.username}</td>
                            <td style={{ backgroundColor: highlightColor(user.username) }}>{user.email}</td>
                            {users &&
                                <td style={{ backgroundColor: highlightColor(user.username) }}>{user.manager ? user.manager.username : "None"}</td>
                            }
                            {users && (!user.manager ?
                                <td style={{ backgroundColor: highlightColor(user.username) }}>
                                    <Button variant="dark" onClick={() => handleAssign(user.username)}>Assign manager</Button>
                                </td>
                                :
                                <td></td>)
                            }
                            {users &&
                                <td style={{ backgroundColor: highlightColor(user.username) }}>
                                    <Button variant="dark" onClick={() => handleDelete(user.username)}>Delete</Button>
                                </td>
                            }
                        </tr>
                    ))}
                </tbody>
            </Table>
            }
        </Container>
    );
}

UsersTable.propTypes = {
    admins: PropTypes.arrayOf(PropTypes.shape({
        email: PropTypes.string,
        firstName: PropTypes.string,
        lastName: PropTypes.string,
        username: PropTypes.string
    })),
    users: PropTypes.arrayOf(PropTypes.shape({
        email: PropTypes.string,
        firstName: PropTypes.string,
        lastName: PropTypes.string,
        username: PropTypes.string,
        managerId: PropTypes.string,
        manager: PropTypes.shape({
            email: PropTypes.string,
            firstName: PropTypes.string,
            lastName: PropTypes.string,
            username: PropTypes.string,
        })
    })),
    loggedInUser: PropTypes.shape({
        role: PropTypes.string,
        username: PropTypes.string
    })
}

export default UsersTable;