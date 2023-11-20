import { Container, Form, Button, Modal } from 'react-bootstrap';
import { useState } from 'react';
import PropTypes from 'prop-types';
import ModalError from '../modals/ModalError';
import { addRegister } from '../../api/methods';


const ModalRegister = ({ show, setShow, setData, setError, setErrorModal}) => {
    const [dataForm, setDataForm] = useState({ location: '', registerCode: '' });

    const handleClose = () => {
        setShow(false);
    }

    const handleSubmit = (e) => {
        e.preventDefault(); 

        const payload = { location: dataForm.location, registerCode: dataForm.registerCode };
        addRegister(payload).then(res => {
            if (res.status !== 201) {
                throw Error('There was an error with the request!');
            }
            return res.data;
        })
            .then(data => {
                setData(registers => [...registers, data]);
                setShow(false);
                setDataForm({ location: '', registerCode: '' });
            })
            .catch(err => {
                setShow(false);
                setError(err.response);
                setErrorModal(true);
            })
    }


    return (
        <Modal show={show} onHide={handleClose}>
            <Modal.Header closeButton>
                <Modal.Title>Please enter the info about</Modal.Title>
            </Modal.Header>
            <Container>
                <Modal.Body>
                    <Form onSubmit={handleSubmit}>                        
                        <Form.Group className="mb-3" controlId="formBasicLocation">
                            <Form.Label>Location</Form.Label>
                            <Form.Control type="text" 
                            placeholder="Location" 
                            name="location" 
                            value={dataForm.location}
                             onChange={(e) => setDataForm({ ...dataForm, location: e.target.value })} 
                             required/>
                        </Form.Group>
                        <Form.Group className="mb-3" controlId="formBasicRegisterCode">
                            <Form.Label>Register code</Form.Label>
                            <Form.Control type="text" 
                            placeholder="Register Code" 
                            name="registerCode"
                            value={dataForm.registerCode}
                             onChange={(e) => setDataForm({ ...dataForm, registerCode: e.target.value })} 
                             required />
                        </Form.Group>
                        <Button variant="dark" type="submit"  disabled={!dataForm.location || !dataForm.registerCode}>
                            Submit
                        </Button>
                    </Form>
                </Modal.Body>
            </Container>
        </Modal>
    );
}

ModalRegister.propTypes = {
    show: PropTypes.bool,
    setShow: PropTypes.func,
    setError: PropTypes.func,
    setErrorModal: PropTypes.func,
    setData: PropTypes.func
}

export default ModalRegister;