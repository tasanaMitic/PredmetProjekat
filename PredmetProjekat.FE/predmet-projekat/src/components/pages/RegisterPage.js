import { Container, Accordion } from "react-bootstrap";
import { useState } from "react";
import RegisterForm from "../RegisterForm";

function RegisterPage() {
    return (
        <Container>
            <h1>Register Page</h1>
            <Accordion >
                <Accordion.Item eventKey="0">
                    <Accordion.Header>Register admin</Accordion.Header>
                    <Accordion.Body>
                    <RegisterForm userType='admin'></RegisterForm>
                    </Accordion.Body>
                </Accordion.Item>
                <Accordion.Item eventKey="1">
                    <Accordion.Header>Register employee</Accordion.Header>
                    <Accordion.Body>
                    <RegisterForm userType='employee'></RegisterForm>
                    </Accordion.Body>
                </Accordion.Item>
            </Accordion>
        </Container>
    );
}

export default RegisterPage;