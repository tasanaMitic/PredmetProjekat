import { Button, Form, Modal } from "react-bootstrap";
import PropTypes from 'prop-types';
import { useState } from "react";
import { stockProduct } from "../../api/methods";

const ModalStock = ({ show, setShow, setError, setErrorModal, setData, productId }) => {
    const [quantity, setQuantity] = useState(0);

    const handleClose = () => {
        setQuantity(0);
        setShow(false);
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        const payload = { value: quantity };

        stockProduct(productId, payload).then(res => {
            if (res.status !== 200) {
                throw Error('There was an error with the request!');
            }
            return res.data;
        })
            .then((data) => {
                setShow(false);
                setQuantity(0);
                setData(data);
            }).catch(err => {
                setError(err.response);
                setErrorModal(true);
                setShow(false);
                setQuantity(0);
            });
    }

    return (
        <Modal show={show} onHide={handleClose}>
            <Modal.Header closeButton>
                <Modal.Title>Please provide quantity</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form onSubmit={handleSubmit}>
                    <Form.Group className="mb-3" controlId="formBasicName">
                        <Form.Label>Quantity of product</Form.Label>
                        <Form.Control type="number"
                         placeholder="Enter quantity" 
                         value={quantity} 
                         onChange={(e) => setQuantity(e.target.value)} 
                         required
                         min={0}
                         max={50} />
                    </Form.Group>
                    <Button variant="dark" type="submit">
                        Submit
                    </Button>
                </Form>
            </Modal.Body>
        </Modal>
    );

}

ModalStock.propTypes = {
    show: PropTypes.bool,
    setShow: PropTypes.func,
    setError: PropTypes.func,
    setErrorModal: PropTypes.func,
    setData: PropTypes.func,
    productId: PropTypes.string
}

export default ModalStock;