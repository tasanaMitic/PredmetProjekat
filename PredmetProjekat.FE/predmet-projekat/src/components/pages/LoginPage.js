import { useState } from "react";
import { useHistory } from "react-router-dom";
import { Container, Form, Button } from "react-bootstrap";
import ModalError from "../modals/ModalError";
import { login } from '../../api/methods'
import PropTypes from 'prop-types';

const LoginPage = ({loginUser}) => {
    const history = useHistory();

    const [error, setError] = useState(null);
    const [show, setShow] = useState(false);

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const handleSubmit = (e) => {
        e.preventDefault();
        setError(null);

        const payload = { email, password };
        login(payload).then(res => {
            if (res.status !== 202) {   
                throw Error('There was an error with the request!'); 
            }
            return res.data;
        })
        .then(data => {
            loginUser(data.token);
            history.replace('/');  
            setError(null);
        })
        .catch(err => {
            setError(err);  //todo ako je nedostupan server, ne postoji response polje. treba to primeniti svuda
            setShow(true);
        })
    }

    return (
        <Container>
            <h1>Login Page</h1>         
            {error && <ModalError setShow={setShow} show={show} error={error} setError={setError}/>}
            <Form onSubmit={handleSubmit}>
                <Form.Group className="mb-3" controlId="formBasicEmail">
                    <Form.Label>Email address</Form.Label>
                    <Form.Control type="email" placeholder="Enter email" value={email} onChange={(e) => setEmail(e.target.value)} required/>
                    <Form.Text className="text-muted">
                        We'll never share your email with anyone else.
                    </Form.Text>
                </Form.Group>
                <Form.Group className="mb-3" controlId="formBasicPassword">
                    <Form.Label>Password</Form.Label>
                    <Form.Control type="password" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)} required/>
                </Form.Group>
                <Button variant="outline-dark" type="submit">
                    Submit
                </Button>
            </Form>
        </Container>
    );
}

LoginPage.propTypes = {
    loginUser: PropTypes.func,
}

export default LoginPage;