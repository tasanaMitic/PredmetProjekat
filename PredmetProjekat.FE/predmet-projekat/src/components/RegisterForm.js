import { useState } from "react";
import { Form, Button } from "react-bootstrap";
import ModalError from "./modals/ModalError";
import ModalSuccess from './modals/ModalSuccess';
import { register } from '../api/methods';
import PropTypes from 'prop-types';

const RegisterForm = ({ userRole }) => {
    const [isPending, setIsPending] = useState(false);
    const [error, setError] = useState(null);
    const [successModal, setSuccessModal] = useState(false);
    const [errorModal, setErrorModal] = useState(false);
    const [successMessage, setSuccessMessage] = useState(null);

    const [data, setData] = useState({email: '', username: '', firstName: '', lastName: '', password: '' });

    const clearData = () => { 
        setData({email: '', username: '', firstName: '', lastName: '', password: '' });
    }

    const handleSubmit = (e) => {   //todo username limited to 4-12 char
        e.preventDefault();
        setIsPending(true);
        setError(null);

        const payload = { email: data.email, username: data.username, firstName: data.firstName, lastName: data.lastName, password: data.password };
        register(userRole, payload).then(res => {
            if (res.status !== 202) {
                throw Error('There was an error with the request!');
            }
            setSuccessModal(true);
            setSuccessMessage("You have successfully creates a user with username " + data.username);
            setIsPending(false);
            setError(null);
            return res.data;
        })
            .catch(err => {
                setIsPending(false);
                setError(err.response);
                setErrorModal(true);
            })
    }

    return (
        <Form onSubmit={handleSubmit}>
            <ModalSuccess setShow={setSuccessModal} show={successModal} clearData={clearData} message={successMessage} />
            {error && <ModalError setShow={setErrorModal} show={errorModal} error={error} setError={setError} />}
            <Form.Group className="mb-3" controlId={"formBasicEmail" + userRole}>
                <Form.Label>Email address</Form.Label>
                <Form.Control type="email" placeholder="Enter email" value={data.email} onChange={(e) => setData({ ...data, email: e.target.value })} required />
                <Form.Text className="text-muted">
                    We'll never share your email with anyone else.
                </Form.Text>
            </Form.Group>
            <Form.Group className="mb-3" controlId={"formBasicUsername" + userRole}>
                <Form.Label>Username</Form.Label>
                <Form.Control type="text" placeholder="Username" value={data.username} onChange={(e) => setData({ ...data, username: e.target.value })} required />
            </Form.Group>
            <Form.Group className="mb-3" controlId={"formBasicPassword" + userRole}>
                <Form.Label>Password</Form.Label>
                <Form.Control type="password" placeholder="Password" value={data.password} onChange={(e) => setData({ ...data, password: e.target.value })} required />
            </Form.Group>
            <Form.Group className="mb-3" controlId={"formBasicFirstName" + userRole}>
                <Form.Label>First name</Form.Label>
                <Form.Control type="text" placeholder="FirstName" value={data.firstName} onChange={(e) => setData({ ...data, firstName: e.target.value })} required />
            </Form.Group>
            <Form.Group className="mb-3" controlId={"formBasicLastName" + userRole}>
                <Form.Label>Last name</Form.Label>
                <Form.Control type="text" placeholder="LastName" value={data.lastName} onChange={(e) => setData({ ...data, lastName: e.target.value })} required />
            </Form.Group>
            <Button variant="dark" type="submit" disabled={isPending}>
                Submit
            </Button>
        </Form>
    );
}

RegisterForm.propTypes = {
    userRole: PropTypes.string
}

export default RegisterForm;