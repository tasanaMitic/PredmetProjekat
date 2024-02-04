import { Button, Col, Container, Modal, Row} from "react-bootstrap";
import PropTypes from 'prop-types';

const ModalProductDetails = ({ show, setShow, product }) => {
    const handleClose = () => {
        setShow(false);
    }

    return (
        <Modal show={show} onHide={handleClose}>
            <Modal.Header closeButton>
                {product && <Modal.Title>Product - {product.name}</Modal.Title>}
            </Modal.Header>
            <Modal.Body>
                {product && <Container>
                    <Row >
                        <Col style={{ fontWeight: 'bold' }}>Product name:</Col>
                        <Col>{product.name}</Col>
                    </Row>
                    <Row style={{ backgroundColor: "#FFE4E1" }}>
                        <Col style={{ fontWeight: 'bold' }}>Product brand:</Col>
                        <Col>{product.brand.name}</Col>
                    </Row>
                    <Row >
                        <Col style={{ fontWeight: 'bold' }}>Product category:</Col>
                        <Col>{product.category.name}</Col>
                    </Row>
                    <Row style={{ backgroundColor: "#FFE4E1" }}>
                        <Col style={{ fontWeight: 'bold' }}>Product type:</Col>
                        <Col>{product.productType.name}</Col>
                    </Row>
                    {product.attributeValues &&
                        <Row>
                            <Col style={{ fontWeight: 'bold' }}>Attributes:</Col>
                            <Col>
                                {product.attributeValues.map((attribute) => (
                                    <Row key={attribute.attributeName}>
                                        <Col style={{ fontWeight: 'bold' }}>{attribute.attributeName}:</Col>
                                        <Col>{attribute.attributeValue}</Col>
                                    </Row>
                                ))}
                            </Col>
                        </Row>}
                </Container>}
                <Button variant="dark" onClick={handleClose}> Ok </Button>
            </Modal.Body>
        </Modal>
    );

}

ModalProductDetails.propTypes = {
    show: PropTypes.bool,
    setShow: PropTypes.func,
    product: PropTypes.shape({
        name: PropTypes.string,
        price: PropTypes.number,
        quantity: PropTypes.number,
        isInStock: PropTypes.bool,
        productType: PropTypes.shape({
            name: PropTypes.string,
        }),
        category: PropTypes.shape({
            name: PropTypes.string,
        }),
        brand: PropTypes.shape({
            name: PropTypes.string,
        }),
        attributeValues: PropTypes.arrayOf(PropTypes.shape({
            attributeName: PropTypes.string,
            attributeValue: PropTypes.string,
        })),

    })
}

export default ModalProductDetails;