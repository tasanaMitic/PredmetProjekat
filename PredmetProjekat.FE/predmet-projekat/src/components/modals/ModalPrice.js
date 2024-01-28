import { Button, Form, Modal } from "react-bootstrap";
import PropTypes from 'prop-types';
import { useEffect, useState } from "react";
import { setProductPrice } from "../../api/methods";

const ModalPrice = ({ show, setShow, setError, setErrorModal, setData, product }) => {
    const [price, setPrice] = useState(null);

    useEffect(() => {
        setPrice(product.price);
    }, [product]);

    const handleClose = () => {
        
        setShow(false);
    }

    const handleChange = (value) => {
        if (!value || value.match(/^\d{1,}(\.\d{0,4})?$/)) {
            setPrice(value);
        }
    }

    const handleSubmit = (e) => {
        e.preventDefault();
        const payload = { value: price };

        setProductPrice(product.productId, payload).then(res => {
            if (res.status !== 200) {
                throw Error('There was an error with the request!');
            }
            return res.data;
        })
            .then((data) => {
                setShow(false);
                setPrice(0);
                setData(data);
            }).catch(err => {
                setError(err.response);
                setErrorModal(true);
                setShow(false);
                setPrice(0);
            });
    }


    return (
        <Modal show={show} onHide={handleClose}>
            <Modal.Header closeButton>
                <Modal.Title>Please provide price</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form onSubmit={handleSubmit}>
                    <Form.Group className="mb-3" controlId="formBasicName">
                        <Form.Label>Price of product</Form.Label>
                        <Form.Control type="number"
                            placeholder="Enter price"
                            value={price}
                            min="0.00"
                            step="0.1"
                            presicion={2}
                            onChange={(e) => handleChange(e.target.value)}
                            required />
                    </Form.Group>
                    <Button variant="dark" type="submit">
                        Submit
                    </Button>
                </Form>
            </Modal.Body>
        </Modal>
    );

}

ModalPrice.propTypes = {
    show: PropTypes.bool,
    setShow: PropTypes.func,
    setError: PropTypes.func,
    setErrorModal: PropTypes.func,
    setData: PropTypes.func,
    product: PropTypes.shape({
        productId: PropTypes.string,
        price: PropTypes.number,
    })
}

export default ModalPrice;