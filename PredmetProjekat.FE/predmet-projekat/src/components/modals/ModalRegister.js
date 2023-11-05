import { Container, Form, Button, Modal } from 'react-bootstrap';
import { useState } from 'react';
import ModalError from '../modals/ModalError';
import { addRegister } from '../../api/methods';


const ModalRegister = ({ show, setShow, confirm}) => {
    const [formData, setFormData] = useState({ location: '', registerCode: '' });

    const [error, setError] = useState(null);
    const [successModal, setSuccessModal] = useState(false);
    const [errorModal, setErrorModal] = useState(false);
    const [successMessage, setSuccessMessage] = useState(null);

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setFormData((prevFormData) => ({ ...prevFormData, [name]: value }));
    };


    const handleClose = () => {
        setShow(false);
    }

    const handleSubmit = (e) => {
        e.preventDefault(); 

        console.log("ne radi jos uvek");
        //todo
        //addRegister().then
    }


    return (
        <Modal show={show} onHide={handleClose}>
            <Modal.Header closeButton>
                <Modal.Title>Please enter the info about</Modal.Title>
            </Modal.Header>
            <Container>
                <Modal.Body>
                    <Form onSubmit={handleSubmit}>
                        {error && <ModalError setShow={setErrorModal} show={errorModal} error={error} setError={setError} />}
                        <Form.Group className="mb-3" controlId="formBasicLocation">
                            <Form.Label>Location</Form.Label>
                            <Form.Control type="text" 
                            placeholder="Location" 
                            name="location" 
                            value={formData.location}
                             onChange={handleInputChange} 
                             required/>
                        </Form.Group>
                        <Form.Group className="mb-3" controlId="formBasicRegisterCode">
                            <Form.Label>Register code</Form.Label>
                            <Form.Control type="text" 
                            placeholder="Register Code" 
                            name="registerCode"
                            value={formData.registerCode}
                             onChange={handleInputChange} 
                             required />
                        </Form.Group>
                        <Button variant="dark" type="submit"  disabled={!formData.location || !formData.registerCode}>
                            Submit
                        </Button>
                    </Form>
                </Modal.Body>
            </Container>
        </Modal>
    );
}

export default ModalRegister;