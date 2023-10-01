import { useState } from "react";
import { Container, Form, Button } from "react-bootstrap";
import AlertDissmisable from "../Alert";
import login from '../../api/methods'

function LoginPage() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    
    const [data, setData] = useState(null);
    const [isPending, setIsPending] = useState(true);
    const [error, setError] = useState(null);

    const handleSubmit = (e) => {
        e.preventDefault();
        setIsPending(true);
        const loginObj = { email, password };


        const data2 = login(loginObj);
        console.log(data2);
        // const { data, isPending, error } = usePost('/api/account/login', JSON.stringify(login));

        // console.log(data);
        // fetch('https://localhost:7155/api/account/login', {
        //     method: 'POST',
        //     headers: { "Content-Type" : "application/json"},
        //     body: JSON.stringify(login)
        // }).then(res => {
        //     if (!res.ok) {        
        //         throw Error('There was an error with the request!'); 
        //     }
        //     return res.json();
        // })
        // .then(data => {
        //     setData(data);
        //     console.log('data');
        //     console.log(data);
        //     setIsPending(false);
        //     setError(null);
        // })
        // .catch(err => {
        //     if (err.name === 'AbortError') {
        //         console.log('Fetch aborted.');
        //     } else {
        //         console.log('catch block');
        //         console.log(err.message);
        //         setIsPending(false);
        //         setError(err.message);
        //     }
        // })
    }

    return (
        <Container>
            <h1>Login Page</h1>
            {isPending && <div>Loading..</div>}
            {error && <AlertDissmisable state={true} />}
            <Form onSubmit={handleSubmit}>
                <Form.Group className="mb-3" controlId="formBasicEmail">
                    <Form.Label>Email address</Form.Label>
                    <Form.Control type="email" placeholder="Enter email" value={email} onChange={(e) => setEmail(e.target.value)} />
                    <Form.Text className="text-muted">
                        We'll never share your email with anyone else.
                    </Form.Text>
                </Form.Group>

                <Form.Group className="mb-3" controlId="formBasicPassword">
                    <Form.Label>Password</Form.Label>
                    <Form.Control type="password" placeholder="Password" value={password} onChange={(e) => setPassword(e.target.value)} />
                </Form.Group>
                <Button variant="outline-dark" type="submit">
                    Submit
                </Button>
            </Form>
        </Container>
    );
}

export default LoginPage;