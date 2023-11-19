import { Container, Form, Button } from 'react-bootstrap';
import { useState, useEffect } from 'react';
import ModalSuccess from '../modals/ModalSuccess';
import ModalError from '../modals/ModalError';
import ModalRegister from '../modals/ModalRegister';

const RegistersPage = () => {
    const [error, setError] = useState(null);
    const [successModal, setSuccessModal] = useState(false);
    const [errorModal, setErrorModal] = useState(false);
    const [successMessage, setSuccessMessage] = useState(null);
    const [registerModal, setRegisterModal] = useState(false);
    

    useEffect(() => {

    }, []);

    const handleClick = () => {
        setRegisterModal(true);
    }

    return (
        <Container>
            <h1>Registers</h1>
            <h2>Ovde nista ne radi</h2>
            <ModalRegister show={registerModal} setShow={setRegisterModal}></ModalRegister>
            <Button variant="outline-dark" onClick={handleClick} >Add register</Button>
        </Container>
    );
}

export default RegistersPage;