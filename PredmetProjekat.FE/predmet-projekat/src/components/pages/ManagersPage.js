import { useEffect } from "react";
import { useState } from "react";
import { Container, Table } from "react-bootstrap";
import { getUser } from '../../api/methods';
import ModalError from "../modals/ModalError";
import PropTypes from 'prop-types';

const ManagersPage = ({ user }) => {
    const [manager, setManager] = useState(null);
    const [managing, setManaging] = useState(null);
    const [error, setError] = useState(null);
    const [errorModal, setErrorModal] = useState(false);

    useEffect(() => {
        getUser(user.role, user.username).then(res => {
            if (res.status !== 200) {
                throw Error('There was an error with the request!');
            }
            return res.data;
        })
            .then(data => {
                setManager(data.manager);
                setManaging(data.manages);
            })
            .catch(err => {
                setError(err.response);
                setErrorModal(true);
            })

    }, [user.username, user.role]);

    return (
        <Container>
        {error && <ModalError setShow={setErrorModal} show={errorModal} error={error} setError={setError}/>}
            <h1>Managers</h1>
            <h2>Your manager</h2>
            {manager !== null ? <Table striped hover>
                <thead>
                    <tr>
                        <th>Name</th>
                        <th>Username</th>
                        <th>Email</th>
                    </tr>
                </thead>
                <tbody>
                    <tr key={manager.username} >
                        <td>{manager.firstName} {manager.lastName}</td>
                        <td>{manager.username}</td>
                        <td>{manager.email}</td>
                    </tr>
                </tbody>
            </Table>
                : <p>You currently don't have a manager.</p>}
            <h2>You are managing</h2>
            {managing !== null && managing.length > 0 ?
                <Table striped hover>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Username</th>
                            <th>Email</th>
                        </tr>
                    </thead>
                    <tbody>
                        {managing.map((user) => (
                            <tr key={user.username} >
                                <td>{user.firstName} {user.lastName}</td>
                                <td>{user.username}</td>
                                <td>{user.email}</td>
                            </tr>
                        ))}
                    </tbody>
                </Table>
                : <p>You are currently not a manager to anyone.</p>}
        </Container>
    );
}

ManagersPage.propTypes = {
    user: PropTypes.shape({
        username: PropTypes.string,
        role: PropTypes.string
    })
}

export default ManagersPage;