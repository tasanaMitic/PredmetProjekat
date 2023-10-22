import { Button, Form, Modal } from "react-bootstrap";
import PropTypes from 'prop-types';
import { useState } from "react";
import { addCategory } from "../../api/methods";

const ModalCategory = ({ show, setShow, setError, setErrorModal, setData}) => {
    const [name, setName] = useState('');

    const handleClose = () => {
        setShow(false);
    }

    const handleSubmit = (e) => {
        e.preventDefault(); 
        const payload = {name: name};

        addCategory(payload).then(res => {
            if (res.status !== 201) {       
                throw Error('There was an error with the request!'); 
            }
            return res.data;
        })
        .then((data) => {
            setShow(false);
            setName('');
            setData(categories =>[...categories, data]);
        }).catch(err => {
            setError(err.response);
            setErrorModal(true);
            setShow(false);
            setName('');
        });
    }

    return (
        <Modal show={show} onHide={handleClose}>
            <Modal.Header closeButton>
                <Modal.Title>Please provide category name</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form onSubmit={handleSubmit}>
                    <Form.Group className="mb-3" controlId="formBasicName">
                        <Form.Label>Category name</Form.Label>
                        <Form.Control type="name" placeholder="Enter name" value={name} onChange={(e) => setName(e.target.value)} required />
                    </Form.Group>
                    <Button variant="dark" type="submit">
                        Submit
                    </Button>
                </Form>
            </Modal.Body>
        </Modal>
        );
}

ModalCategory.propTypes = {
    show: PropTypes.bool,
    setShow: PropTypes.func,
    setError: PropTypes.func,
    setErrorModal: PropTypes.func,
    setData: PropTypes.func
}

export default ModalCategory;