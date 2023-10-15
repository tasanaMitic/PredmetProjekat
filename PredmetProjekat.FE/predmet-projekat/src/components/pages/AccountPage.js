import { Container, Form, Button } from "react-bootstrap";

function AccountPage() {
    
//zapoceto pozivanje endpointa za dobavljanje svih info o ulogovanom useru/
//odradjen eednpoint na beku
    return (
        <Container>
            <h1>My Account</h1>
            <Form>
                <Form.Group className="mb-3" controlId="formBasicFirstName">
                    <Form.Label>FirstName</Form.Label>
                    <Form.Control type="email" placeholder="heloo" />
                    <Form.Text className="text-muted">
                        We'll never share your email with anyone else.
                    </Form.Text>
                </Form.Group>
                <Form.Group className="mb-3" controlId="formBasicEmail">
                    <Form.Label>Email address</Form.Label>
                    <Form.Control type="email" placeholder="heloo" />
                    <Form.Text className="text-muted">
                        We'll never share your email with anyone else.
                    </Form.Text>
                </Form.Group>

                <Form.Group className="mb-3" controlId="formBasicPassword">
                    <Form.Label>Password</Form.Label>
                    <Form.Control type="password" placeholder="heloo" />
                </Form.Group>
                <Button variant="outline-dark" type="submit">
                    Submit
                </Button>
            </Form>
        </Container>
    );
}

export default AccountPage;