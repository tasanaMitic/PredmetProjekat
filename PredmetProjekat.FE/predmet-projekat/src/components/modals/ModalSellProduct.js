import { Button, Col, Container, Form, Modal, Row, Table } from "react-bootstrap";
import PropTypes from 'prop-types';
import { useEffect, useState } from "react";
import { getRegisters, sellProduct } from "../../api/methods";

const ModalSell = ({ show, setShow, setError, setErrorModal, selectedProducts, setSuccessModal, setSuccessMessage }) => {
    const [quantities, setQuantities] = useState(null);
    const [registers, setRegisters] = useState(null);
    const [register, setRegister] = useState(null);
    const [totalPrice, setTotalPrice] = useState(0);

    const handleClose = () => {
        setShow(false);
    }

    useEffect(() => {
        setQuantities(selectedProducts.map((product) => {
            return { productId: product.productId, quantity: 0 };
        }));

        getRegisters().then(res => {
            if (res.status !== 200) {
                throw Error('There was an error with the request!');
            }
            return res.data;
        })
            .then(data => {
                setRegisters(data);
            })
            .catch(err => {
                setRegisters(null);
            })
    }, [selectedProducts]);

    const handleQuantityChange = (productId, value) => {
        const updatedQuantities = quantities.map((item) =>
            item.productId === productId ? { ...item, quantity: parseInt(value) } : item
        );

        const updatedTotalPrice = updatedQuantities.reduce(
            (total, { productId, quantity }) =>
                total + quantity * (selectedProducts.find((product) => product.productId === productId)?.price || 0),
            0
        );

        setQuantities(updatedQuantities);
        setTotalPrice(Math.round(updatedTotalPrice * 100) / 100);
    };
    const handleRegisterChange = (registerId) => {
        setRegister(registerId === 'default' ? null : registerId);
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        const payload = {
            registerId: register,
            soldProducts: quantities
        };

        sellProduct(payload).then(res => {
            if (res.status !== 204) {
                throw Error('There was an error with the request!');
            }
            setSuccessModal(true);
            setSuccessMessage('You have successfully made a sale!');
            setShow(false);

        }).catch(err => {
            setError(err.response);
            setErrorModal(true);
            setShow(false);
        });
    };

    return (

        <Modal show={show} onHide={handleClose}>
            <Modal.Header closeButton>
                <Modal.Title>Please provide quantity</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form onSubmit={handleSubmit}>
                    <Table striped hover >
                        <thead>
                            <tr>
                                <th>Select quantity</th>
                                <th>Product name</th>
                                <th>Category</th>
                                <th>Brand</th>
                                <th>Price</th>
                            </tr>
                        </thead>
                        <tbody>
                            {selectedProducts.map((product) => (
                                <tr key={product.productId}>
                                    <td>
                                        <Form.Group className="mb-3" controlId={`formBasicName${product.productId}`}>
                                            <Form.Control type="number"
                                                placeholder={0}
                                                onChange={(e) => handleQuantityChange(product.productId, e.target.value)}
                                                required
                                                min={0}
                                                max={product.quantity} />
                                        </Form.Group>
                                    </td>
                                    <td>{product.name}</td>
                                    <td>{product.category.name}</td>
                                    <td>{product.brand.name}</td>
                                    <td>{product.price}$</td>
                                </tr>
                            ))}
                        </tbody>
                    </Table>
                    <Form.Group className="mb-3" controlId="formBasicName">
                        <Form.Label>Choose register</Form.Label>
                        {registers ?
                            <Form.Select aria-label="Default select example" onChange={(e) => handleRegisterChange(e.target.value)}>
                                <option value="default" key="default">Select register</option>
                                {registers.map((register) => <option value={register.registerId} key={register.registerId}>{register.registerCode}</option>)}
                            </Form.Select>
                            : <p value="default">There are no available registers.</p>}
                    </Form.Group>
                    <Container>
                        <Row>
                            <Col style={{ fontWeight: 'bold', fontSize: '20px' }}>Total price:</Col>
                            <Col style={{ fontWeight: 'bold', fontSize: '20px' }}>{totalPrice}$</Col>
                        </Row>
                    </Container>
                    <Container className="d-flex flex-column align-items-center p-3">
                        <Button variant="dark" type="submit" disabled={!register}>
                            Submit
                        </Button>
                    </Container>
                </Form>
            </Modal.Body>
        </Modal>
    );

}

ModalSell.propTypes = {
    show: PropTypes.bool,
    setShow: PropTypes.func,
    setError: PropTypes.func,
    setErrorModal: PropTypes.func,
    selectedProducts: PropTypes.arrayOf(PropTypes.shape({
        productId: PropTypes.string,
        name: PropTypes.string,
        season: PropTypes.string,
        sex: PropTypes.string,
        size: PropTypes.string,
        brand: PropTypes.shape({
            brandId: PropTypes.string,
            name: PropTypes.string
        }),
        category: PropTypes.shape({
            categoryId: PropTypes.string,
            name: PropTypes.string
        })
    }))
}

export default ModalSell;