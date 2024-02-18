import { Button, Col, Container, Modal, Row} from "react-bootstrap";
import PropTypes from 'prop-types';

const ModalProductTypeDetails = ({ show, setShow, productType }) => {
    const handleClose = () => {
        setShow(false);
    }

    return (
        <Modal show={show} onHide={handleClose}>
            <Modal.Header closeButton>
                {productType && <Modal.Title>Product type - {productType.name}</Modal.Title>}
            </Modal.Header>
            <Modal.Body>
                {productType && <Container>
                    <Row >
                        <Col style={{ fontWeight: 'bold' }}>Product type name:</Col>
                        <Col>{productType.name}</Col>
                    </Row>
                    {productType.attributes &&
                        <Row>
                            <Col style={{ fontWeight: 'bold' }}>Attributes:</Col>
                            <Col>
                                {productType.attributes.map((attribute) => (
                                    <Row key={attribute.attributeId}>
                                        <Col>{attribute.attributeName}</Col>
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

ModalProductTypeDetails.propTypes = {
    show: PropTypes.bool,
    setShow: PropTypes.func,
    productType: PropTypes.shape({
        name: PropTypes.string,
        productTypeId: PropTypes.string,
        attributes: PropTypes.arrayOf(PropTypes.shape({
            attributeName: PropTypes.string,
            attributeId: PropTypes.string,
        })),

    })
}

export default ModalProductTypeDetails;