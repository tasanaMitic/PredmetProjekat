import { useState } from "react";
import { Form, Button } from "react-bootstrap";
import ModalError from "./modals/ModalError";
import ModalSuccess from './modals/ModalSuccess';
import { register } from '../api/methods';
import PropTypes from 'prop-types';

const RegisterForm = ({userRole}) => {
    const [isPending, setIsPending] = useState(false);
    const [error, setError] = useState(null);
    const [successModal, setSuccessModal] = useState(false);
    const [errorModal, setErrorModal] = useState(false);
    const [successMessage, setSuccessMessage] = useState(null);

    const [email, setEmail] = useState('');
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    
    const clearData = () => {   //doto kao u account page
        setEmail('');
        setUsername('');
        setPassword('');
        setFirstName('');
        setLastName('');
    }

    const handleSubmit = (e) => {   //todo username limited to 4-12 char
        e.preventDefault(); 
        setIsPending(true);
        setError(null);

        const payload = { email, username, firstName, lastName, password };
        register(userRole, payload).then(res => {
            if (res.status !== 202) {       
                throw Error('There was an error with the request!'); 
            }
            setSuccessModal(true);
            setSuccessMessage("You have successfully creates a user with username " + username);
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
        <ModalSuccess setShow={setSuccessModal} show={successModal} clearData={clearData} message={successMessage}/>
        {error && <ModalError setShow={setErrorModal} show={errorModal} error={error} setError={setError}/>}
            <Form.Group className="mb-3" controlId={"formBasicEmail" + userRole}>
                <Form.Label>Email address</Form.Label>
                <Form.Control type="email" placeholder="Enter email" value={email} onChange={(e) => setEmail(e.target.value)} required/>
                <Form.Text className="text-muted">
                    We'll never share your email with anyone else.
                </Form.Text>
            </Form.Group>
            <Form.Group className="mb-3" controlId={"formBasicUsername" + userRole}>
                <Form.Label>Username</Form.Label>
                <Form.Control type="text" placeholder="Username" value={username} onChange={(e) => setUsername(e.target.value)} required /> 
            </Form.Group>
            <Form.Group className="mb-3" controlId={"formBasicPassword" + userRole}>
                    <Form.Label>Password</Form.Label>
                    <Form.Control type="password" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)} required/>
            </Form.Group>
            <Form.Group className="mb-3" controlId={"formBasicFirstName" + userRole}>
                <Form.Label>First name</Form.Label>
                <Form.Control type="text" placeholder="FirstName" value={firstName} onChange={(e) => setFirstName(e.target.value)} required />
            </Form.Group>
            <Form.Group className="mb-3" controlId={"formBasicLastName" + userRole}>
                <Form.Label>Last name</Form.Label>
                <Form.Control type="text" placeholder="LastName" value={lastName} onChange={(e) => setLastName(e.target.value)} required />
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