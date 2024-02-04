import { Button, Col, Container, Modal, Row } from "react-bootstrap";
import PropTypes from 'prop-types';
import moment from 'moment';

const ModalSaleDetails = ({ show, setShow, sale }) => {
    const handleClose = () => {
        setShow(false);
    }

    return (
        <Modal show={show} onHide={handleClose}>
            <Modal.Header closeButton>
                <Modal.Title>Sale details</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                {sale && <Container className="d-flex flex-column">
                    <Row >
                        <Col style={{ fontWeight: 'bold' }}>Sale date:</Col>
                        <Col>{moment(sale.date).format('DD/MM/YYYY')}</Col>
                    </Row>
                    <Row style={{ backgroundColor: "#FFE4E1" }} >
                        <Col style={{ fontWeight: 'bold' }}>Sale time:</Col>
                        <Col>{moment(sale.date).format('HH:mm')}</Col>
                    </Row>
                    <Row>
                        <Col style={{ fontWeight: 'bold' }}>Register location:</Col>
                        <Col>{sale.register.location}</Col>
                    </Row>
                    <Row style={{ backgroundColor: "#FFE4E1" }}>
                        <Col style={{ fontWeight: 'bold' }}>Register code:</Col>
                        <Col>{sale.register.registerCode}</Col>
                    </Row>
                    <Row>
                        <Col style={{ fontWeight: 'bold' }}>Sold by:</Col>
                        <Col>{sale.soldBy.username}</Col>
                    </Row>
                    <Row style={{ backgroundColor: "#FFE4E1" }}>
                        <Col style={{ fontWeight: 'bold' }}>Total price:</Col>
                        <Col>{sale.totalPrice}$</Col>
                    </Row>
                    {sale.soldProducts &&
                        <Row>
                            <Col style={{ fontWeight: 'bold' }}>Sold products:</Col>
                            <div style={{ border: '1px solid #000' }} />
                            <Row>
                                {sale.soldProducts.map((soldProduct) => (
                                    <Container>
                                        <Row key={soldProduct.soldProductId}>
                                            <Row>
                                                <Col style={{ fontWeight: 'bold', textAlign: 'right' }}>Name:</Col>
                                                <Col>{soldProduct.product.name}</Col>
                                            </Row>
                                            <Row>
                                                <Col style={{ fontWeight: 'bold', textAlign: 'right' }}>Product type:</Col>
                                                <Col>{soldProduct.product.productType.name}</Col>
                                            </Row>
                                            <Row>
                                                <Col style={{ fontWeight: 'bold', textAlign: 'right' }}>Price:</Col>
                                                <Col>{soldProduct.product.price}$</Col>
                                            </Row>
                                            <Row>
                                                <Col style={{ fontWeight: 'bold', textAlign: 'right' }}>Quantity:</Col>
                                                <Col>{soldProduct.quantity}</Col>
                                            </Row>
                                        </Row>
                                        <div style={{ border: '1px solid #000' }} />
                                    </Container>
                                ))}
                            </Row>
                        </Row>}
                        <Button variant="dark" style={{ marginTop: '10px' }}  onClick={handleClose}> Ok </Button>
                </Container>}
            </Modal.Body>
        </Modal>
    );

}

ModalSaleDetails.propTypes = {
    show: PropTypes.bool,
    setShow: PropTypes.func,
    sale: PropTypes.shape({
        date: PropTypes.string,
        totalPrice: PropTypes.number,
        register: PropTypes.shape({
            location: PropTypes.string,
            registerCode: PropTypes.string,
        }),
        soldBy: PropTypes.shape({
            username: PropTypes.string,
        }),
        soldProducts: PropTypes.arrayOf(PropTypes.shape({
            name: PropTypes.string,
            productId: PropTypes.string,
            quantity: PropTypes.number,
            price: PropTypes.number,
            productType : PropTypes.shape({
                name: PropTypes.string,
            })
        })),
    })
}

export default ModalSaleDetails;