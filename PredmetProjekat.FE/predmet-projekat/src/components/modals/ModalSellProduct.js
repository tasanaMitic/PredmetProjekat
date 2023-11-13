import { Button, Form, Modal, Table } from "react-bootstrap";
import PropTypes from 'prop-types';
import { useEffect, useState } from "react";
import { getRegisters, sellProduct } from "../../api/methods";

const ModalSell = ({ show, setShow, setError, setErrorModal, selectedProducts, setSuccessModal, setSuccessMessage }) => {
    const [quantities, setQuantities] = useState(null);
    const [registers, setRegisters] = useState(null);
    const [register, setRegister] = useState(null);

    const handleClose = () => {
        setShow(false);
    }

    useEffect(() => {
        setQuantities(selectedProducts.map((product) => {
            return { productId: product.productId, quantity: 0 };
        }));
        console.log('useeffect');
        console.log(quantities);        

        getRegisters().then(res => {
            if (res.status !== 200) {
                throw Error('There was an error with the request!');
            }
            return res.data;
        })
            .then(data => {
                setRegisters(data);
                console.log(quantities);
            })
            .catch(err => {
                setRegisters(null);
            })
    }, [selectedProducts]);

    const handleQuantityChange = (productId, value) => {
        setQuantities(quantities.map((item) =>
            item.productId === productId ? { ...item, quantity: parseInt(value) } : item
        ));
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
            console.log('da2');
            setSuccessModal(true);
            setSuccessMessage('You have successfully made a sale!');
            setShow(false);

        }).catch(err => {
            console.log('ne');
            setError(err.response);
            setErrorModal(true);
            setShow(false);

            console.log(err);
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
                                <th>Season</th>
                                <th>Sex</th>
                                <th>Size</th>
                                <th>Category</th>
                                <th>Brand</th>
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
                                    <td>{product.season}</td>
                                    <td>{product.sex}</td>
                                    <td>{product.size}</td>
                                    <td>{product.category.name}</td>
                                    <td>{product.brand.name}</td>
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
                    <Button variant="dark" type="submit" disabled={!register}>
                        Submit
                    </Button>
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