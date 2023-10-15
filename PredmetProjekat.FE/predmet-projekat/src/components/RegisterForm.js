import { useState } from "react";
import { Form, Button } from "react-bootstrap";
import AlertDissmisable from './Alert';
import ModalSuccess from './ModalSuccess';
import { register } from '../api/methods';
import PropTypes from 'prop-types';
import Cookies from 'universal-cookie';

const RegisterForm = ({userType}) => {
    const [isPending, setIsPending] = useState(false);
    const [error, setError] = useState(null);
    const [show, setShow] = useState(false);
    const [successMessage, setSuccessMessage] = useState(false);

    const [email, setEmail] = useState('');
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');
    
    const clearData = () => {
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
        register(payload, userType).then(res => {
            if (res.status !== 202) {       
                throw Error('There was an error with the request!'); 
            }
            setShow(true);
            setSuccessMessage("You have successfully creates a user with username " + username);
            setIsPending(false);
            setError(null);            
            return res.data;
        })
        .catch(err => {
            setIsPending(false);
            console.log(err);
            setError(err);
        })
    }

    return (
        <Form onSubmit={handleSubmit}>
        <ModalSuccess setShow={setShow} show={show} clearData={clearData} message={successMessage}/>
        {error && <AlertDissmisable error={error} setError={setError}/>}
            <Form.Group className="mb-3" controlId={"formBasicEmail" + userType}>
                <Form.Label>Email address</Form.Label>
                <Form.Control type="email" placeholder="Enter email" value={email} onChange={(e) => setEmail(e.target.value)} required/>
                <Form.Text className="text-muted">
                    We'll never share your email with anyone else.
                </Form.Text>
            </Form.Group>
            <Form.Group className="mb-3" controlId={"formBasicUsername" + userType}>
                <Form.Label>Username</Form.Label>
                <Form.Control type="text" placeholder="Username" value={username} onChange={(e) => setUsername(e.target.value)} required /> 
            </Form.Group>
            <Form.Group className="mb-3" controlId={"formBasicPassword" + userType}>
                    <Form.Label>Password</Form.Label>
                    <Form.Control type="password" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)} required/>
            </Form.Group>
            <Form.Group className="mb-3" controlId={"formBasicFirstName" + userType}>
                <Form.Label>First name</Form.Label>
                <Form.Control type="text" placeholder="FirstName" value={firstName} onChange={(e) => setFirstName(e.target.value)} required />
            </Form.Group>
            <Form.Group className="mb-3" controlId={"formBasicLastName" + userType}>
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
    userType: PropTypes.string
}

export default RegisterForm;