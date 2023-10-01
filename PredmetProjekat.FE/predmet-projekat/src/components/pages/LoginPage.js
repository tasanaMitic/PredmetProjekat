import { useState } from "react";
import { useHistory } from "react-router-dom";
import { Container, Form, Button, Spinner } from "react-bootstrap";
import AlertDissmisable from "../Alert";
import { login } from '../../api/methods'

function LoginPage({loginUser}) {
    const history = useHistory();

    const [isPending, setIsPending] = useState(false);
    const [error, setError] = useState(null);

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');

    const handleSubmit = (e) => {
        e.preventDefault();
        setIsPending(true);
        setError(null);

        const payload = { email, password };
        login(payload).then(res => {
            if (res.status != 202) {       
                throw Error('There was an error with the request!'); 
            }
            return res.data;
        })
        .then(data => {
            loginUser(data.token);
            history.replace('/');  
            setIsPending(false);
            setError(null);
        })
        .catch(err => {
            console.log("error")
            setIsPending(false);
            setError(err);
        })
    }

    return (
        <Container>
            <h1>Login Page</h1>
            {error && <AlertDissmisable error={error} setError={setError} />}
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
                <Button variant="outline-dark" type="submit" disabled={isPending} >
                    Submit
                </Button>
            </Form>
        </Container>
    );
}

export default LoginPage;