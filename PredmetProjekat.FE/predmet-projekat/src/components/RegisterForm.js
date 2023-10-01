import { Form, Button } from "react-bootstrap";

function RegisterForm() {

    return (
        <Form>
            <Form.Group className="mb-3" controlId="formBasicEmail">
                <Form.Label>Email address</Form.Label>
                <Form.Control type="email" placeholder="Enter email" />
                <Form.Text className="text-muted">
                    We'll never share your email with anyone else.
                </Form.Text>
            </Form.Group>
            <Form.Group className="mb-3" controlId="formBasicPassword">
                <Form.Label>Username</Form.Label>
                <Form.Control type="text" placeholder="Username" />
            </Form.Group>
            <Form.Group className="mb-3" controlId="formBasicPassword">
                <Form.Label>First name</Form.Label>
                <Form.Control type="text" placeholder="FirstName" />
            </Form.Group>
            <Form.Group className="mb-3" controlId="formBasicPassword">
                <Form.Label>Last name</Form.Label>
                <Form.Control type="text" placeholder="LastName" />
            </Form.Group>
            <Button variant="dark" type="submit">
                Submit
            </Button>
        </Form>
    );
}

export default RegisterForm;